using System;

namespace AdventOfCode2022.Days
{
	public static class RpsCalculator
	{
		public enum RPS { Rock = 0, Paper = 1, Scisssors = 2 }

		public static int GetScoreForRound(string roundTxt)
		{
			var hands = roundTxt.Split(" ");
			if (hands.Length != 2) throw new InvalidOperationException();

			var oppHand = hands[0].ToRps();
			var myHand = GetInstructedHand(oppHand, hands[1]);
			return GetScoreForWinDrawLose(oppHand, myHand) + ((int)myHand + 1);
		}

		public static int GetScoreForRound(RPS oppHand, RPS myHand)
		{
			return GetScoreForWinDrawLose(oppHand, myHand) + (int)myHand + 1;
		}

		public static int GetScoreForWinDrawLose(RPS opponentHand, RPS myHand)
		{
			if (opponentHand == myHand) return 3;
			if (myHand == GetInstructedHand(opponentHand, "z")) return 6;
			return 0;
		}

		public static RPS GetInstructedHand(RPS opponentHand, string instruction)
		{
			switch (instruction.ToLower())
			{
				case "x": return (RPS)((((int)opponentHand - 1) % 3 + 3) % 3);
				case "y": return opponentHand;
				case "z": return (RPS)((((int)opponentHand + 1) % 3 + 3) % 3);
				default: throw new InvalidOperationException();
			}
		}

		public static RPS ToRps(this string rpsText)
		{
			switch (rpsText.ToLower())
			{
				case "a":
				case "x":
					return RPS.Rock;
				case "b":
				case "y":
					return RPS.Paper;
				case "c":
				case "z":
					return RPS.Scisssors;
				default:
					throw new InvalidOperationException();
			}
		}
	}
}
