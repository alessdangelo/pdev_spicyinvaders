/*
 * ETML
 * Auteurs: Bruno Martins Constantino, Manuel Oro, Alessandro D'Angelo, Clément Sartoni
 * Description: Classe program du projet Spiciy Invaders, contient l'architecture globale du jeu et le thread de jeu contenant les checks
 */
using System;
using System.Collections.Generic;
using System.Media;
using System.Threading;

namespace P_032_SpicyInvaders
{
    /// <summary>
    /// Class Program
    /// </summary>
    class Program
    {
        /// <summary>
        /// Class variables
        /// </summary>

        public const int hudSizeX = 80, hudSizeY = 50;

        public static readonly string musicFile = "song";
        public static readonly string fileToPlay = Environment.CurrentDirectory + $@"\{musicFile}.wav";
        private static readonly Random random = new Random();
        public static Player ship;

        public static Enemy[,] enemiesArray = new Enemy[10, 4]; //10, 4
        public static List<Shoot> bullets = new List<Shoot>();
        private static Thread Global;

        public static int[] enemiesSpawnPoint = {hudSizeX/2-enemiesArray.GetLength(0)/2, hudSizeY/2 - 5 - enemiesArray.GetLength(1)/2 };

        private static int enemiesSpeed = 400;
        private static bool gameOver = false;
        public static bool soundOn = true;
        public static int difficulty = 0;

        private static DateTime one;
        private static DateTime two;

        private static int[] direction = new int[] { -1, 0 }; //la direction du pack en [x,y]
        private static int[] enemiesLimits = { 5, hudSizeX - 5, enemiesSpawnPoint[1] - 6, enemiesSpawnPoint[1] + 6 }; //les limites du déplacemenmt, en [xMin, xMax, yMin, yMax]
        public static List<Block> blockList = new List<Block>();
        public static Hud hud;
        private static Menu menu;

        // Launch game
        public static void RunAll()
        {
            menu = null;
            Console.SetWindowSize(50, 50);
            ship = new Player(39, 45, 3);
            hud = new Hud(hudSizeX, hudSizeY);
            
            //Music
            SoundPlayer music = new SoundPlayer();
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

            // Add blocks
            blockList.Add(new Block(new int[] { 7, 3 }, new int[] { Console.WindowWidth / 4 - 6, 40}));
            blockList.Add(new Block(new int[] { 7, 3 }, new int[] { Console.WindowWidth / 4 + 8, 40 }));
            blockList.Add(new Block(new int[] { 7, 3 }, new int[] { Console.WindowWidth / 4 + 24, 40 }));
            blockList.Add(new Block(new int[] { 7, 3 }, new int[] { Console.WindowWidth / 4 + 38, 40 }));

            // play and loop music
            if (soundOn)
            {
                music.SoundLocation = fileToPlay; // Breakpoint here to see what fileToPlay is
                music.PlayLooping();
            }

            Global = new Thread(GlobalMoves);
            Global.Start();

            // execute methods on keys input
            ConsoleKeyInfo keyEnterred;
            int test = 0;
            do
            {
                keyEnterred = Console.ReadKey(true);
                switch (keyEnterred.Key)
                {
                    case ConsoleKey.RightArrow:
                        ship.Move(1);
                        break;

                    case ConsoleKey.LeftArrow:
                        ship.Move(-1);
                        break;

                    case ConsoleKey.Spacebar:
                        // wait one second before shoot again
                        if (DateTime.Now.Second > test)
                        {
                            test = DateTime.Now.Second;
                            bullets.Add(new Shoot(ship.PosX, ship.PosY - 1, -1));
                        }
                        break;
                }
            }
            while (gameOver == false);
        }

        /// <summary>
        /// Display Menu
        /// </summary>
        static void Main()
        {
            menu = new Menu();
        }

        /// <summary>
        /// Moves ennemies and do some checks
        /// </summary>
        public static void GlobalMoves()
        {
            one = new DateTime();
            two = new DateTime();

            do
            {
                // check is all ennemy are dead
                foreach (Enemy ennemy in enemiesArray)
                {
                    if (ennemy.IsAlive == true)
                    {
                        gameOver = false;
                    }
                }

                MoveEnnemys();
                MoveBullets();

                // check if as bullet hit ennemy then detroy ennemy and bullet
                foreach (Enemy ennemy in enemiesArray)
                {
                    for (int i = 0; i < bullets.Count; i++)
                    {
                        if (bullets[i] != null && bullets[i].PosX == ennemy.PosX && bullets[i].PosY == ennemy.PosY && ennemy.IsAlive)
                        {
                            bullets[i].DestroyBullet();
                            ennemy.IsAlive = false;
                            ennemy.DestroyEnemy();
                            GC.Collect();
                        }
                    }
                }

                // check if a bullet hit a block then destroy part of the block
                foreach (Block block in blockList)
                {
                    for (int i = 0; i < bullets.Count; i++)
                    {
                        if (block.IsInside(new int[] { bullets[i].PosX, bullets[i].PosY }))
                        {
                            bullets[i].DestroyBullet();
                            GC.Collect();
                        }
                    }
                }

                // check if a bullet hit the player then decrease lifes
                for (int i = 0; i < bullets.Count; i++)
                {
                    if (bullets[i].PosX == ship.PosX && bullets[i].PosY == ship.PosY)
                    {
                        bullets[i].DestroyBullet();
                        GC.Collect();

                        ship.Life--;
                        Hud.PrintPlayerLifes();
                    }
                }

                // if player has no more lifes, stop the game and display gameOver
                if(ship.Life < 1)
                {
                    gameOver = true;
                }
            }
            while (!gameOver);
            hud.PrintGameOver();
            Console.Read();
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
                        if (random.Next(50) == 1)
                        {
                            bullets.Add(new Shoot(ennemy.PosX, ennemy.PosY + 5, + 1));
                        }
                        ennemy.Move(direction);
                    }
                }
                //si l'ennemi en haut à gauche atteint la limite de gauche, change la direction des ennemis pour les faire descendre
                if (enemiesArray[0, 0].PosX + direction[0] <= enemiesLimits[0])
                {
                    direction = new int[] { 0, 1 };
                }
                //si l'ennemi en haut à droite atteint la limite de droite, change la direction des ennemis pour les faire monter
                if (enemiesArray[enemiesArray.GetLength(0) - 1, 0].PosX + direction[0] >= enemiesLimits[1])
                {
                    direction = new int[] { 0, -1 };
                }
                //si l'ennemi en haut à gauche atteint la limite du haut, change la direction des ennemis pour les faire aller à gauche
                if (enemiesArray[0, 0].PosY + direction[1] <= enemiesLimits[2])
                {
                    direction = new int[] { -1, 0 };
                }
                //si l'ennemi en bas à gauche atteint la limite du bas, change la direction des ennemis pour les faire aller à droite
                if (enemiesArray[0, enemiesArray.GetLength(1) - 1].PosY + direction[1] >= enemiesLimits[3])
                {
                    direction = new int[] { 1, 0 };
                }
            }
        }

        /// <summary>
        /// Move all bullets at the same time
        /// </summary>
        static public void MoveBullets()
        {
            // wait some time before execute
            if(DateTime.Now.Ticks > one.Ticks)
            {
                one = DateTime.Now.AddMilliseconds(40);

                for (int i = 0; i < bullets.Count; i++)
                {
                    // if bullet is in a specific range then move it, else destroy bullet
                    if (bullets[i].PosY > 10 && bullets[i].PosY < 45)
                    {
                        bullets[i].Move();
                    }
                    else
                    {
                        bullets[i].DestroyBullet();
                        GC.Collect();
                    }
                }                       
            }
        }
    }
}
