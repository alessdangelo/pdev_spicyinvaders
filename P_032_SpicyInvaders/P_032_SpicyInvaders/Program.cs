using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P_032_SpicyInvaders
{
    class Program
    {
        static readonly string musicFile = "song";
        static readonly string fileToPlay = Environment.CurrentDirectory + $@"\{musicFile}.wav";
        public static Player ship;

        public static Enemy[,] enemiesArray = new Enemy[10, 4];
        public static List<Shoot> bullets = new List<Shoot>();
        public static Thread Global;

        public static int[] enemiesSpawnPoint = {Console.WindowWidth/2-enemiesArray.GetLength(0)/2, Console.WindowHeight/1 - 5 - enemiesArray.GetLength(1)/2 };

        public static int enemiesSpeed = 400;
        public static bool gameOver = false;
        public static bool canShoot = true;
        public static bool soundOn = true;
        public static int difficulty = 0;
        public static Random random = new Random();

        private static DateTime one;
        private static DateTime two;

        public static int[] direction = new int[] { -1, 0 }; //la direction du pack en [x,y]
        public static int[] enemiesLimits = { 5, Console.WindowWidth - 5, enemiesSpawnPoint[1] - 6, enemiesSpawnPoint[1] + 6 }; //les limites du déplacemenmt, en [xMin, xMax, yMin, yMax]
        public static List<Block> blockList = new List<Block>();

        public static void RunAll()
        {
            Console.SetWindowSize(50, 50);
            ship = new Player(39, 45, 3);
            Hud hud = new Hud(80, 50);

            //Music
            var music = new System.Media.SoundPlayer();
            music.SoundLocation = fileToPlay; // Breakpoint here to see what fileToPlay is
            music.PlayLooping();

            //initialisation des ennemis
            for (int y = 0; y < enemiesArray.GetLength(1); y++)
            {
                for (int x = 0; x < enemiesArray.GetLength(0); x++)
                {
                    enemiesArray[x, y] = new Enemy(enemiesSpawnPoint[0] + (2 * x), enemiesSpawnPoint[1] + (1 * y));
                }
            }

            blockList.Add(new Block(new int[] { 7, 3 }, new int[] { Console.WindowWidth / 4 - 6, 40}));
            blockList.Add(new Block(new int[] { 7, 3 }, new int[] { Console.WindowWidth / 4 + 8, 40 }));
            blockList.Add(new Block(new int[] { 7, 3 }, new int[] { Console.WindowWidth / 4 + 24, 40 }));
            blockList.Add(new Block(new int[] { 7, 3 }, new int[] { Console.WindowWidth / 4 + 38, 40 }));


            if (soundOn)
            {
                music.SoundLocation = fileToPlay; // Breakpoint here to see what fileToPlay is
                music.PlayLooping();
            }

            Global = new Thread(GlobalMoves);
            Global.Start();

            ConsoleKeyInfo keyEnterred;
            do
            {
                keyEnterred = Console.ReadKey(true);
                switch (keyEnterred.Key)
                {
                    case ConsoleKey.RightArrow:
                        ship.moveRight();
                        break;

                    case ConsoleKey.LeftArrow:
                        ship.moveLeft();
                        break;

                    case ConsoleKey.Spacebar:
                        if (canShoot)
                        {
                            bullets.Add(new Shoot(ship.PosX, ship.PosY - 1, -1));
                            canShoot = false;
                        }
                        break;
                }
            }
            while (gameOver == false);
        }


        static void Main(string[] args)
        {
            Menu menu = new Menu();
        }

        public static void GlobalMoves()
        {
            one = new DateTime();
            two = new DateTime();

            do
            {
                MoveEnnemys();
                MoveBullets();

                foreach (Enemy ennemy in enemiesArray)
                {
                    for (int i = 0; i < bullets.Count; i++)
                    {
                        if (bullets[i].PosX == ennemy.PosX && bullets[i].PosY == ennemy.PosY && ennemy.IsAlive)
                        {
                            bullets[i].DestroyBullet();
                            canShoot = true;
                            ennemy.IsAlive = false;
                            ennemy.DestroyEnemy();
                            GC.Collect();
                        }
                    }
                }
                foreach (Block block in blockList)
                {
                    for (int i = 0; i < bullets.Count; i++)
                    {
                        if (block.IsInside(new int[] { bullets[i].PosX, bullets[i].PosY }))
                        {
                            bullets[i].DestroyBullet();
                            canShoot = true;
                            GC.Collect();
                        }
                    }
                }

                for (int i = 0; i < bullets.Count; i++)
                {
                    if (bullets[i].PosX == ship.PosX && bullets[i].PosY == ship.PosY)
                    {
                        bullets[i].DestroyBullet();
                        GC.Collect();
                        canShoot = true;

                        ship.Life--;
                        Hud.PrintPlayerLifes();
                    }
                }
            }
            while (true);
        }

        static public void MoveEnnemys()
        {
                
            if(DateTime.Now.Ticks > two.Ticks)
            {
                two = DateTime.Now.AddMilliseconds(enemiesSpeed);

                foreach (Enemy ennemy in enemiesArray)
                {
                    if (ennemy.IsAlive)
                    {
                        /*if (random.Next(50) == 1)
                        {
                            bullets.Add(new Shoot(ennemy.PosX, ennemy.PosY + 5, + 1));
                        }*/
                        ennemy.Move(direction);
                    }
                }
                if (enemiesArray[0, 0].PosX + direction[0] <= enemiesLimits[0])
                {
                    direction = new int[] { 0, 1 };
                }
                if (enemiesArray[enemiesArray.GetLength(0) - 1, 0].PosX + direction[0] >= enemiesLimits[1])
                {
                    direction = new int[] { 0, -1 };
                }
                if (enemiesArray[0, 0].PosY + direction[1] <= enemiesLimits[2])
                {
                    direction = new int[] { -1, 0 };
                }
                if (enemiesArray[0, enemiesArray.GetLength(1) - 1].PosY + direction[1] >= enemiesLimits[3])
                {
                    direction = new int[] { 1, 0 };
                }
            }
        }
        static public void MoveBullets()
        {
            if(DateTime.Now.Ticks > one.Ticks)
            {
                one = DateTime.Now.AddMilliseconds(30);

                for (int i = 0; i < bullets.Count; i++)
                {
                    if (bullets[i].PosY > 10 && bullets[i].PosY < 45)
                    {
                        bullets[i].Move();
                    }
                    else
                    {
                        bullets[i].DestroyBullet();
                        GC.Collect();
                        canShoot = true;
                    }
                }                       
            }
        }
    }
}
