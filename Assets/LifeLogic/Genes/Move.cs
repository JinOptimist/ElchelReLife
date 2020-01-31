using System.Linq;

namespace Assets.LifeLogic.Genes
{
    public class Move : AbstractGen
    {
        public Direction Direction { get; set; }
        public Move(Cell cell) : base(cell)
        {
        }

        public override void Do()
        {
            if (God.Cells.Any(x => x.X == Cell.X && x.Y == Cell.Y))
            {
                CellBuilder.TryToMove(Cell, Direction);
            }
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
