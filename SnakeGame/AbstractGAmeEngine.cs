using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace SnakeGame
{
    public abstract class AbstractGAmeEngine
    {
        private ConsoleGraphics cg;
        private List<IGameObj> gameObj = new List<IGameObj>();

        public AbstractGAmeEngine(ConsoleGraphics cg)
        {
            this.cg = cg;       
        }

        public void AddObj(IGameObj obj)
        {
            gameObj.Add(obj);
        }

        public void Start()
        {
            while(true)
            {
                foreach (var item in gameObj)
                {
                    item.Update(this);
                }
                foreach (var item in gameObj)
                {
                    item.Render(cg);
                }

                cg.FlipPages();
            }
        }
    }
}
