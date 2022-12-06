using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2022.Days
{
	public static class CrateStacker
	{
		public struct Instruction
		{
			public int Count { get; set; }
			public int SrcIndex { get; set; }
			public int DestIndex { get; set; }
		}

		public static Instruction ToInstruction(this string instructionText)
		{
			var tokens = instructionText.Split(' ');
			return new Instruction()
			{
				Count = int.Parse(tokens[1]),
				SrcIndex = int.Parse(tokens[3]) - 1,
				DestIndex = int.Parse(tokens[5]) - 1,
			};
		}

		public static List<Stack<char>> GetInitialStacks()
		{
			// some of this is hardcoded to make my life easier
			var lines = File.ReadLines("Inputs\\crates_input.txt");

			var stacks = new List<Stack<char>>();
			for (int i = 0; i < 9; i++) stacks.Add(new Stack<char>());

			foreach (var line in lines.Take(8).Reverse())
			{
				for (int i = 0; i < 9; i++)
				{
					if (char.IsLetter(line[i * 4 + 1])) stacks[i].Push(line[i * 4 + 1]);
				}
			}

			return stacks;
		}

		public static IEnumerable<string> GetInstructions()
		{
			var lines = File.ReadLines("Inputs\\crates_input.txt");
			return lines.Skip(10);
		}

		public static void DoInstruction9000(List<Stack<char>> stacks, string instructionText)
		{
			var inst = instructionText.ToInstruction();

			for (int i = 0; i < inst.Count; i++)
			{
				stacks[inst.DestIndex].Push(stacks[inst.SrcIndex].Pop());
			}
		}

		public static void DoInstruction9001(List<Stack<char>> stacks, string instructionText)
		{
			var inst = instructionText.ToInstruction();
			var tmpStack = new Stack<char>();

			for (int i = 0; i < inst.Count; i++)
			{
				tmpStack.Push(stacks[inst.SrcIndex].Pop());
			}

			while (tmpStack.Count > 0) stacks[inst.DestIndex].Push(tmpStack.Pop());
		}
	}
}
