namespace AoC2025;

public class Day10
{
    private int[,] terrain;

    public Day10(List<string> allData)
    {
        terrain = new int[allData.Count, allData[0].Length];

        for (int y = 0; y < allData.Count; y++)
        {
            for (int x = 0; x < allData[0].Length; x++)
            {
                terrain[y,x] = int.Parse(allData[y][x].ToString());
            }
        }
    }

    public int Part1()
    {
        var sum = 0;
        for (int startY = 0; startY < terrain.GetLength(0); startY++)
        {
            for (int startX = 0; startX < terrain.GetLength(1); startX++)
            {
                if (terrain[startY, startX] == 0)
                    sum += new TrainHead(startY, startX, terrain).ReachableNines();
            }
        }

        return sum;
    }

    private class TrainHead
    {
        private HashSet<(int y, int x)>[] reachablePerLength = new HashSet<(int y, int x)>[10];

        public TrainHead( int[,] terrain)
        {
            Terrain = terrain;
        }

        public TrainHead(int startY, int startX, int[,] terrain)
        {
            Terrain = terrain;
            for (int i = 0; i < 10; i++)
                reachablePerLength[i] = [];

            reachablePerLength[0].Add((startY, startX));

        }

        public int[,] Terrain { get; set; }


        IEnumerable<(int y, int x)> NextSteps((int y, int x) position)
        {
            (var y, var x) = position;
                return  new List<(int,int)>{
                (y - 1, x),
                (y + 1, x),
                (y , x -1),
                (y , x+1),
                }.Where(p => !IsOutOfBound(p))
                ;

        }

        private bool IsOutOfBound((int, int ) nextSquare)
        {
            var (y, x) = nextSquare;
            if (y < 0 || y >= Terrain.GetLength(0))
                return true;
            if (x < 0 || x >= Terrain.GetLength(1))
                return true;
            return false;
        }

        public int UniquePathToNine((int y, int x) startPosition, int onLevel)
        {
            if (Terrain[startPosition.y, startPosition.x] != onLevel)
                return 0;

            if (onLevel == 9)
                return 1;

            var sum = 0;
            foreach (var nextep in NextSteps(startPosition))
            {
                sum += UniquePathToNine(nextep, onLevel+1);
            }
            return sum;
        }




        public int ReachableNines()
        {
            for (int l = 0; l < 9; l++)
            {
                foreach (var positions in reachablePerLength[l])
                {
                    foreach (var futurPos in
                             NextSteps(positions)
                                 .Where(p => Terrain[p.y, p.x] == l + 1))
                    {
                        reachablePerLength[l + 1].Add(futurPos);
                    }
                }
            }

            return reachablePerLength[9].Count;

        }
    }

    public int Part2()
    {
        var sum = 0;
        for (int startY = 0; startY < terrain.GetLength(0); startY++)
        {
            for (int startX = 0; startX < terrain.GetLength(1); startX++)
            {
                if (terrain[startY, startX] == 0)
                    sum += new TrainHead(terrain).UniquePathToNine((startY, startX), 0);
            }
        }

        return sum;
    }

}
