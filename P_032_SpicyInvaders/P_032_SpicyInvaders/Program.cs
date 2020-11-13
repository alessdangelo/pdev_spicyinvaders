/*
 * ETML
 * Auteurs: Bruno Martins Constantino, Manuel Oro, Alessandro D'Angelo, Clément Sartoni
 * Description: Classe program du projet Spiciy Invaders, contient l'architecture globale du jeu et le thread de jeu contenant les checks
 */
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Media;

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
        public static readonly string shootingEffectPath = Environment.CurrentDirectory + @"\Laser_Shoot.wav"; //A optimiser
        public static readonly string shotEffectPath = Environment.CurrentDirectory + @"\Hit_Hurt.wav"; //A optimiser
        private static readonly Random random = new Random();
        public static Player ship;

        public static Enemy[,] enemiesArray = new Enemy[10, 4]; //10, 4
        public static List<Shoot> bullets = new List<Shoot>();

        public static int[] enemiesSpawnPoint = {hudSizeX/2-enemiesArray.GetLength(0)/2, hudSizeY/2 - 5 - enemiesArray.GetLength(1)/2 };

        static int ennemyAlive = enemiesArray.Length;  //Take the numbers of ennemy and decrement it each time one dies.

        private static int enemiesSpeed;
        private static int bulletSpeed = 25;
        private static double reloadTime = 0.8;
        private static double invincibilityTime = 3.0;

        public static bool gameOver = false;
        public static bool soundOn = true;
        public static int difficulty = 0;

        private static DateTime one;
        private static DateTime two;
        private static DateTime timeBeforeShoot;
        private static DateTime tempInvincibility;

        private static int[] direction = new int[] { -1, 0 }; //la direction du pack en [x,y]
        private static int[] enemiesLimits = { 5, hudSizeX - 5, enemiesSpawnPoint[1] -3, enemiesSpawnPoint[1] + 10 }; //les limites du déplacemenmt, en [xMin, xMax, yMin, yMax]
        public static List<Block> blockList = new List<Block>();
        public static Hud hud;
        private static Menu menu;
        public static bool gamePaused = false;

        // Launch game
        public static void RunAll()
        {
            menu = new Menu();
            Console.SetWindowSize(50, 50);
            ship = new Player(39, 45, 3);
            hud = new Hud(hudSizeX, hudSizeY);

            // Music
            if (soundOn)
            {
                SoundPlayer music = new SoundPlayer();
                music.SoundLocation = fileToPlay;
                music.PlayLooping();
            }

            // Dificulty system
            if(difficulty == 0)
            {
                enemiesSpeed = 400;
            }
            else
            {
                enemiesSpeed = 150;
            }

            //initialisation des ennemis
            for (int y = 0; y < enemiesArray.GetLength(1); y++)
            {
                for (int x = 0; x < enemiesArray.GetLength(0); x++)
                {
                    enemiesArray[x, y] = new Enemy(enemiesSpawnPoint[0] + (2 * x), enemiesSpawnPoint[1] + (1 * y));
                }
            }

            // Add blocks
            blockList.Add(new Block( 7, 3 , Console.WindowWidth / 4 - 6, 40));
            blockList.Add(new Block(7, 3, Console.WindowWidth / 4 + 8, 40));
            blockList.Add(new Block(7, 3, Console.WindowWidth / 4 + 24, 40));
            blockList.Add(new Block(7, 3, Console.WindowWidth / 4 + 38, 40));

            // execute methods on keys input
            ConsoleKeyInfo keyEnterred;
            timeBeforeShoot = new DateTime();
            tempInvincibility = new DateTime();
            one = new DateTime();
            two = new DateTime();

            do
            {
                if (gamePaused  == false)
                {
                    GlobalMoves();

                    if (Console.KeyAvailable)
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
                                if (DateTime.Now > timeBeforeShoot)
                                {
                                    timeBeforeShoot = DateTime.Now.AddSeconds(reloadTime);
                                    DirectSoundOut shootingEffect = new DirectSoundOut();
                                    WaveFileReader shoot = new WaveFileReader(shootingEffectPath);
                                    shootingEffect.Init(new WaveChannel32(shoot));
                                    shootingEffect.Play();
                                    bullets.Add(new Shoot(ship.PosX, ship.PosY - 1, -1));
                                }
                                break;

                            case ConsoleKey.Escape:
                                gamePaused = true;
                                menu.PauseMenu();
                                gamePaused = false;
                                break;
                        }
                    }
                }

            }
            while (gameOver == false);
            if(ship.Life < 1)
            {
                hud.PrintGameOver();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("HEEEEE c cassé");
                menu.Win();
            }

            System.IO.File.WriteAllText(Environment.CurrentDirectory + "/highscore.txt", ship.Score.ToString());
        }

        /// <summary>
        /// Display Menu
        /// </summary>
        static void Main()
        {
            menu = new Menu();
            menu.MainMenu();
        }

        /// <summary>
        /// Moves ennemies and do some checks
        /// </summary>
        public static void GlobalMoves()
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
                            ennemyAlive--;
                            GC.Collect();
                        }
                    }
                }

                // check if a bullet hit a block then destroy part of the block
                foreach (Block block in blockList)
                {
                    for (int i = 0; i < bullets.Count; i++)
                    {
                        if (bullets[i] != null && block.IsInside(bullets[i].PosX, bullets[i].PosY ))
                        {
                            bullets[i].DestroyBullet();
                            GC.Collect();
                        }
                    }
                }

                // check if a bullet hit the player then decrease lifes
                for (int i = 0; i < bullets.Count; i++)
                {
                    if (bullets[i] != null && bullets[i].PosX == ship.PosX && bullets[i].PosY == ship.PosY)
                    {
                        bullets[i].DestroyBullet();
                        GC.Collect();
                        Console.SetCursorPosition(ship.PosX, ship.PosY);
                        Console.Write(ship.PlayerChar);

                        // invincibility time (when plyers is hit)
                        if (DateTime.Now > tempInvincibility)
                            {
                                tempInvincibility = DateTime.Now.AddSeconds(invincibilityTime);

                                DirectSoundOut shotEffect = new DirectSoundOut();
                                WaveFileReader shoot = new WaveFileReader(shotEffectPath);
                                shotEffect.Init(new WaveChannel32(shoot));
                                shotEffect.Play();
                                ship.Life--;
                                Hud.PrintPlayerLifes();
                            }
                    }
                }

                // if player has no more lifes, stop the game and display gameOver
                if(ship.Life < 1 || ennemyAlive == 0)
                {
                    gameOver = true;
                }
        }

        /// <summary>
        /// Move enemys and control shoot
        /// </summary>
        static public void MoveEnnemys()
        {
                
            if(DateTime.Now.Ticks > two.Ticks)
            {
                two = DateTime.Now.AddMilliseconds(enemiesSpeed);

                if(direction[1] == 1)
                {
                    for (int y = enemiesArray.GetLength(1)-1; y >= 0; y--)
                    {
                        for (int x = 0; x < enemiesArray.GetLength(0); x++)
                        {
                            if (enemiesArray[x, y].IsAlive)
                            {
                                if (random.Next(50) == 1)
                                {
                                    bullets.Add(new Shoot(enemiesArray[x, y].PosX, enemiesArray[x, y].PosY + 5, +1));
                                }
                            }
                            enemiesArray[x, y].Move(direction);
                        }
                    }
                }
                else
                {
                    foreach (Enemy ennemy in enemiesArray)
                    {
                        if (ennemy.IsAlive)
                        {
                            if (random.Next(50) == 1)
                            {
                                bullets.Add(new Shoot(ennemy.PosX, ennemy.PosY + 5, +1));
                            }
                        }
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

        /// <summary>
        /// Move all bullets at the same time
        /// </summary>
        static public void MoveBullets()
        {
            // wait some time before execute
            if(DateTime.Now.Ticks > one.Ticks)
            {
                one = DateTime.Now.AddMilliseconds(bulletSpeed);

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
