using System.Linq;

namespace Assets.LifeLogic.Genes
{
    public class Move : AbstractGen
    {
        public int MoveEnergy = 1;
        public Direction Direction { get; set; }
        public Move(Cell cell) : base(cell)
        {
        }

        public override void Do()
        {
            if (Cell.StoreEnegry > MoveEnergy)
            {
                Cell.StoreEnegry -= MoveEnergy;
                CellBuilder.TryToMove(Cell, Direction);
            }
        }

        public override string ToString()
        {
            return $"M:{(int)Direction}";
        }
    }

    // 1 2 3
    // 8 x 4
    // 7 6 5
    public enum Direction
    {
        UpLeft = 1,
        Up = 2,
        UpRight = 3,
        Right = 4,
        DownRight = 5,
        Down = 6,
        DownLeft = 7,
        Left = 8
    }
}
