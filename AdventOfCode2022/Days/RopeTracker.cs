using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2022.Days
{
	public static class RopeTracker
	{
		public class Rope
		{
			private int _headX = 0;
			private int _headY = 0;
			private int _tailX = 0;
			private int _tailY = 0;

			private readonly HashSet<string> _visitedCoords = new HashSet<string>();
			private readonly List<Rope> _additionalSegments = new List<Rope>();

			public int VisitedCoordsCount => _additionalSegments.Count == 0 ? _visitedCoords.Count : _additionalSegments.Last().VisitedCoordsCount;

			public Rope(int segmentCount = 1)
			{
				_visitedCoords.Add(GetTailCoordId());

				for (int i = 1; i < segmentCount; i++)
				{
					_additionalSegments.Add(new Rope());
				}
			}

			public void Move(string moveInstruction)
			{
				var inst = ParseInstruction(moveInstruction);
				var moveOffset = GetMoveDirectionOffset(inst.dir);

				for (int i = 0; i < inst.count; i++)
				{
					MoveHead(moveOffset);
					UpdateTail();

					var nextSegmentHeadX = _tailX;
					var nextSegmentHeadY = _tailY;
					foreach (var segment in _additionalSegments)
					{
						segment._headX = nextSegmentHeadX;
						segment._headY = nextSegmentHeadY;
						segment.UpdateTail();
						nextSegmentHeadX = segment._tailX;
						nextSegmentHeadY = segment._tailY;
					}
				}
			}

			private void MoveHead((int x, int y) move)
			{
				_headX += move.x;
				_headY += move.y;
			}

			private void UpdateTail()
			{
				var offsetX = _headX - _tailX;
				var offsetY = _headY - _tailY;

				if (Math.Abs(offsetX) <= 1 && Math.Abs(offsetY) <= 1) return;

				if (offsetX != 0) _tailX += Math.Sign(offsetX) * 1;
				if (offsetY != 0) _tailY += Math.Sign(offsetY) * 1;
				AddTailCoord();
			}

			private (int xdel, int ydel) GetMoveDirectionOffset(string dir)
			{
				switch (dir.ToLower())
				{
					case "r": return (1, 0);
					case "l": return (-1, 0);
					case "u": return (0, 1);
					case "d": return (0, -1);
					default: throw new InvalidOperationException();
				}
			}

			private (string dir, int count) ParseInstruction(string moveInstruction)
			{
				var tokens = moveInstruction.Split(" ");
				return (tokens[0], int.Parse(tokens[1]));
			}

			private void AddTailCoord()
			{
				var id = GetTailCoordId();
				if (!_visitedCoords.Contains(id)) _visitedCoords.Add(id);
			}

			private string GetTailCoordId()
			{
				return $"{_tailX},{_tailY}";
			}
		}
	}
}
