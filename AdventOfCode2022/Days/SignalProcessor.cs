using System.Linq;

namespace AdventOfCode2022.Days
{
	public static class SignalProcessor
	{
		public static int GetFirstMarkerPosition(string signal, int markerCount)
		{
			for (int i = 0; i < signal.Length - markerCount + 1; i++)
			{
				var uniqueChars = signal.Substring(i, markerCount).Distinct().ToArray();
				if (uniqueChars.Length == markerCount) return i + markerCount;
			}
			return -1;
		}
	}
}
