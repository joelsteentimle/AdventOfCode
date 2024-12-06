using System.Diagnostics;
using Microsoft.VisualBasic;

namespace AoC2024;

public class Day06
{
    public enum Place : int
    {
        Open,
        Boulder,
        Visited
    };

    private (int, int) GuardPosition = (0, 0);
    private (int,int) GuardHeading = (0, 0);
    private Place[,] Field;

    public Day06(List<string> input)
    {
        Field = new Place[input.Count,input[0].Length];

        for (int y = 0; y < input.Count; y++)
        {
            var row = input[i].ToCharArray();
            for (int x = 0; x < row.Length; x++)
            {
                switch (row[x])
                {
                    case '.':
                        Field[y, x] = Place.Open;
                        break;
                    case '#':
                        Field[y, x] = Place.Boulder;
                        break;
                    case 'X':
                        Field[y, x] = Place.Visited;
                        break;
                    case '^':
                        GuardPosition = (y, x);
                        GuardHeading = (-1, 0);
                        Field[y, x] = Place.Visited;
                        break;
                    case '>':
                        GuardPosition = (y, x);
                        GuardHeading = (0, 1);
                        Field[y, x] = Place.Visited;
                        break;
                    case 'v':
                        GuardPosition = (y, x);
                        GuardHeading = (1, 0);
                        Field[y, x] = Place.Visited;
                        break;
                    case '<':
                        GuardPosition = (y, x);
                        GuardHeading = (0, -1);
                        Field[y, x] = Place.Visited;
                        break;
                }

                // Field[y, x] = row[x] switch
                // {
                //     '.' => Place.Open,
                //     '#' => Place.Boulder,
                //     'X' => Place.Visited,
                //     '^' => {
                //     GuardPosition = (y, x);
                //     GuardHeading = (-1, 0);
                //     return Place.Visited;
                // }
                // };
            }
        }
    }

    private void MoveGuard()
    {

    }

    public int GuardVisitedSquares()
    {
        return 0;
    }
}