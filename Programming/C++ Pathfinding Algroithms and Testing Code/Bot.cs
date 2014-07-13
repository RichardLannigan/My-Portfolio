using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pathfinder
{
    class Bot : AiBotBase
    {
        Coord2 newPos;

        public Bot(int x, int y)
            : base(x, y)
        {
            newPos = GridPosition;
        }

        protected override void ChooseNextGridLocation(Level level, Player plr)
        {
            
        }
    }
}
