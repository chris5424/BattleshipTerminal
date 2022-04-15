using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship
{
    public class Ship
    {
        public int Size { get; set; }
        public int ReceivedHits { get; set; }
        public string Name { get; set; }
        public bool IsSunk { get; set; }
    }
}
