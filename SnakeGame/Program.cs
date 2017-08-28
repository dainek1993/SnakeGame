using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;
using System.Threading;
using System.IO;

namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.CursorVisible = false;
            ConsoleGraphics cg = new ConsoleGraphics();
            MenuBuilder mb = new MenuBuilder(cg);
            mb.MenuBegin();
        }
    }
}
