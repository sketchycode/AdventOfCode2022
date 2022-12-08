using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2022.Days
{
	public static class TreeHouseFinder
	{
		public class Tree
		{
			public int Height { get; set; }
			public Tree NorthTree { get; set; }
			public Tree SouthTree { get; set; }
			public Tree EastTree { get; set; }
			public Tree WestTree { get; set; }
			public bool IsVisible
			{
				get
				{
					if (NorthTree == null || SouthTree == null || EastTree == null || WestTree == null) return true;
					if (GetTreesToEdge(t => t.NorthTree).TrueForAll(t => t.Height < Height)) return true;
					if (GetTreesToEdge(t => t.SouthTree).TrueForAll(t => t.Height < Height)) return true;
					if (GetTreesToEdge(t => t.EastTree).TrueForAll(t => t.Height < Height)) return true;
					if (GetTreesToEdge(t => t.WestTree).TrueForAll(t => t.Height < Height)) return true;
					return false;
				}
			}

			public int ScenicScore
			{
				get
				{
					return 
						GetViewingDistance(t => t.NorthTree) *
						GetViewingDistance(t => t.SouthTree) *
						GetViewingDistance(t => t.EastTree) *
						GetViewingDistance(t => t.WestTree);
				}
			}

			private List<Tree> GetTreesToEdge(Func<Tree, Tree> dirFunc)
			{
				List<Tree> trees = new List<Tree>();
				var tree = dirFunc(this);
				while (tree != null)
				{
					trees.Add(tree);
					tree = dirFunc(tree);
				}
				return trees;
			}

			private int GetViewingDistance(Func<Tree, Tree> dirFunc)
			{
				int distance = 0;
				var tree = dirFunc(this);

				while (tree != null)
				{
					distance++;
					if (tree.Height >= Height) break;
					tree = dirFunc(tree);
				}

				return distance;
			}
		}

		private const int ForestSize = 99;
		public static Tree[,] GetTrees()
		{
			var lines = File.ReadAllLines("Inputs\\trees_input.txt");
			var trees = new Tree[ForestSize, ForestSize];

			for (int y = 0; y < lines.Length; y++)
			{
				var line = lines[y];
				for (int x = 0; x < line.Length; x++)
				{
					trees[y, x] = new Tree() { Height = int.Parse(line[x].ToString()) };
				}
			}

			for (int y = 0; y < ForestSize; y++)
			{
				for (int x = 0; x < ForestSize; x++)
				{
					trees[y, x].NorthTree = GetNorthTree(trees, x, y);
					trees[y, x].SouthTree = GetSouthTree(trees, x, y);
					trees[y, x].EastTree = GetEastTree(trees, x, y);
					trees[y, x].WestTree = GetWestTree(trees, x, y);
				}
			}

			return trees;
		}

		private static Tree GetNorthTree(Tree[,] trees, int x, int y)
		{
			return y == 0 ? null : trees[y - 1, x];
		}

		private static Tree GetSouthTree(Tree[,] trees, int x, int y)
		{
			return y == ForestSize - 1 ? null : trees[y + 1, x];
		}

		private static Tree GetEastTree(Tree[,] trees, int x, int y)
		{
			return x == ForestSize - 1 ? null : trees[y, x + 1];
		}

		private static Tree GetWestTree(Tree[,] trees, int x, int y)
		{
			return x == 0 ? null : trees[y, x - 1];
		}
	}
}
