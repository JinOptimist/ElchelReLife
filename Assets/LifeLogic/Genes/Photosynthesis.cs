namespace Assets.LifeLogic.Genes
{
    public class Photosynthesis : AbstractGen
    {
        public Photosynthesis(Cell cell) : base(cell)
        {
        }

        public override void Do()
        {
            if (Cell.StoreEnegry < Cell.MaxStoreEnegry)
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
