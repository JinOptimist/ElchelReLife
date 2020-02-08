namespace Assets.LifeLogic.Genes
{
    public class Burn : AbstractGen
    {
        public Burn(Cell cell) : base(cell)
        {
        }

        public override void Do()
        {
            Cell.Burn();
        }

        public override string ToString()
        {
            return "B";
        }
    }
}
