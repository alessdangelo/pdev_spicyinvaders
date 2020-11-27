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
        /// Attributes
        /// </summary>
        public const int _hudSizeX = 80, _hudSizeY = 50;

        // Music
        private static DirectSoundOut _soundPlayer = new DirectSoundOut();
        public static ResourceManager _resMan = new ResourceManager(typeof(AppResources.SoundFiles));
        public static readonly string _mainSong = "song";
        public static readonly string _shootingEffect = "Laser_Shoot";
        public static readonly string _shotEffect = "Hit_Hurt";
        
        // Objects from class
        private static readonly Random _random = new Random();
        public static Player _ship;
        public static Hud _hud;
        public static Menu _menu;

        // Speed (Delay)
        private static int _enemiesSpeed;
        private readonly static int _bulletSpeed = 25;
        private readonly static double _reloadTime = 0.8;

        // Settings and score
        public static bool _gameOver = false;
        public static bool _soundOn = true;
        public static int _difficulty = 0;
        private readonly static string _highscorePath = @"highscore.txt";

        // Timer
        private static DateTime _one;
        private static DateTime _two;
        private static DateTime _timeBeforeShoot;

        // toDo : DONNER UN NOM ########################
        private static int[] _direction = new int[] { -1, 0 }; //la direction du pack en [x,y]
        public static Enemy[,] _enemiesArray = new Enemy[10, 4]; //10, 4
        public static int[] _enemiesSpawnPoint = { _hudSizeX / 2 - _enemiesArray.GetLength(0) / 2, _hudSizeY / 2 - 5 - _enemiesArray.GetLength(1) / 2 };
        private readonly static int[] _enemiesLimits = { 5, _hudSizeX - 5, _enemiesSpawnPoint[1] -3, _enemiesSpawnPoint[1] + 10 }; //les limites du déplacemenmt, en [xMin, xMax, yMin, yMax]
        public static List<Block> _blockList = new List<Block>();
        public static List<Shoot> _bullets = new List<Shoot>();


        static int _ennemyAlive = _enemiesArray.Length;  //Take the numbers of ennemy and decrement it each time one dies.
        // ##############################

        // State
        public static bool _gamePaused = false;


        // Launch game
        public static void RunAll()
        {
            _menu = new Menu();
            Console.SetWindowSize(50, 50);
            _ship = new Player(39, 45, 3);
            _hud = new Hud(_hudSizeX, _hudSizeY);

            // Enable Music
            if (_soundOn)
            {
                SoundPlayer music = new SoundPlayer();
                music.Stream = _resMan.GetStream(_mainSong);
                music.PlayLooping();
            }

            // Dificulty system
            if(_difficulty == 0)
            {
                _enemiesSpeed = 400;
            }
            else
            {
                _enemiesSpeed = 150;
            }

            // Ini/Spawn enemies
            for (int y = 0; y < _enemiesArray.GetLength(1); y++)
            {
                for (int x = 0; x < _enemiesArray.GetLength(0); x++)
                {
                    _enemiesArray[x, y] = new Enemy(_enemiesSpawnPoint[0] + (2 * x), _enemiesSpawnPoint[1] + (1 * y));
                }
            }

            // Add blocks
            _blockList.Add(new Block( 7, 3 , Console.WindowWidth / 4 - 6, 40));
            _blockList.Add(new Block(7, 3, Console.WindowWidth / 4 + 8, 40));
            _blockList.Add(new Block(7, 3, Console.WindowWidth / 4 + 24, 40));
            _blockList.Add(new Block(7, 3, Console.WindowWidth / 4 + 38, 40));

            // init some vars
            ConsoleKeyInfo keyEnterred;
            _timeBeforeShoot = new DateTime();
            _one = new DateTime();
            _two = new DateTime();

            // Main while (player input, enemies moves, bullets moves, ...)
            do
            {
                // do if game is not paused
                if (_gamePaused  == false)
                {
                    GlobalMoves();

                    if (Console.KeyAvailable)
                    {
                        keyEnterred = Console.ReadKey(true);
                        switch (keyEnterred.Key)
                        {
                                // Move right
                            case ConsoleKey.RightArrow:
                                _ship.Move(1);
                                break;
                                // Move left
                            case ConsoleKey.LeftArrow:
                                _ship.Move(-1);
                                break;
                                // Shoot
                            case ConsoleKey.Spacebar:
                                // wait one second before shoot again
                                if (DateTime.Now > _timeBeforeShoot)
                                {
                                    _timeBeforeShoot = DateTime.Now.AddSeconds(_reloadTime);
                                    PlaySound(_shootingEffect);
                                    _bullets.Add(new Shoot(_ship.PosX, _ship.PosY - 1, -1));
                                }
                                break;
                                // Pause game
                            case ConsoleKey.Escape:
                                _gamePaused = true;
                                _menu.PauseMenu();
                                _gamePaused = false;
                                break;
                        }
                    }
                }

            }
            while (_gameOver == false);
            if(_ship.Life < 1)
            {
                _menu.GameOver();
            }
            else
            {
                Console.Clear();
                _menu.Win();
            }

            WriteHighscore(_highscorePath);
        }

        /// <summary>
        /// Display Menu
        /// </summary>
        static void Main()
        {
            _menu = new Menu();
            _menu.MainMenu();
        }

        /// <summary>
        /// Moves ennemies and do some checks
        /// </summary>
        public static void GlobalMoves()
        {
                // check is all ennemy are dead
                foreach (Enemy ennemy in _enemiesArray)
                {
                    if (ennemy.IsAlive == true)
                    {
                        _gameOver = false;
                    }
                }
                MoveEnnemys();
                MoveBullets();
                // check if as bullet hit ennemy then detroy ennemy and bullet
                foreach (Enemy ennemy in _enemiesArray)
                {
                    for (int i = 0; i < _bullets.Count; i++)
                    {
                        if (_bullets[i] != null && _bullets[i].PosX == ennemy.PosX && _bullets[i].PosY == ennemy.PosY && ennemy.IsAlive)
                        {
                            _bullets[i].DestroyBullet();
                            ennemy.IsAlive = false;
                            ennemy.DestroyEnemy();
                            _ennemyAlive--;
                            GC.Collect();
                        }
                    }
                }

                // check if a bullet hit a block then destroy part of the block
                foreach (Block block in _blockList)
                {
                    for (int i = 0; i < _bullets.Count; i++)
                    {
                        if (_bullets[i] != null && block.IsInside(_bullets[i].PosX, _bullets[i].PosY ))
                        {
                            _bullets[i].DestroyBullet();
                            GC.Collect();
                        }
                    }
                }

                // check if a bullet hit the player then decrease lifes
                for (int i = 0; i < _bullets.Count; i++)
                {
                    if (_bullets[i] != null && _bullets[i].PosX == _ship.PosX && _bullets[i].PosY == _ship.PosY)
                    {
                        _bullets[i].DestroyBullet();
                        GC.Collect();
                        Console.SetCursorPosition(_ship.PosX, _ship.PosY);
                        Console.Write(_ship.PlayerChar);

                        // invincibility time (when player is hit) & decrement life
                        if (DateTime.Now > _ship.TempInvicibility)
                            {
                                _ship.Invicibility();
                                PlaySound(_shotEffect);
                                _ship.Life--;
                                Hud.PrintPlayerLifes();
                            }
                    }
                }

                // if player has no more lives or if the ennemies are dead, stop the game and display gameOver
                if(_ship.Life < 1 || _ennemyAlive == 0)
                {
                    _gameOver = true;
                }
        }

        /// <summary>
        /// Move enemys and control shoot
        /// </summary>
        static public void MoveEnnemys()
        {
            if(DateTime.Now.Ticks > _two.Ticks)
            {
                _two = DateTime.Now.AddMilliseconds(_enemiesSpeed);
                if(_direction[1] == 1)
                {
                    for (int y = _enemiesArray.GetLength(1)-1; y >= 0; y--)
                    {
                        for (int x = 0; x < _enemiesArray.GetLength(0); x++)
                        {
                            if (_enemiesArray[x, y].IsAlive)
                            {
                                if (_random.Next(50) == 1)
                                {
                                    _bullets.Add(new Shoot(_enemiesArray[x, y].PosX, _enemiesArray[x, y].PosY + 5, +1));
                                }
                            }
                            _enemiesArray[x, y].Move(_direction);
                        }
                    }
                }
                else
                {
                    foreach (Enemy ennemy in _enemiesArray)
                    {
                        if (ennemy.IsAlive)
                        {
                            if (_random.Next(50) == 1)
                            {
                                _bullets.Add(new Shoot(ennemy.PosX, ennemy.PosY + 5, +1));
                            }
                        }
                        ennemy.Move(_direction);
                    }
                }
              
                if (_enemiesArray[0, 0].PosX + _direction[0] <= _enemiesLimits[0])
                {
                    _direction = new int[] { 0, 1 };
                }
                if (_enemiesArray[_enemiesArray.GetLength(0) - 1, 0].PosX + _direction[0] >= _enemiesLimits[1])
                {
                    _direction = new int[] { 0, -1 };
                }
                if (_enemiesArray[0, 0].PosY + _direction[1] <= _enemiesLimits[2])
                {
                    _direction = new int[] { -1, 0 };
                }
                if (_enemiesArray[0, _enemiesArray.GetLength(1) - 1].PosY + _direction[1] >= _enemiesLimits[3])
                {
                    _direction = new int[] { 1, 0 };
                }
            }
        }
        /// <summary>
        /// Move all bullets at the same time
        /// </summary>
        static public void MoveBullets()
        {
            // wait some time before execute
            if(DateTime.Now.Ticks > _one.Ticks)
            {
                _one = DateTime.Now.AddMilliseconds(_bulletSpeed);
                for (int i = 0; i < _bullets.Count; i++)
                {
                    // if bullet is in a specific range then move it, else destroy bullet
                    if (_bullets[i].PosY > 10 && _bullets[i].PosY < 45)
                    {
                        _bullets[i].Move();
                    }
                    else
                    {
                        _bullets[i].DestroyBullet();
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
            if (_soundOn)
            {
                _soundPlayer.Init(new WaveChannel32(new WaveFileReader(_resMan.GetStream(name))));
                _soundPlayer.Play();
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
                File.WriteAllText(path, _ship.Score.ToString());
            }
            else
            {
                File.Create(path).Close();
            }
        }
    }
}
