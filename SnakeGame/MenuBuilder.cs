using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;
using System.IO;

namespace SnakeGame
{
    class MenuBuilder
    {

        public MenuBuilder(ConsoleGraphics cg)
        {
            this.cg = cg;
            List<MenuItem> menu = new List<MenuItem>();
            startItem = new MenuItem(10, 10, 100, 16, "Start");
            items.Add(startItem);
            loadItem = new MenuItem(10, 30, 100, 16, "Load");
            items.Add(loadItem);
            exitItem = new MenuItem(10, 50, 100, 16, "Exit");
            items.Add(exitItem);

            startItem.Click += StartItem_Click;
            loadItem.Click += LoadItem_Click;
            exitItem.Click += ExitItem_Click;
        }

        private ConsoleGraphics cg;

        private void ExitItem_Click()
        {      
            isExit = true;
        }

        private void LoadItem_Click()
        {
            int lvl = 10;
            cg.FillRectangle(0xFF000000, 0, 0, 200, 200);
            if (File.Exists("save.txt"))
            {
                lvl = int.Parse(File.ReadAllText("save.txt"));
            }
            if (lvl > 9 && lvl < 50)
            {
                GameEngine ge = new GameEngine(cg, lvl);
                ge.Start();
            }
        }

        private void StartItem_Click()
        {
            cg.FillRectangle(0xFF000000, 0, 0, 200, 200);
            GameEngine ge = new GameEngine(cg, 10);
            ge.Start();
        }

        MenuItem startItem, loadItem, exitItem;
        List<MenuItem> items = new List<MenuItem>();
        private bool isExit = false;

        public void MenuBegin()
        {
            while (true)
            {
                if (!isExit)
                {
                    foreach (MenuItem item in items)
                    {
                        item.Update();
                    }
                    foreach (MenuItem item in items)
                    {
                        item.Draw(cg);
                    }
                    cg.FlipPages();
                }
                else
                    return;
            }
        }


    }
}
