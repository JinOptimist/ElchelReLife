namespace Assets.LifeLogic
{
    public abstract class AbstractGen
    {
        public AbstractGen(Cell cell)
        {
            Cell = cell;
        }

        public Cell Cell { get; set; }

        public virtual void CopyGenInformation(AbstractGen gen)
        {
        }

        public virtual void Do()
        {
        }
    }
}
