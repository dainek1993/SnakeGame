using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace SnakeGame
{
    public interface IGameObj
    {
        void Render(ConsoleGraphics cg);
        void Update(AbstractGAmeEngine age);
    }
}
