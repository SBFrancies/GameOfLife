namespace GameOfLife.Data
{
    public class GameBoard
    {
        private readonly int _startingCells;

        private readonly Random _random = new();

        public GameBoard(int maxX, int maxY, int startingCells = 0)
        {
            _startingCells = startingCells;

            MaxX = maxX;
            MaxY = maxY;
            Board = new bool[maxX * maxY];
        }

        public int MaxX { get; }
        public int MaxY { get; }
        public bool[] Board { get; private set; }

        public void InitialiseBoard()
        {
            var startingCells = new HashSet<int>();

            while (startingCells.Count < _startingCells)
            {
                var cell = _random.Next(0, Board.Length);
                startingCells.Add(cell);
            }

            foreach (var cell in startingCells)
            {
                Board[cell] = true;
            }
        }

        public void ToggleCell(int index)
        {
            Board[index] = !Board[index];
        }

        public void AdvanceGeneration()
        {
            var newBoard = new bool[Board.Length];

            for (var i = 0; i < Board.Length; i++)
            {
                var neighbours = Neighbours(i);

                newBoard[i] = neighbours switch
                {
                    < 2 => false,
                    > 3 => false,
                    3 => true,
                    _ => Board[i]
                };
            }

            Board = newBoard;
        }

        private int Neighbours(int index)
        {
            var x = index % MaxX;
            var y = index / MaxX;

            var neighbours = new List<int>
            {
                GetIndexFromCoords(x - 1, y - 1),
                GetIndexFromCoords(x, y - 1),
                GetIndexFromCoords(x + 1, y - 1),
                GetIndexFromCoords(x - 1, y),
                GetIndexFromCoords(x + 1, y),
                GetIndexFromCoords(x - 1, y + 1),
                GetIndexFromCoords(x, y + 1),
                GetIndexFromCoords(x + 1, y + 1)
            };

            return neighbours.Count(a => a >= 0 && Board[a]);
        }

        private int GetIndexFromCoords(int x, int y)
        {
            if(x < 0 || y < 0 || x >= MaxX || y >= MaxY)
            {
                return -1;
            }

            return (y * MaxX) + x;
        }
    }
}
