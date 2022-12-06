using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022.Days
{
	public static class RucksackSearcher
	{
		public static (string first, string second) SplitSack(string rucksack)
		{
			return (rucksack.Substring(0, rucksack.Length / 2), rucksack.Substring(rucksack.Length / 2));
		}

		public static char FindCommonItem(params string[] haystacks)
		{
			var hashedItems = haystacks.Select(h => new HashSet<char>(h.ToCharArray().Distinct()));
			var seed = hashedItems.First();
			var commonItems = hashedItems.Skip(1)
				.Aggregate(seed,
					(common, next) => new HashSet<char>(common.Intersect(next)),
					set => set);

			return commonItems.First();
		}

		public static int GetPriority(char item)
		{
			return char.IsUpper(item) ? item - 'A' + 27 : item - 'a' + 1;
		}
	}
}
