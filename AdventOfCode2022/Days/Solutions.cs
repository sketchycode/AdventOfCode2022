using AdventOfCode2022.Days;
using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2022
{
	public static class Solutions
	{
		#region Day01
		public static void SolveDay01Part1()
		{
			var calorieEntries = SnackCalorieCounter.GetCalorieCounts();
			var sum = calorieEntries.Max();

			Console.WriteLine($"day 01 - part 1: {sum}");
		}

		public static void SolveDay01Part2()
		{
			var calorieEntries = SnackCalorieCounter.GetCalorieCounts();
			var sum = calorieEntries
				.OrderByDescending(v => v)
				.Take(3)
				.Sum();

			Console.WriteLine($"day 01 - part 2: {sum}");
		}
		#endregion Day01

		#region Day02
		public static void SolveDay02Part1()
		{
			var rounds = File.ReadLines("Inputs\\rps_input.txt");
			var scoreTotal = 0;
			foreach (var round in rounds)
			{
				var hands = round.Split(" ");
				if (hands.Length != 2) throw new InvalidOperationException();

				var oppHand = hands[0].ToRps();
				var myHand = hands[1].ToRps();
				scoreTotal += RpsCalculator.GetScoreForRound(oppHand, myHand);
			}

			Console.WriteLine($"day 02 - part 1: {scoreTotal}");
		}

		public static void SolveDay02Part2()
		{
			var rounds = File.ReadLines("Inputs\\rps_input.txt");
			var scoreTotal = 0;
			foreach (var round in rounds)
			{
				var hands = round.Split(" ");
				if (hands.Length != 2) throw new InvalidOperationException();

				var oppHand = hands[0].ToRps();
				var myHand = RpsCalculator.GetInstructedHand(oppHand, hands[1]);
				scoreTotal += RpsCalculator.GetScoreForRound(oppHand, myHand);
			}

			Console.WriteLine($"day 02 - part 2: {scoreTotal}");
		}
		#endregion Day02

		#region Day03
		public static void SolveDay03Part1()
		{
			var sacks = File.ReadLines("Inputs\\rucksack_input.txt");
			var totalPriority = sacks
				.Select(RucksackSearcher.SplitSack)
				.Select(c => RucksackSearcher.FindCommonItem(c.first, c.second))
				.Sum(RucksackSearcher.GetPriority);

			Console.WriteLine($"day 03 - part 1: {totalPriority}");
		}

		public static void SolveDay03Part2()
		{
			var sacks = File.ReadLines("Inputs\\rucksack_input.txt");
			var totalPriority = sacks.Select((x, i) => new { contents = x, index = i })
					.GroupBy(x => x.index / 3)
					.Select(g => g.Select(a => a.contents).ToArray())
					.Select(RucksackSearcher.FindCommonItem)
					.Select(RucksackSearcher.GetPriority)
					.Sum();

			Console.WriteLine($"day 03 - part 2: {totalPriority}");
		}
		#endregion Day03

		#region Day04
		public static void SolveDay04Part1()
		{
			var sectionAssignments = File.ReadLines("Inputs\\sections_input.txt");
			var fullContainedCount = sectionAssignments
				.Select(SectionParser.IsPairFullyContained)
				.Count(x => x);

			Console.WriteLine($"day 04 - part 1: {fullContainedCount}");
		}

		public static void SolveDay04Part2()
		{
			var sectionAssignments = File.ReadLines("Inputs\\sections_input.txt");
			var overlappingCount = sectionAssignments
				.Select(SectionParser.IsPairOverlapping)
				.Count(x => x);

			Console.WriteLine($"day 04 - part 2: {overlappingCount}");
		}
		#endregion Day04

		#region Day05
		public static void SolveDay05Part1()
		{
			var stacks = CrateStacker.GetInitialStacks();
			var instructions = CrateStacker.GetInstructions();

			foreach (var instruction in instructions)
			{
				CrateStacker.DoInstruction9000(stacks, instruction);
			}
			var tops = string.Join("", stacks.Select(s => s.Count == 0 ? " " : s.Peek().ToString()));

			Console.WriteLine($"day 05 - part 1: {tops}");
		}

		public static void SolveDay05Part2()
		{
			var stacks = CrateStacker.GetInitialStacks();
			var instructions = CrateStacker.GetInstructions();

			foreach (var instruction in instructions)
			{
				CrateStacker.DoInstruction9001(stacks, instruction);
			}
			var tops = string.Join("", stacks.Select(s => s.Count == 0 ? " " : s.Peek().ToString()));

			Console.WriteLine($"day 05 - part 2: {tops}");
		}
		#endregion Day05

		#region Day06
		public static void SolveDay06Part1()
		{
			var signal = File.ReadAllText("Inputs\\signalmarker_input.txt");
			var markerPos = SignalProcessor.GetFirstMarkerPosition(signal, 4);

			Console.WriteLine($"day 06 - part 1: {markerPos}");
		}

		public static void SolveDay06Part2()
		{
			var signal = File.ReadAllText("Inputs\\signalmarker_input.txt");
			var markerPos = SignalProcessor.GetFirstMarkerPosition(signal, 14);

			Console.WriteLine($"day 06 - part 2: {markerPos}");
		}
		#endregion Day06

		#region Day07
		public static void SolveDay07Part1()
		{
			var fs = FileBrowseParser.GetFileSystem();
			var folders = FileBrowseParser.FindFolders(fs, f => f.Size <= 100000);

			Console.WriteLine($"day 07 - part 1: {folders.Sum(f => f.Size)}");
		}

		public static void SolveDay07Part2()
		{
			var fs = FileBrowseParser.GetFileSystem();
			var totalUsed = fs.RootFolder.Size;
			var unusedSpace = 70000000 - totalUsed;
			var neededSpace = 30000000 - unusedSpace;
			var folder = FileBrowseParser.FindFolders(fs, f => f.Size >= neededSpace).OrderBy(f => f.Size).First();

			Console.WriteLine($"day 07 - part 2: {folder.Name} - {folder.Size}");
		}
		#endregion Day07
	}
}
