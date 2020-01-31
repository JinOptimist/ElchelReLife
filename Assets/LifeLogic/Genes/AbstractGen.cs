namespace Assets.LifeLogic
{
    public abstract class AbstractGen
    {
        public AbstractGen(Cell cell)
        {
            Cell = cell;
        }

        public Cell Cell { get; set; }

        public abstract void Do();
    }
}
