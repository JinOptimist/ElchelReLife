using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.LifeLogic.Genes.Move
{
    public class MoveRight : AbstractMove
    {
        public MoveRight(Cell cell) : base(cell)
        {
        }

        public override Direction GetDirection()
        {
            return Direction.Right;
        }
    }
}
