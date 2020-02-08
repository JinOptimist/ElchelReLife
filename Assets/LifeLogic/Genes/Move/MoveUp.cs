using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.LifeLogic.Genes.Move
{
    public class MoveUp : AbstractMove
    {
        public MoveUp(Cell cell) : base(cell)
        {
        }

        public override Direction GetDirection()
        {
            return Direction.Up;
        }
    }
}
