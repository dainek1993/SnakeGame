using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace SnakeGame
{
    abstract class GameObject
    {
        public GameObject(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public bool IntersectWith(GameObject obj)
        {
            return ((X == obj.X) && (Y == obj.Y)) ? true : false;
        }
    }
}
