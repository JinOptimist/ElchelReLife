using Assets.LifeLogic.Genes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.LifeLogic
{
    class CellBuilder
    {
        public static Action<Cell> AfterCellBurn;
        public static Cell CellBurnChild(Cell parent)
        {
            var nearCells = NearCells(parent);
            if (nearCells.Count >= 8)
            {
                // There are no any empty slot.
                // Build cell imposible
                return null;
            }

            var child = Clone(parent);

            God.Cells.Add(child);

            MoveToEmptySlot(child, nearCells);

            child.StoreEnegry = parent.StoreEnegry / 2;
            parent.StoreEnegry /= 2;

            AfterCellBurn?.Invoke(child);
            return child;
        }

        public static Cell GetDefaultCell()
        {
            var cell = new Cell();
            cell.X = 0;
            cell.Y = 0;
            cell.LifePoint = 10;
            cell.StoreEnegry = 1;
            cell.Genome = new List<AbstractGen>();
            cell.Genome.Add(new Photosynthesis(cell));
            cell.Genome.Add(new Photosynthesis(cell));
            cell.Genome.Add(new Photosynthesis(cell));
            cell.Genome.Add(new Photosynthesis(cell));
            cell.Genome.Add(new Photosynthesis(cell));
            cell.Genome.Add(new Burn(cell));
            return cell;
        }

        private static Cell Clone(Cell parent)
        {
            var child = new Cell();
            child.X = parent.X;
            child.Y = parent.Y;
            child.LifePoint = 10;
            child.StoreEnegry = 1;
            child.Genome = new List<AbstractGen>();
            foreach (var gen in parent.Genome)
            {
                if (gen is Burn)
                {
                    child.Genome.Add(new Burn(child));
                }
                else if (gen is Photosynthesis)
                {
                    child.Genome.Add(new Photosynthesis(child));
                }
            }

            return child;
        }

        private static List<Cell> NearCells(Cell cell)
        {
            return God.Cells.Where(rndCell => 
                Math.Abs(rndCell.X - cell.X) <= 1
                && Math.Abs(rndCell.Y - cell.Y) <= 1).ToList();
        }

        private static void MoveToEmptySlot(Cell cell, List<Cell> nearCells)
        {
            var rnd = new Random();
            var findPlace = false;
            var dir = 0;
            while (!findPlace)
            {
                // 1 2 3
                // 8 x 4
                // 7 6 5
                dir = rnd.Next(1, 8);
                switch (dir)
                {
                    case 1:
                        if (!nearCells.Any(x=>x.X == cell.X - 1 && x.Y == cell.Y - 1))
                        {
                            cell.X -= 1;
                            cell.Y -= 1;
                            findPlace = true;
                        }
                        break;
                    case 2:
                        if (!nearCells.Any(x => x.X == cell.X && x.Y == cell.Y - 1))
                        {
                            cell.Y -= 1;
                            findPlace = true;
                        }
                        break;
                    case 3:
                        if (!nearCells.Any(x => x.X == cell.X + 1 && x.Y == cell.Y - 1))
                        {
                            cell.X += 1;
                            cell.Y -= 1;
                            findPlace = true;
                        }
                        break;
                    case 4:
                        if (!nearCells.Any(x => x.X == cell.X + 1 && x.Y == cell.Y))
                        {
                            cell.X += 1;
                            findPlace = true;
                        }
                        break;
                    case 5:
                        if (!nearCells.Any(x => x.X == cell.X + 1 && x.Y == cell.Y + 1))
                        {
                            cell.X += 1;
                            cell.Y += 1;
                            findPlace = true;
                        }
                        break;
                    case 6:
                        if (!nearCells.Any(x => x.X == cell.X && x.Y == cell.Y + 1))
                        {
                            cell.Y += 1;
                            findPlace = true;
                        }
                        break;
                    case 7:
                        if (!nearCells.Any(x => x.X == cell.X - 1 && x.Y == cell.Y + 1))
                        {
                            cell.X -= 1;
                            cell.Y += 1;
                            findPlace = true;
                        }
                        break;
                    case 8:
                        if (!nearCells.Any(x => x.X == cell.X - 1 && x.Y == cell.Y))
                        {
                            cell.X -= 1;
                            findPlace = true;
                        }
                        break;
                }
            }

            if (God.Cells.Any(x => x.X == cell.X && x.Y == cell.Y && x != cell))
            {
                var bad = 1;
            }
        }
    }
}
