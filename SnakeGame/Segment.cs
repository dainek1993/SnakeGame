using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    enum Direction { Up = 1, Down, Left, Right}

    class Segment : GameObject
    {
        public Direction Direct { get; set; }
        public Segment(int x, int y)
            : base(x, y)
        {}

        public void SetNewCoord(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public void ReverseDirection()
        {
            switch (this.Direct)
            {
                case Direction.Up:
                    Direct = Direction.Down;
                    break;
                case Direction.Down:
                    Direct = Direction.Up;
                    break;
                case Direction.Left:
                    Direct = Direction.Right;
                    break;
                case Direction.Right:
                    Direct = Direction.Left;
                    break;
            }
        }
    }
}
