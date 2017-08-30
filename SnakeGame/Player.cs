using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NConsoleGraphics;
using System.IO;

namespace SnakeGame
{
    enum Difficult { Calm = 1, Easy, Normal, Hard}

    class Player : IGameObj
    {
        private const int _snakeStartCoordX = 300, _snakeStartCoordY = 300, _cellSize = 30;
        private List<IDravable> drawGameObj = new List<IDravable>();
        
        private ConsoleGraphics cg;
        private Snake snake;
        private Food food;
        private List<Control> arrowBtns = new List<Control>();
        private Random rand;
        private int currentLvlNumber;
        private Level currentLvl;

        private bool isPaused = false;
        private bool isExit = false;
        private int foodCount = 0;

        private Control btnUp = new Control(Keys.UP, Direction.Up);
        private Control btnDown = new Control(Keys.DOWN, Direction.Down);
        private Control btnLeft = new Control(Keys.LEFT, Direction.Left);
        private Control btnRight = new Control(Keys.RIGHT, Direction.Right);
        private Control btnSpace = new Control(Keys.SPACE, Direction.Down);
        private Control btnEsc = new Control(Keys.ESCAPE, Direction.Down);

        public Player(ConsoleGraphics cg, int lvl)
        {
            this.currentLvlNumber = lvl;
            currentLvl = new Level(lvl);
            rand = new Random();
            snake = new Snake(_snakeStartCoordX, _snakeStartCoordY);
            food = new Food(rand.Next(11) * _cellSize, rand.Next(11) * _cellSize);
            this.cg = cg;
            btnSpace.OnClick += OnClick;
            arrowBtns.Add(btnUp);
            btnUp.OnClick += OnClick;
            arrowBtns.Add(btnDown);
            btnDown.OnClick += OnClick;
            arrowBtns.Add(btnLeft);
            btnLeft.OnClick += OnClick;
            arrowBtns.Add(btnRight);
            btnRight.OnClick += OnClick;
            btnEsc.OnClick += BtnEsc_OnClick;
        }

        private void OnClick(object sender, ControlEventArg e)
        {
            if (e.Key == Keys.SPACE)
                isPaused = !isPaused;
            else
                snake.ChangeDirection(e.Direction);
        }

        private void BtnEsc_OnClick(object sender, ControlEventArg e)
        {
            File.WriteAllText("save.txt", currentLvl.LvlNum.ToString());
            isExit = true;
            btnSpace.OnClick -= OnClick;
            btnEsc.OnClick -= BtnEsc_OnClick;
            cg.FillRectangle(0xFF000000, 0, 0, 600, 600);
            foreach (Control cc in arrowBtns)
            {
                cc.OnClick -= OnClick; 
            }
            
        }

        private void ChangeFoodCoord(Food food)
        {
            food.SetNewPosition(rand.Next(19) * _cellSize, rand.Next(19) * _cellSize);
            while (currentLvl.IntersectWithWalls(food))
            {
                food.SetNewPosition(rand.Next(19) * _cellSize, rand.Next(19) * _cellSize);
            }
        }

        public void Start()
        {
            cg.DrawRectangle(0xFF005050, 0, 0, 600, 600, 2f);
            currentLvl.BuildLevel(rand);
            AddObject(currentLvl);
            AddObject(food);
            while (true)
            {
                btnEsc.Check();
                if (isExit)
                    break;

                btnSpace.Check();
                if (isPaused)
                {
                    btnSpace.Check();
                    continue;
                }

                foreach (var i in arrowBtns)
                    i.Check();

                foreach (var item in drawGameObj)
                    item.Draw(cg);

                snake.Hide(cg);
                RedrawStats(foodCount);
                if (snake[0].IntersectWith(food))
                {
                    snake.IncreaseBody();
                    if (foodCount++ == 1)
                    {
                        IncreaseLvl(++currentLvlNumber);
                    }
                    ChangeFoodCoord(food);
                    food.Draw(cg);
                    RedrawStats(foodCount);
                }

                for (int i = 1; i < snake.Length; i++)
                {
                    if (snake[0].IntersectWith(snake[i]))
                    {
                        snake.Reset(_snakeStartCoordX, _snakeStartCoordY);
                        ResetStats();
                    }
                }

                if (currentLvl.IntersectWithWalls(snake[0]))
                {
                    snake.Reset(_snakeStartCoordX, _snakeStartCoordY);
                    ResetStats();
                }

                snake.MoveNext();
                snake.Draw(cg);
                cg.DrawRectangle(0xFF005050, 0, 0, 600, 600, 2f);
                Thread.Sleep(100);
                cg.FlipPages();
            }
            
        }

        private void IncreaseLvl(int lvl)
        {
            cg.FillRectangle(0xFF000000, 0, 0, 600, 600);
            RemoveObj(currentLvl);
            currentLvl = new Level(lvl);
            currentLvl.BuildLevel(rand);
            AddObject(currentLvl);
            snake.Reset(_snakeStartCoordX, _snakeStartCoordY);
            foodCount = 0;
        }

        private void ResetStats()
        {
            foodCount = 0;
            RedrawStats(foodCount);
        }

        private void RedrawStats(int count)
        {
            cg.FillRectangle(0xFF000000, 600, 10, 150, 40);
            cg.DrawString("Score " + count.ToString(), "Consolas", 0xFF00FF00, 610, 10);
            cg.DrawString("Lvl " + (currentLvl.LvlNum - 9).ToString(), "Consolas", 0xFF00FF00, 610, 30);
        }

        public void AddObject(IDravable obj)
        {
            drawGameObj.Add(obj);
        }

        public void RemoveObj(IDravable obj)
        {
            drawGameObj.Remove(obj);
        }

        public void Render(ConsoleGraphics cg)
        {
            foreach (var item in drawGameObj)
            {
                item.Draw(cg);
            }
        }

        public void Update(AbstractGAmeEngine age)
        {
            //this.Start();
        }
    }
}
