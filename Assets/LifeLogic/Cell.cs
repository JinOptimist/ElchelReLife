using Assets.LifeLogic.Genes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.LifeLogic
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int StoreEnegry { get; set; }
        public int LifePoint { get; set; }
        
        public List<AbstractGen> Genome { get; set; }

        public void Turn()
        {
            Genome.ForEach(x => x.Do());
        }

        public override string ToString()
        {
            return $"[{X},{Y}]E:{StoreEnegry}L:{LifePoint}";
        }
    }
}
