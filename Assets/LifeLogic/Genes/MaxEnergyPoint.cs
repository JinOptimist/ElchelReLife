namespace Assets.LifeLogic.Genes
{
    public class MaxEnergyPoint : AbstractGen
    {
        public MaxEnergyPoint(Cell cell) : base(cell)
        {
        }

        public override string ToString()
        {
            return $"Mep";
        }
    }
}
