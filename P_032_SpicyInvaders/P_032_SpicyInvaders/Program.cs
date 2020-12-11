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
        private const int _windowWidth = 80, _windowHeight = 50;

        // Music
        private static SoundPlayer _music;
        private static DirectSoundOut _soundPlayer = new DirectSoundOut();
        private static ResourceManager _resMan = new ResourceManager(typeof(AppResources.SoundFiles));
        private static readonly string _mainSong = "Song";
        private static readonly string _shootingEffect = "Laser_Shoot";
        private static readonly string _shotEffect = "Hit_Hurt";
        
        // Objects from class
        private static Random _random = new Random();
        public static Player _ship;
        public static Hud _hud;
        private static Menu _menu;

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
        private static DateTime _bulletMove;
        private static DateTime _moveEnnemyAndControlShoot;
        private static DateTime _timeBeforeShoot;

        // Entities arrays and lists
        private static int[] _direction = new int[] { -1, 0 }; //la direction du pack en [x,y]
        private static Enemy[,] _enemiesArray = new Enemy[10, 4]; //10, 4
        private static readonly int[] _enemiesSpawnPoint = { _windowWidth / 2 - _enemiesArray.GetLength(0) / 2, _windowHeight / 2 - 5 - _enemiesArray.GetLength(1) / 2 };
        private static int[] _enemiesLimits = { 5, _windowWidth - 5, _enemiesSpawnPoint[1] -3, _enemiesSpawnPoint[1] + 10 }; //les limites du déplacemenmt, en [xMin, xMax, yMin, yMax]
        private static List<Block> _blockList = new List<Block>();
        public static List<Shoot> _bullets = new List<Shoot>();
        private static int _ennemyAlive = _enemiesArray.Length;  //Take the numbers of ennemy and decrement it each time one dies.

        // Block Size
        private static readonly int _blockXSize = 7;
        private static readonly int _blockYSize = 3;

        // State
        public static bool _gamePaused = false;

        /// <summary>
        /// Main method: Display Menu
        /// </summary>
        static void Main()
        {
            _menu = new Menu();
            _menu.MainMenu();
        }

        /// <summary>
        /// Run game
        /// </summary>
        public static void RunGame()
        {
            Console.SetWindowSize(_windowWidth, _windowHeight);
            Console.SetBufferSize(_windowWidth, _windowHeight);

            _ship = new Player(39, 45, 3);
            _hud = new Hud();
            _hud.PrintAllInfos();

            // Play Music
            Sound.PlaySound(Sound.Sounds.Song);

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
            _blockList.Add(new Block(_blockXSize, _blockYSize, Console.WindowWidth / 4 - 6, 40));
            _blockList.Add(new Block(_blockXSize, _blockYSize, Console.WindowWidth / 4 + 8, 40));
            _blockList.Add(new Block(_blockXSize, _blockYSize, Console.WindowWidth / 4 + 24, 40));
            _blockList.Add(new Block(_blockXSize, _blockYSize, Console.WindowWidth / 4 + 38, 40));

            // init some vars
            ConsoleKeyInfo keyEnterred;
            _timeBeforeShoot = new DateTime();
            _bulletMove = new DateTime();
            _moveEnnemyAndControlShoot = new DateTime();

            // wait some time before start -> don't surprise player
            System.Threading.Thread.Sleep(500);

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
                                    Sound.PlaySound(Sound.Sounds.Laser_Shoot);
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

            WriteHighscore(_highscorePath);

            if (_music != null)
            {
                _music.Stop();
            }

            if (_ship.Life < 1)
            {
                _menu.GameOver();
            }
            else
            {
                Console.Clear();
                _menu.Win();
            }
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
            Enemy.MoveEnnemies(ref _moveEnnemyAndControlShoot, ref _enemiesSpeed, ref _direction, ref _enemiesArray, _random, ref _bullets, ref _enemiesLimits);
            Shoot.MoveBullets(ref _bulletMove, ref _bullets);
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
                    }
                }
            }

            // check if a bullet hit the player then decrease lifes
            for (int i = 0; i < _bullets.Count; i++)
            {
                if (_bullets[i] != null && _bullets[i].PosX == _ship.PosX && _bullets[i].PosY == _ship.PosY)
                {
                    _bullets[i].DestroyBullet();
                    Console.SetCursorPosition(_ship.PosX, _ship.PosY);
                    Console.Write(_ship.PlayerChar);

                    // invincibility time (when player is hit) & decrement life
                    if (DateTime.Now > _ship.TempInvicibility)
                        {
                            _ship.Invicibility();
                            Sound.PlaySound(Sound.Sounds.Hit_Hurt);
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

            GC.Collect();
        }

        /// <summary>
        /// Write highscore in txt file
        /// </summary>
        /// <param name="path">txt file path</param>
        public static void WriteHighscore(string path)
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
