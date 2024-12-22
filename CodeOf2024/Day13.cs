using System.Drawing;

namespace AoC2024;

public class Day13
{
    private char[,] Field;
    private int MaxY;
    private readonly int MaxX;

    private record Button(long y, long x, int cost)
    {
        public float MovePerCoint => ((float)y + (float)x) / cost;
        public long TotalMovement => y + x;
    }

    private class ClawMachine
    {
        // A cost 3
        // public (long y, long x, int cost) ButtonADiff;
        public Button ButtonADiff;

        // B cost 1
        // public (long y, long x, int cost) ButtonBDiff;
        public Button ButtonBDiff;

        public (long y, long x) PrizePosition;

        // public long Amovement => ButtonADiff.y + ButtonADiff.x;
        // public long Bmovement => ButtonBDiff.y + ButtonBDiff.x;


        public ClawMachine(string fButton, string sButton, string prize, long adding)
        {
            // var fbutton = fButton[7];
            var adx = fButton.Split("X+", StringSplitOptions.RemoveEmptyEntries)[1].Split(",")[0];
            var ady = fButton.Split("Y+", StringSplitOptions.RemoveEmptyEntries)[1].Split(",")[0];

            // var sbutton = sButton[7];
            var bdx = sButton.Split("X+", StringSplitOptions.RemoveEmptyEntries)[1].Split(",")[0];
            var bdy = sButton.Split("Y+", StringSplitOptions.RemoveEmptyEntries)[1].Split(",")[0];

            var targetX = prize.Split("X=", StringSplitOptions.RemoveEmptyEntries)[1].Split(",")[0];
            var targetY = prize.Split("Y=", StringSplitOptions.RemoveEmptyEntries)[1].Split(",")[0];

            ButtonADiff = new Button(long.Parse(ady), long.Parse(adx), 3);
            ButtonBDiff = new Button(long.Parse(bdy), long.Parse(bdx), 1);

            PrizePosition = (adding + long.Parse(targetY), adding + long.Parse(targetX));
        }
        // private record Button(char name, long dy, long dx);
    }

    private List<ClawMachine> ClawMachines = [];

    public Day13(List<string> allData, long adding = 0)
    {
        for (int r = 0; r+3 <= allData.Count; r+=4)
        {
            var clawMachine = new ClawMachine(
                allData[r],
                allData[r + 1],
                allData[r + 2],
                adding
            );
            ClawMachines.Add(clawMachine);
        }
    }



    public long Part1()
    {
        var sum = 0L;

        foreach (var clawMachine in ClawMachines)
        {
            sum += WinzWithAtMostPresses(clawMachine, 100L);
        }

        return sum;
    }

    private long WinzWithAtMostPresses(ClawMachine clawMachine, long maxButtonPresses)
    {
        var buttADiff = clawMachine.ButtonADiff;
        var buttBDiff = clawMachine.ButtonBDiff;

        var aMovementPerCoint = clawMachine.ButtonADiff.MovePerCoint;
        var bMovementPerCoint = clawMachine.ButtonBDiff.MovePerCoint;

        if (aMovementPerCoint > bMovementPerCoint)
        {
            var maxPressForY =  clawMachine.PrizePosition.y/buttADiff.y ;
            var maxPressForX =  clawMachine.PrizePosition.x/buttADiff.x ;

            var l = new List<long> { maxPressForY, maxPressForX, maxButtonPresses };

            var aStartPush = l.Min();

            // var aPush = aStartPush;
            //
            // while(aPush >= 0)
            // {
            //     (long y, long x) distLeft = (clawMachine.PrizePosition.y - buttADiff.y * aPush,
            //         clawMachine.PrizePosition.x - buttADiff.x * aPush);
            //
            //     if (distLeft.y % buttBDiff.y == 0)
            //     {
            //         var bPushes = distLeft.y / buttBDiff.y;
            //         if (bPushes <= 100 &&
            //             distLeft.x == bPushes * buttBDiff.x)
            //         {
            //             var totalCost = aPush *3 + bPushes *1;
            //             return totalCost;
            //         }
            //     }
            // }

            for (var aPush = aStartPush; aPush >=0; aPush--)
            {
                (long y, long x) distLeft = (clawMachine.PrizePosition.y - buttADiff.y * aPush,
                    clawMachine.PrizePosition.x - buttADiff.x * aPush);

                if (distLeft.y % buttBDiff.y == 0)
                {
                    var bPushes = distLeft.y / buttBDiff.y;
                    if (bPushes <= 100 &&
                        distLeft.x == bPushes * buttBDiff.x)
                    {
                        var totalCost = aPush *3 + bPushes *1;
                        return totalCost;
                    }
                }
            }
        }
        else
        {
            var maxPressForY =   clawMachine.PrizePosition.y/buttBDiff.y;
            var maxPressForX =   clawMachine.PrizePosition.x/buttBDiff.x;

            var l = new List<long> { maxPressForY, maxPressForX, maxButtonPresses };

            var bStartPush = l.Min();

            for (var bPushes = bStartPush; bPushes >=0; bPushes--)
            {
                (long y, long x) distLeft = (clawMachine.PrizePosition.y - buttBDiff.y * bPushes,
                    clawMachine.PrizePosition.x - buttBDiff.x * bPushes);

                if (distLeft.y % buttADiff.y == 0)
                {
                    var aPushes = distLeft.y / buttADiff.y;
                    if (aPushes <= 100 &&
                        distLeft.x == aPushes * buttADiff.x)
                    {
                        var totalCost = bPushes *1 + aPushes *3;
                        return totalCost;
                    }
                }
            }
        }

        var longestMovingButton =
                clawMachine.ButtonADiff.TotalMovement > clawMachine.ButtonBDiff.TotalMovement ?
                    clawMachine.ButtonADiff : clawMachine.ButtonBDiff;

        var otherClamMovement =
            clawMachine.ButtonADiff.TotalMovement <= clawMachine.ButtonBDiff.TotalMovement ?
                clawMachine.ButtonADiff : clawMachine.ButtonBDiff;

        // Get the most movement per coin
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

        foreach (var clawMachine in ClawMachines)
        {
            sum += WinzWithAtMostPresses(clawMachine, long.MaxValue);
        }

        return sum;
    }

    private long MoveForPart2(ClawMachine clawMachine)
    {
        return 5;
    }
}

