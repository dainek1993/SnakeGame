using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace SnakeGame
{
    class Wall
    {
        public Wall(Random r, int x, int y, int seed)
        {
            Elements = new List<WallElement>();
            X = x;
            Y = y;

            for (int i = 1; i < 3; i++)
            {
                for (int j = 1; j < 3; j++)
                {
                    if (r.Next(16) < seed + 2)
                    {
                        Elements.Add(new WallElement(X + i * 30, Y + j * 30));
                    }
                }

            }
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public List<WallElement> Elements { get; private set; }

        public void Draw(ConsoleGraphics cg)
        {
            for (int i = 0; i < Elements.Count; i++)
            {
                Elements[i].Draw(cg);
            }
        }
    }

    class WallElement : GameObject
    {
        public WallElement(int x, int y)
            : base(x, y)
        { }

        public void Draw(ConsoleGraphics cg)
        {
            cg.DrawRectangle(0xFFA0A0A0, X, Y, 30, 30, 2f);
        }
    }
}
