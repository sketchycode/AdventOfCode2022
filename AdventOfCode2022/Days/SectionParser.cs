namespace AdventOfCode2022.Days
{
	public static class SectionParser
	{
		public struct Section
		{
			public int Start { get; set; }
			public int End { get; set; }

			public bool DoesFullyContain(Section section)
			{
				return Start <= section.Start && End >= section.End;
			}

			public bool DoesOverlap(Section section)
			{
				return (Start <= section.Start && End >= section.Start) || (Start <= section.End && End >= section.End);
			}
		}

		public static Section ToSection(this string sectionText)
		{
			var ends = sectionText.Split("-");
			return new Section() { Start = int.Parse(ends[0]), End = int.Parse(ends[1]) };
		}

		public static bool IsPairFullyContained(string sectionPair)
		{
			var sections = sectionPair.Split(",");
			var firstSection = sections[0].ToSection();
			var secondSection = sections[1].ToSection();

			return firstSection.DoesFullyContain(secondSection) || secondSection.DoesFullyContain(firstSection);
		}

		public static bool IsPairOverlapping(string sectionPair)
		{
			var sections = sectionPair.Split(",");
			var firstSection = sections[0].ToSection();
			var secondSection = sections[1].ToSection();

			return firstSection.DoesOverlap(secondSection) || secondSection.DoesOverlap(firstSection);
		}
	}
}
