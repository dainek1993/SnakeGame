using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace SnakeGame
{
    class Food : GameObject, IDravable
    {
        public Food(int x, int y)
            : base(x, y) { }

        public void Draw(ConsoleGraphics cg)
        {
            cg.DrawRectangle(0xFF0000FF, X, Y, 30, 30, 2f);
        }

        public void SetNewPosition(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
