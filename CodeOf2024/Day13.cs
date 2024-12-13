using System.Drawing;

namespace AoC2024;

public class Day13
{
    private char[,] Field;
    private int MaxY;
    private readonly int MaxX;

    private class ClawMachine
    {
        public (long y, long x) ButtonADiff;
        public (long y, long x) ButtonBDiff;
        public (long y, long x) PrizePosition;

        public long Amovement => ButtonADiff.y + ButtonADiff.x;
        public long Bmovement => ButtonBDiff.y + ButtonBDiff.x;

        public ClawMachine(string fButton, string sButton, string prize)
        {
            var fbutton = fButton[7];
            var fdx = fButton.Split("X+", StringSplitOptions.RemoveEmptyEntries)[1].Split(",")[0];
            var fdy = fButton.Split("Y+", StringSplitOptions.RemoveEmptyEntries)[1].Split(",")[0];

            var sbutton = sButton[7];
            var sdx = sButton.Split("X+", StringSplitOptions.RemoveEmptyEntries)[1].Split(",")[0];
            var sdy = sButton.Split("Y+", StringSplitOptions.RemoveEmptyEntries)[1].Split(",")[0];

            var targetX = prize.Split("X=", StringSplitOptions.RemoveEmptyEntries)[1].Split(",")[0];
            var targetY = prize.Split("Y=", StringSplitOptions.RemoveEmptyEntries)[1].Split(",")[0];

            ButtonADiff = (long.Parse(fdy), long.Parse(fdx));
            ButtonBDiff = (long.Parse(sdy), long.Parse(sdx));

            PrizePosition = (long.Parse(targetY), long.Parse(targetX));
        }

        private record Button(char name, long dy, long dx);
    }

    private List<ClawMachine> ClawMachines = [];

    public Day13(List<string> allData)
    {
        // MaxY = allData.Count;
        // MaxX = allData[0].Length;
        // Field = new char[MaxY, MaxX];

        for (int r = 0; r+3 < allData.Count; r+=4)
        {
            var clawMachine = new ClawMachine(
                allData[r],
                allData[r + 1],
                allData[r + 2]
            );
            ClawMachines.Add(clawMachine);
        }
    }



    public long Part1()
    {
        var sum = 0L;

        foreach (var clawMachine in ClawMachines)
        {
            sum += WinzWithAtMostPresses(clawMachine, 100);
        }

        return sum;
    }

    private long WinzWithAtMostPresses(ClawMachine clawMachine, int i)
    {

        var longestMovingButton =
                clawMachine.Amovement > clawMachine.Bmovement ?
                    clawMachine.ButtonADiff : clawMachine.ButtonBDiff;

        var otherClamMovement =
            clawMachine.Amovement <= clawMachine.Bmovement ?
                clawMachine.ButtonADiff : clawMachine.ButtonBDiff;

        // Associate the longer moving button with as many steps as it overshoots a position

        var maxMovY = longestMovingButton.y / clawMachine.PrizePosition.y;
        var maxMovx = longestMovingButton.x / clawMachine.PrizePosition.x;

        var longButtonPushMax = Math.Min(maxMovY, maxMovx);

        for (var lPush = longButtonPushMax; lPush >=0; lPush--)
        {
             (long y, long x) distLeft = (clawMachine.PrizePosition.y - longestMovingButton.y * lPush,
                clawMachine.PrizePosition.x - longestMovingButton.x * lPush);

             if (distLeft.y % otherClamMovement.y == 0)
             {
                 var otherPushes = distLeft.y / otherClamMovement.y;
                 if (distLeft.x == otherPushes * otherClamMovement.x)
                 {
                     var totalPusher = lPush + otherPushes;
                     return totalPusher <= 100 ? totalPusher : 0;
                 }
             }
        }

        return 0;
    }


    public long Part2()
    {
        var sum = 0L;


        return sum;
    }
}

