using Assets.LifeLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.LifeLogic
{
    /// <summary>
    /// Know about all cells in the world
    /// </summary>
    class God
    {
        /// <summary>
        /// All cells must be registered here
        /// </summary>
        public static List<Cell> Cells { get; set; } = new List<Cell>();
    }
}
