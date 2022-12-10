using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2022.Days
{
	public static class DisplayProgramCpu
	{
		public class Cpu
		{
			private IEnumerable<string> _program;

			public int Register { get; private set; } = 1;
			public int CycleCount { get; private set; } = 0;

			public void ExecuteInstruction(string instruction)
			{
				CycleCount += GetInstructionCycleCount(instruction);

				if (instruction.StartsWith("addx"))
				{
					var tokens = instruction.Split(" ");
					Register += int.Parse(tokens[1]);
				}
			}

			public int GetInstructionCycleCount(string instruction)
			{
				if (instruction.StartsWith("addx")) return 2;
				if (instruction.StartsWith("noop")) return 1;
				throw new InvalidOperationException();
			}

			public void SubmitProgram(IEnumerable<string> lines)
			{
				_program = lines.ToList();
				CycleCount = 0;
				Register = 1;
			}

			public IEnumerator<(int register, int cycle)> GetEnumerator()
			{
				foreach (var line in _program)
				{
					if (line.StartsWith("addx"))
					{
						CycleCount++;
						yield return (Register, CycleCount);
						CycleCount++;
						yield return (Register, CycleCount);
						var tokens = line.Split(" ");
						Register += int.Parse(tokens[1]);
					}
					else if (line.StartsWith("noop"))
					{
						CycleCount++;
						yield return (Register, CycleCount);
					}
				}
			}
		}

		public static List<List<bool>> DrawScreen(Cpu cpu)
		{
			List<List<bool>> display = new List<List<bool>>();

			var screenWidth = 40;
			display.Add(new List<bool>());
			display[0].Add(true);
			foreach (var state in cpu)
			{
				var row = state.cycle / screenWidth;
				if (display.Count <= row) display.Add(new List<bool>());
				display[row].Add((state.cycle % screenWidth) >= state.register && (state.cycle % screenWidth) <= (state.register + 2));
			}

			return display;
		}
	}
}
