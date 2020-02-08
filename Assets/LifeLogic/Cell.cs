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

        private int _storeEnegry;
        public int StoreEnegry
        {
            get
            {
                return _storeEnegry;
            }
            set
            {
                _storeEnegry = value;
                if (_storeEnegry > MaxStoreEnegry)
                {
                    _storeEnegry = MaxStoreEnegry;
                }
            }
        }

        public int MaxStoreEnegry { get; set; } = 10;

        public int BurnCost
        {
            get
            {
                return 3
                    + Genome.OfType<MaxEnergyPoint>().Count()
                    + Genome.Count() / 2;
            }
        }

        private int _lifePoint;
        public int LifePoint
        {
            get
            {
                return _lifePoint;
            }

            set
            {
                _lifePoint = value;

                if (_lifePoint < 1)
                {
                    Die();
                }
            }
        }

        public List<AbstractGen> Genome { get; set; }

        public Action AfterCellDie;

        public void Turn()
        {
            foreach (var gen in Genome)
            {
                gen.Do();
            }
            if (StoreEnegry == MaxStoreEnegry)
            {
                Burn();
            }
        }

        public void Burn()
        {
            var burnCost = BurnCost + Genome.Count / 3;
            if (StoreEnegry > burnCost)
            {
                StoreEnegry -= burnCost;
                CellBuilder.CellBurnChild(this);
            }
        }

        public void Die()
        {
            AfterCellDie?.Invoke();
        }

        public override string ToString()
        {
            var genome = string.Join(" ", Genome.Select(x => $"{x}"));
            return $"[{X},{Y}].E:{StoreEnegry}.Me:{MaxStoreEnegry}.Bc:{BurnCost}.Lp:{LifePoint}.G:{Genome.Count}.GenCode: {genome}";
        }
    }
}
