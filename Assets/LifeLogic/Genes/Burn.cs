namespace Assets.LifeLogic.Genes
{
    public class Burn : AbstractGen
    {
        public int BurnCost { get; set; } = 3;

        public Burn(Cell cell) : base(cell)
        {
        }

        public override void Do()
        {
            Cell.StoreEnegry -= BurnCost;
            CellBuilder.CellBurnChild(Cell);
        }
    }
}
