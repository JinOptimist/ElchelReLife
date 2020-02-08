using System;
using System.Linq;

namespace Assets.LifeLogic.Genes
{
    public abstract class AbstractMove : AbstractGen
    {
        public int MoveEnergy { get; set; } = 1;

        public AbstractMove(Cell cell) : base(cell)
        {
        }

        public abstract Direction GetDirection();

        public override void Do()
        {
            if (Cell.StoreEnegry > MoveEnergy)
            {
                Cell.StoreEnegry -= MoveEnergy;
                CellBuilder.TryToMove(Cell, GetDirection());
            }
        }

        public override string ToString()
        {
            return $"M:{(int)GetDirection()}";
        }
    }
}
