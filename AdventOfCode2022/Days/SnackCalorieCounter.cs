using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2022.Days
{
	public static class SnackCalorieCounter
	{
		public static List<int> GetCalorieCounts()
		{
			var lines = File.ReadLines("Inputs\\calorie_input.txt");
			var currentCalorieTotal = 0;
			var calorieEntries = new List<int>();

			foreach (var calorieTxt in lines)
			{
				if (!int.TryParse(calorieTxt, out int calorie))
				{
					calorieEntries.Add(currentCalorieTotal);
					currentCalorieTotal = 0;
				}
				else
				{
					currentCalorieTotal += calorie;
				}
			}

			return calorieEntries;
		}
	}
}
