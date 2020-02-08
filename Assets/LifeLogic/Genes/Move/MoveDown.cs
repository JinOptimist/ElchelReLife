namespace Assets.LifeLogic.Genes.Move
{
    public class MoveDown : AbstractMove
    {
        public MoveDown(Cell cell) : base(cell)
        {
        }

        public override Direction GetDirection()
        {
            return Direction.Down;
        }
    }
}
