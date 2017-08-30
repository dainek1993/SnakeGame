using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace SnakeGame
{
    class ConcreteGameEngine : AbstractGAmeEngine
    {
        public ConcreteGameEngine(ConsoleGraphics cg)
            :base(cg)
        {
            AddObj(new MenuBuilder(cg));
        }
    }
}
