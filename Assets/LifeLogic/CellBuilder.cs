using Assets.LifeLogic.Genes;
using Assets.LifeLogic.Genes.Move;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.LifeLogic
{
    class CellBuilder
    {
        public static double ChanceOfMutation = 0.2;
        public static double ChanceOfLoseGen = 0.1;
        public static double ChanceOfGetNewGen = 0.2;

        public static Random _random = new Random();
        public static Action<Cell> AfterCellBurn;
        public static List<Type> ListOfGenType = new List<Type>() 
        { 
            typeof(Bite),
            typeof(Burn),
            typeof(MaxEnergyPoint),
            typeof(MoveUp),
            typeof(MoveDown),
            typeof(MoveLeft),
            typeof(MoveRight),
            typeof(Photosynthesis)
        };
        
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
            child.MaxStoreEnegry = 10 + child.Genome.OfType<MaxEnergyPoint>().Count() * 3;

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
            cell.LifePoint = 1;
            cell.StoreEnegry = 10;
            cell.Genome = new List<AbstractGen>
            {
                new Photosynthesis(cell)
            };

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
                if (Mutate(child))
                {
                    continue;
                }

                child.Genome.Add(CallConstractorByGenType(gen.GetType(), child));
            }
            if (_random.NextDouble() < ChanceOfGetNewGen)
            {
                child.Genome.Add(GetRandomGen(child));
            }

            return child;
        }

        public static bool Mutate(Cell cell)
        {
            //Check if not mutation return false immediately
            if (!(_random.NextDouble() < ChanceOfMutation))
            {
                return false;
            }

            if (_random.NextDouble() < ChanceOfLoseGen)
            {
                //lose old gen
                if (cell.Genome.Count < 2)
                {
                    return false;
                }
                
                //Min include, max not.
                var index = _random.Next(0, cell.Genome.Count);
                cell.Genome.RemoveAt(index);
            }
            else
            {
                //Add new gen
                var gen = GetRandomGen(cell);
                if (gen == null)
                {
                    return false;
                }

                cell.Genome.Add(gen);
            }

            return true;
        }

        private static AbstractGen GetRandomGen(Cell cell)
        {
            //Chance of drop gen
            var index = _random.Next(0, ListOfGenType.Count());//Min include, max not.
            return CallConstractorByGenType(ListOfGenType[index], cell);
        }

        public static List<Cell> NearCells(Cell centerCell)
        {
            return God.Cells.Where(someCell =>
                Math.Abs(someCell.X - centerCell.X) <= 1
                && Math.Abs(someCell.Y - centerCell.Y) <= 1
                && someCell != centerCell).ToList();
        }

        private static void MoveToEmptySlot(Cell cell, List<Cell> nearCells)
        {
            var findPlace = false;
            while (!findPlace)
            {
                // 1 2 3
                // 8 x 4
                // 7 6 5
                var direction = (Direction)_random.Next(1, 9);//Min include, max not.
                findPlace = TryToMove(cell, direction, nearCells);
            }
        }

        public static bool TryToMove(Cell cell, Direction direction, List<Cell> nearCells = null)
        {
            if (nearCells == null)
            {
                nearCells = NearCells(cell);
            }

            var findPlace = false;
            switch (direction)
            {
                case Direction.UpLeft:
                    if (!nearCells.Any(x => x.X == cell.X - 1 && x.Y == cell.Y - 1))
                    {
                        cell.X -= 1;
                        cell.Y -= 1;
                        findPlace = true;
                    }
                    break;
                case Direction.Up:
                    if (!nearCells.Any(x => x.X == cell.X && x.Y == cell.Y - 1))
                    {
                        cell.Y -= 1;
                        findPlace = true;
                    }
                    break;
                case Direction.UpRight:
                    if (!nearCells.Any(x => x.X == cell.X + 1 && x.Y == cell.Y - 1))
                    {
                        cell.X += 1;
                        cell.Y -= 1;
                        findPlace = true;
                    }
                    break;
                case Direction.Right:
                    if (!nearCells.Any(x => x.X == cell.X + 1 && x.Y == cell.Y))
                    {
                        cell.X += 1;
                        findPlace = true;
                    }
                    break;
                case Direction.DownRight:
                    if (!nearCells.Any(x => x.X == cell.X + 1 && x.Y == cell.Y + 1))
                    {
                        cell.X += 1;
                        cell.Y += 1;
                        findPlace = true;
                    }
                    break;
                case Direction.Down:
                    if (!nearCells.Any(x => x.X == cell.X && x.Y == cell.Y + 1))
                    {
                        cell.Y += 1;
                        findPlace = true;
                    }
                    break;
                case Direction.DownLeft:
                    if (!nearCells.Any(x => x.X == cell.X - 1 && x.Y == cell.Y + 1))
                    {
                        cell.X -= 1;
                        cell.Y += 1;
                        findPlace = true;
                    }
                    break;
                case Direction.Left:
                    if (!nearCells.Any(x => x.X == cell.X - 1 && x.Y == cell.Y))
                    {
                        cell.X -= 1;
                        findPlace = true;
                    }
                    break;
            }

            return findPlace;
        }
    
        private static AbstractGen CallConstractorByGenType(Type type, Cell cell)
        {
            var constr = type.GetConstructor(new Type[] { typeof(Cell) });
            return constr.Invoke(new object[] { cell }) as AbstractGen;
        }
    }
}
