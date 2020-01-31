namespace Assets.LifeLogic.Genes
{
    public class Photosynthesis : AbstractGen
    {
        public int MaxEnergy = 10;
        public Photosynthesis(Cell cell) : base(cell)
        {
        }

        public override void Do()
        {
            if (Cell.StoreEnegry < MaxEnergy)
            {
                Cell.StoreEnegry += 1;
            }
        }

        public override string ToString()
        {
            return $"P";
        }
    }
}
