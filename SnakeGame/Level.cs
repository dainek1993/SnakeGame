using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace SnakeGame
{
    class Level : IDravable
    {
        private const int _cellSize = 30;
        private List<Wall> walls = new List<Wall>();
        private int lvlNum;

        public Level(int lvlNumber)
        {
            LvlNum = lvlNumber;
        }

        public int LvlNum
        {
            get { return lvlNum; }
            set { lvlNum = (value > 9 && value < 50) ? value : 10; }
        }

        public bool IntersectWithWalls(GameObject obj)
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

        private void CreateAbstractLvl(int seed, Random rand, Func<int, int, bool> condition)
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
                    }

                }
            }
        }

        public void BuildLevel(Random rand)
        {
            int seed, difficult;
            seed = lvlNum % 10;
            difficult = lvlNum / 10;

            switch ((Difficult)difficult)
            {
                case Difficult.Calm:
                    break;
                case Difficult.Easy:
                    CreateAbstractLvl(seed, rand, (x, y) => (x % 2 == 0 && y % 2 == 0));
                    break;
                case Difficult.Normal:
                    CreateAbstractLvl(seed, rand, (x, y) => (x % 2 != 0 && y % 2 != 0));
                    break;
                case Difficult.Hard:
                    CreateAbstractLvl(seed, rand, (x, y) => (x % 2 != 0 && y % 2 == 0));
                    break;
            }

        }

        public void Draw(ConsoleGraphics cg)
        {
            foreach (var wall in walls)
            {
                wall.Draw(cg);
            }
        }
    }
}
