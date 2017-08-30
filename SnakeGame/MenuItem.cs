using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace SnakeGame
{
    delegate void EventDelegate();

    class MenuItem
    {
        private int x, y, w, h;
        private uint textColor;
        private string text;
        public event EventDelegate Hover;
        public event EventDelegate Click;

        public MenuItem(int x, int y, int w, int h, string text)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            this.text = text;
        }

        public void Draw(ConsoleGraphics cg)
        {
            cg.DrawString(text, "Consolas", textColor, x, y, h);
        }

        public void Update()
        {
            textColor = IsMouseOnItem() ? 0xFFFF0000 : 0xFFFFF000;
            if (IsMouseOnItem())
            {
                if (Input.IsMouseLeftButtonDown && Click != null)
                    Click();
                else if (Hover != null)
                    Hover();
            }
        }

        private bool IsMouseOnItem()
        {
            int mx = Input.MouseX;
            int my = Input.MouseY;
            return mx >= x && my >= y && mx <= x + w && my <= y + h;
        }
    }

}
