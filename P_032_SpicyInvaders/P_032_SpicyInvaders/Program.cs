/*
 * ETML
 * Auteurs: Bruno Martins Constantino, Manuel Oro, Alessandro D'Angelo, Clément Sartoni
 * Description: Spicy Invaders program class, contains the overall architecture of the game and the game thread containing the checks. 
 */
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Resources;

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

        // Music
        private static DirectSoundOut soundPlayer = new DirectSoundOut();
        public static ResourceManager resMan = new ResourceManager(typeof(AppResources.SoundFiles));
        public static readonly string mainSong = "song";
        public static readonly string shootingEffect = "Laser_Shoot";
        public static readonly string shotEffect = "Hit_Hurt";
        
        // Objects from class
        private static readonly Random random = new Random();
        public static Player ship;
        public static Hud hud;
        public static Menu menu;

        // Speed (Delay)
        private static int enemiesSpeed;
        private static int bulletSpeed = 25;
        private static double reloadTime = 0.8;

        // Settings and score
        public static bool gameOver = false;
        public static bool soundOn = true;
        public static int difficulty = 0;
        private readonly static string _highscorePath = @"highscore.txt";

        // Timer
        private static DateTime one;
        private static DateTime two;
        private static DateTime timeBeforeShoot;

        // toDo : DONNER UN NOM ########################
        private static int[] direction = new int[] { -1, 0 }; //la direction du pack en [x,y]
        public static Enemy[,] enemiesArray = new Enemy[10, 4]; //10, 4
        public static int[] enemiesSpawnPoint = { hudSizeX / 2 - enemiesArray.GetLength(0) / 2, hudSizeY / 2 - 5 - enemiesArray.GetLength(1) / 2 };
        private static int[] enemiesLimits = { 5, hudSizeX - 5, enemiesSpawnPoint[1] -3, enemiesSpawnPoint[1] + 10 }; //les limites du déplacemenmt, en [xMin, xMax, yMin, yMax]
        public static List<Block> blockList = new List<Block>();
        public static List<Shoot> bullets = new List<Shoot>();


        static int ennemyAlive = enemiesArray.Length;  //Take the numbers of ennemy and decrement it each time one dies.
        // ##############################

        // State
        public static bool gamePaused = false;

        // Launch game
        public static void RunAll()
        {
            menu = new Menu();
            Console.SetWindowSize(50, 50);
            ship = new Player(39, 45, 3);
            hud = new Hud(hudSizeX, hudSizeY);

            // Enable Music
            if (soundOn)
            {
                SoundPlayer music = new SoundPlayer();
                music.Stream = resMan.GetStream(mainSong);
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

            // Ini/Spawn enemies
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

            // init some vars
            ConsoleKeyInfo keyEnterred;
            timeBeforeShoot = new DateTime();
            one = new DateTime();
            two = new DateTime();

            // Main while (player input, enemies moves, bullets moves, ...)
            do
            {
                // do if game is not paused
                if (gamePaused  == false)
                {
                    GlobalMoves();

                    if (Console.KeyAvailable)
                    {
                        keyEnterred = Console.ReadKey(true);
                        switch (keyEnterred.Key)
                        {
                                // Move right
                            case ConsoleKey.RightArrow:
                                ship.Move(1);
                                break;
                                // Move left
                            case ConsoleKey.LeftArrow:
                                ship.Move(-1);
                                break;
                                // Shoot
                            case ConsoleKey.Spacebar:
                                // wait one second before shoot again
                                if (DateTime.Now > timeBeforeShoot)
                                {
                                    timeBeforeShoot = DateTime.Now.AddSeconds(reloadTime);
                                    PlaySound(shootingEffect);
                                    bullets.Add(new Shoot(ship.PosX, ship.PosY - 1, -1));
                                }
                                break;
                                // Pause game
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
                menu.GameOver();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("TACO FROM TRELLOOOOOOO \n\n\n\n\n\n\n\n\n../ ../ ../ ../ we will we will rock you");
                menu.Win();
            }

            WriteHighscore(_highscorePath);
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

                        // invincibility time (when player is hit) & decrement life
                        if (DateTime.Now > ship.TempInvicibility)
                            {
                                ship.Invicibility();
                                PlaySound(shotEffect);
                                ship.Life--;
                                Hud.PrintPlayerLifes();
                            }
                    }
                }

                // if player has no more lives or if the ennemies are dead, stop the game and display gameOver
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
        /// <summary>
        /// Play a sound effect
        /// </summary>
        /// <param name="path">Sound path to play</param>
        public static void PlaySound(string name)
        {
            if (soundOn)
            {
                soundPlayer.Init(new WaveChannel32(new WaveFileReader(resMan.GetStream(name))));
                soundPlayer.Play();
            }
        }

        /// <summary>
        /// Write highscore in txt file
        /// </summary>
        /// <param name="path">txt file path</param>
        private static void WriteHighscore(string path)
        {
            if (File.Exists(path))
            {
                File.WriteAllText(path, ship.Score.ToString());
            }
            else
            {
                File.Create(path).Close();
            }
        }
    }
}
