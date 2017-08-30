using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;
using System.IO;

namespace SnakeGame
{
    class MenuBuilder : IGameObj
    {

        private MenuItem startItem, loadItem, exitItem;
        private List<MenuItem> items = new List<MenuItem>();
        private ConsoleGraphics cg;

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

        private void ExitItem_Click()
        {
            Environment.Exit(0);
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
                Player ge = new Player(cg, lvl);
                ge.Start();
            }
        }

        private void StartItem_Click()
        {
            cg.FillRectangle(0xFF000000, 0, 0, 200, 200);
            Player player = new Player(cg, 10);
            player.Start();
        }

        public void Render(ConsoleGraphics cg)
        {
            foreach (MenuItem item in items)
            {
                item.Draw(cg);
            }
        }

        public void Update(AbstractGAmeEngine age)
        {
            foreach (MenuItem item in items)
            {
                item.Update();
            }
        }
    }
}
