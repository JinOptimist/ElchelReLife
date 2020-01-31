namespace Assets.LifeLogic.Genes
{
    public class Photosynthesis : AbstractGen
    {
        public Photosynthesis(Cell cell) : base(cell)
        {
        }

        public override void Do()
        {
            Cell.StoreEnegry += 1;
        }
    }
}
