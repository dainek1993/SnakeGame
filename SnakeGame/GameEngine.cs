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

    class GameEngine
    {
        public GameEngine(ConsoleGraphics cg, int lvl)
        {
            this.curentLvl = lvl;
            rand = new Random();
            snake = new Snake(_snakeStartCoordX, _snakeStartCoordY);
            food = new Food(rand.Next(11) * _cellSize, rand.Next(11) * _cellSize);
            drawGameObj.Add(food);
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

        private const int _snakeStartCoordX = 300, _snakeStartCoordY = 300, _cellSize = 30;
        private List<IDravable> drawGameObj = new List<IDravable>();
        private List<Wall> walls = new List<Wall>();
        private ConsoleGraphics cg;
        private Snake snake;
        private Food food;
        private List<Control> arrowBtns = new List<Control>();
        private Random rand;
        private int curentLvl;

        private bool isPaused = false;
        private bool isExit = false;
        private int foodCount = 0;
 
        private Control btnUp = new Control(Keys.UP, Direction.Up);
        private Control btnDown = new Control(Keys.DOWN, Direction.Down);
        private Control btnLeft = new Control(Keys.LEFT, Direction.Left);
        private Control btnRight = new Control(Keys.RIGHT, Direction.Right);
        private Control btnSpace = new Control(Keys.SPACE, Direction.Down);
        private Control btnEsc = new Control(Keys.ESCAPE, Direction.Down);

        private void OnClick(object sender, ControlEventArg e)
        {
            if (e.Key == Keys.SPACE)
                isPaused = !isPaused;
            else
                snake.ChangeDirection(e.Direction);
        }

        private void BtnEsc_OnClick(object sender, ControlEventArg e)
        {
            File.WriteAllText("save.txt", curentLvl.ToString());
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
            while (IntersectWithWalls(food))
            {
                food.SetNewPosition(rand.Next(19) * _cellSize, rand.Next(19) * _cellSize);
            }
        }


        private bool IntersectWithWalls(GameObject obj)
        {
            if (walls.Count != 0)
            {
                foreach (Wall w in walls)
                {
                    for (int i = 0; i < w.Elements.Count; i++)
                    {
                        if (obj.IntersectWith(w.Elements[i]))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void Start()
        {
            cg.DrawRectangle(0xFF005050, 0, 0, 600, 600, 2f);
            BuildLevel(curentLvl);
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
                    if (foodCount++ == 15)
                    {
                        IncreaseLvl(++curentLvl);
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

                if (IntersectWithWalls(snake[0]))
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
            BuildLevel(lvl);
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
            cg.DrawString("Lvl " + (curentLvl - 9).ToString(), "Consolas", 0xFF00FF00, 610, 30);
        }

        public void AddObject(IDravable obj)
        {
            drawGameObj.Add(obj);
        }

        private void CreateAbstractLvl(int seed, Func<int, int, bool> condition)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (i == 2 && j == 2)
                        continue;
                    if (condition(i, j))
                    {
                        Wall w = new Wall(rand, i * (_cellSize * 4), j * (_cellSize * 4), seed);
                        walls.Add(w);
                        AddObject(w);
                    }

                }
            }
        }

        private void BuildLevel(int level)
        {
            int seed, difficult;
            if (level > 40 && level < 10)
            {
                return;
            }
            else
            {
                seed = level % 10;
                difficult = level / 10;

                switch ((Difficult)difficult)
                {
                    case Difficult.Calm:
                        break;
                    case Difficult.Easy:
                        CreateAbstractLvl(seed, (x, y) => (x % 2 == 0 && y % 2 == 0)); 
                        break;
                    case Difficult.Normal:
                        CreateAbstractLvl(seed, (x, y) => (x % 2 != 0 && y % 2 != 0));
                        break;
                    case Difficult.Hard:
                        CreateAbstractLvl(seed, (x, y) => (x % 2 != 0 && y % 2 == 0));
                        break;
                }

            }
        }
    }
}
