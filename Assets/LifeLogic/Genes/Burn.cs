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
            var burnCost = BurnCost + Cell.Genome.Count / 3;
            if (Cell.StoreEnegry > burnCost)
            {
                Cell.StoreEnegry -= burnCost;
                CellBuilder.CellBurnChild(Cell);
            }
            //else
            //{
            //    Cell.StoreEnegry = 0;
            //}
        }

        public override string ToString()
        {
            return "B";
        }
    }
}
