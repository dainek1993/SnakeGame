using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace SnakeGame
{
    class Snake : IDravable
    {
        private const int _cellWidth = 30;

        public Snake(int x, int y)
        {
            head = new Segment(x, y) { Direct = Direction.Up};
            body.Add(head);
            body.Add(new Segment(x , y + _cellWidth) { Direct = head.Direct });
            body.Add(new Segment(x , y + _cellWidth * 2) { Direct = head.Direct });
        }
        private Segment head;
        private List<Segment> body = new List<Segment>();
        public int Length => body.Count;

        public Segment this[int index]
        {
            get { return body[index]; }
        }

        internal void OnClick(object s, ControlEventArg e)
        {
            ChangeDirection(e.Direction);
        }

        public void ChangeDirection(Direction direction)
        {
            switch (head.Direct)
            {
                case Direction.Down:
                    if (direction == Direction.Up)
                        Reverse();
                    else
                        head.Direct = direction;
                    break;
                case Direction.Up:
                    if (direction == Direction.Down)
                        Reverse();
                    else
                        head.Direct = direction;
                    break;
                case Direction.Left:
                    if (direction == Direction.Right)
                        Reverse();
                    else
                        head.Direct = direction;
                    break;
                case Direction.Right:
                    if (direction == Direction.Left)
                        Reverse();
                    else
                        head.Direct = direction;
                    break;
            }
        }

        public void Draw(ConsoleGraphics cg)
        {
            for (int i = 1; i < body.Count; i++)
            {
                cg.DrawRectangle(0xFFFF0000, body[i].X, body[i].Y, _cellWidth, _cellWidth, 2f);
            }
            DrawHead(cg);
        }

        private void DrawHead(ConsoleGraphics cg)
        {
            cg.DrawRectangle(0xFF00FF00, head.X, head.Y, _cellWidth, _cellWidth, 2f);
        }

        public void MoveHeadSegment()
        {
            int x = head.X, y = head.Y;
            switch (head.Direct)
            {
                case Direction.Up:
                    y = (y <= 0) ? 600 - _cellWidth : y -= _cellWidth;
                    break;
                case Direction.Down:
                    y = (y >= 600 - _cellWidth) ? 0 : y += _cellWidth;
                    break;
                case Direction.Left:
                    x = (x <= 0) ? 600 - _cellWidth : x -= _cellWidth;
                    break;
                case Direction.Right:
                    x = (x >= 600 - _cellWidth) ? 0 : x += _cellWidth;
                    break;

            }
            head.SetNewCoord(x, y);
        }

        public void Hide(ConsoleGraphics cg)
        {
            for (int i = 0; i < body.Count; i++)
            {
                cg.DrawRectangle(0xFF000000, body[i].X, body[i].Y, _cellWidth, _cellWidth, 2f);
            }
        }
        public void MoveNext()
        {
            for (int i = body.Count - 1; i > 0; i--)
            {
                body[i].SetNewCoord(body[i - 1].X, body[i - 1].Y);
                body[i].Direct = body[i - 1].Direct;
            }
            MoveHeadSegment();
        }

        public void IncreaseBody()
        {
            Segment newSegment = new Segment(body[body.Count - 1].X, body[body.Count - 1].Y);
            MoveNext();
            body.Add(newSegment);
        }

        public void Reset(int x, int y)
        {
            head.Direct = Direction.Up;
            head.SetNewCoord(x, y);
            body.Clear();
            body.Add(head);
            body.Add(new Segment(x + _cellWidth, y));
            body.Add(new Segment(x + _cellWidth * 2, y));
        }

        public void Reverse()
        {
            body.Reverse();
            head = body[0];
            head.ReverseDirection();
            for (int i = 1; i < body.Count; i++)
            {
                body[i].ReverseDirection();
            }
        }
    }
}
