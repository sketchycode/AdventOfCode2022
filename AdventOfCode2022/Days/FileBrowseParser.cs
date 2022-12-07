using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode2022.Days
{
	public static class FileBrowseParser
	{
		public class ElfFileSystem
		{
			public ElfFolder RootFolder { get; set; }
			public ElfFolder CurrentFolder { get; set; }
		}

		public class ElfFolder
		{
			public string Name { get; set; }
			public List<ElfFile> Files { get; } = new List<ElfFile>();
			public ElfFolder ParentFolder { get; set; }
			public List<ElfFolder> SubFolders { get; } = new List<ElfFolder>();
			public int Size => Files.Sum(f => f.Size) + SubFolders.Sum(f => f.Size);
		}

		public class ElfFile
		{
			public string Name { get; set; }
			public int Size { get; set; }
		}

		public static ElfFileSystem GetFileSystem()
		{
			var lines = File.ReadLines("Inputs\\dirlist_input.txt");
			var fileSystem = new ElfFileSystem();

			foreach (var line in lines)
			{
				Action<ElfFileSystem, string> command = line switch
				{
					string l when l.StartsWith("$ cd") => ParseChangeDirectory,
					string l when l == "$ ls" => ParseList,
					string l when l.StartsWith("dir") => ParseListFolderResult,
					string l when char.IsDigit(l[0]) => ParseListFileResult,
					_ => throw new NotImplementedException(),
				};

				command(fileSystem, line);
			}

			return fileSystem;
		}

		public static List<ElfFolder> FindFolders(ElfFileSystem fs, Predicate<ElfFolder> pred)
		{
			List<ElfFolder> matchedFolders = new List<ElfFolder>();

			Queue<ElfFolder> uncheckedFolders = new Queue<ElfFolder>();
			uncheckedFolders.Enqueue(fs.RootFolder);

			while (uncheckedFolders.Count > 0)
			{
				var curFolder = uncheckedFolders.Dequeue();
				curFolder.SubFolders.ForEach(f => uncheckedFolders.Enqueue(f));

				if (pred(curFolder)) matchedFolders.Add(curFolder);
			}

			return matchedFolders;
		}

		private static void ParseChangeDirectory(ElfFileSystem fs, string cmdText)
		{
			var targetFolderName = cmdText.Replace("$ cd ", null);

			switch (targetFolderName)
			{
				case string s when s == "/":
					if (fs.RootFolder == null) fs.RootFolder = new ElfFolder();
					fs.CurrentFolder = fs.RootFolder;
					break;
				case string s when s == "..":
					fs.CurrentFolder = fs.CurrentFolder.ParentFolder;
					break;
				default:
					var subFolder = fs.CurrentFolder.SubFolders.FirstOrDefault(f => f.Name == targetFolderName);
					if (subFolder == null) subFolder = CreateFolderIfNecessary(fs.CurrentFolder, targetFolderName);
					fs.CurrentFolder = subFolder;
					break;
			}
		}

		private static void ParseList(ElfFileSystem fs, string cmdText)
		{
			// do nothing currently
		}

		private static void ParseListFolderResult(ElfFileSystem fs, string cmdText)
		{
			var folderName = cmdText.Replace("dir ", null);
			CreateFolderIfNecessary(fs.CurrentFolder, folderName);
		}

		private static void ParseListFileResult(ElfFileSystem fs, string cmdText)
		{
			var tokens = cmdText.Split(' ');
			CreateFileIfNecessary(fs.CurrentFolder, tokens[1], int.Parse(tokens[0]));
		}

		private static ElfFolder CreateFolderIfNecessary(ElfFolder parentFolder, string name)
		{
			var folder = parentFolder.SubFolders.FirstOrDefault(f => f.Name == name);
			if (folder == null)
			{
				folder = new ElfFolder();
				folder.Name = name;
				folder.ParentFolder = parentFolder;
				parentFolder.SubFolders.Add(folder);
			}
			return folder;
		}

		private static void CreateFileIfNecessary(ElfFolder containingFolder, string name, int size)
		{
			var file = containingFolder.Files.FirstOrDefault(f => f.Name == name);
			if (file == null)
			{
				file = new ElfFile()
				{
					Name = name,
					Size = size,
				};
				containingFolder.Files.Add(file);
			}
		}
	}
}
