using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;


namespace SnakeGame
{
    class Control
    {
        public Control(Keys key, Direction direction)
        {
            this.key = key;
            this.direction = direction;
        }
        Keys key;
        Direction direction;
        public event EventHandler<ControlEventArg> OnClick;

        public void Check()
        {
            if (Input.IsKeyDown(key))
                OnClick?.Invoke(this, new ControlEventArg(key, direction));
        }
    }

    class ControlEventArg : EventArgs
    {
        public ControlEventArg(Keys k, Direction d)
        {
            Key = k;
            Direction = d;
        }
        public ControlEventArg(Keys k)
        {
            Key = k;
            Direction = 0;
        }

        public Keys Key { get; private set; }
        public Direction Direction{ get; private set; }
    }
}
