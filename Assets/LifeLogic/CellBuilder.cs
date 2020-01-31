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
        public static double ChanceOfMutation = 0.2;

        public static Random _random = new Random();
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
            cell.StoreEnegry = 10;
            cell.Genome = new List<AbstractGen>();
            cell.Genome.Add(new Photosynthesis(cell));
            
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
                if (gen is Burn)
                {
                    child.Genome.Add(new Burn(child));
                }
                else if (gen is Photosynthesis)
                {
                    child.Genome.Add(new Photosynthesis(child));
                }
                else if (gen is Move)
                {
                    var move = new Move(child);
                    move.Direction = (gen as Move).Direction;
                    child.Genome.Add(move);
                }
            }

            return child;
        }

        public static bool Mutate(Cell cell)
        {
            if (_random.NextDouble() < ChanceOfMutation)
            {
                return false;
            }

            var gen = GetRandomGen(cell);
            if (gen != null)
            {
                cell.Genome.Add(gen);
            }

            return true;
        }

        private static AbstractGen GetRandomGen(Cell cell)
        {
            //Chance of drop gen
            var rndNumber = _random.Next(1, 4);
            switch (rndNumber)
            {
                case 1:
                    return new Photosynthesis(cell);
                case 2:
                    var move = new Move(cell);
                    move.Direction = (Direction)_random.Next(1, 8);
                    return move;
                case 3:
                    return new Burn(cell);
            }
            
            return null;
        }

        private static List<Cell> NearCells(Cell cell)
        {
            return God.Cells.Where(rndCell =>
                Math.Abs(rndCell.X - cell.X) <= 1
                && Math.Abs(rndCell.Y - cell.Y) <= 1).ToList();
        }

        private static void MoveToEmptySlot(Cell cell, List<Cell> nearCells)
        {
            var findPlace = false;
            while (!findPlace)
            {
                // 1 2 3
                // 8 x 4
                // 7 6 5
                var direction = (Direction)_random.Next(1, 8);
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
    }
}
