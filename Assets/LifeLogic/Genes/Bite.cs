using System;
using System.Linq;

namespace Assets.LifeLogic.Genes
{
    public class Bite : AbstractGen
    {
        public int BiteEnergyCost { get; set; } = 1;
        public int BiteEnergyProfit { get; set; } = 3;

        private Random _random = new Random();

        public Bite(Cell cell) : base(cell)
        {
        }

        public override void Do()
        {
            if (Cell.StoreEnegry > BiteEnergyCost)
            {
                var cells = CellBuilder.NearCells(Cell);
                //TODO
                if (!cells.Any())
                {
                    return;
                }

                var index = _random.Next(0, cells.Count());
                var enemy = cells[index];
                enemy.LifePoint -= 3;
                Cell.StoreEnegry += BiteEnergyProfit;
            }
        }

        public override string ToString()
        {
            return $"Bit";
        }
    }
}
