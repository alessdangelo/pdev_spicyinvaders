/*
	ETML
	Date: 11.12.2020
	Auteur: ADO, MOO
	Description: Class game that contains the game loop.
	Modifié le: 08.01.201
*/

using System;
using System.Collections.Generic;

namespace P_032_SpicyInvaders
{
    /// <summary>
    /// Class Game
    /// </summary>
    public class Game
    {
        //Consts
        /// <summary>
        /// the constants used to set the size of the window
        /// </summary>
        private const int _windowWidth = 80, _windowHeight = 50;

        // Objects from class
        /// <summary>
        /// A Usefull random
        /// </summary>
        private Random _random = new Random();

        /// <summary>
        /// The Game's Player
        /// </summary>
        public Player _ship;

        /// <summary>
        /// The Menu that launched the game
        /// </summary>
        private Menu _menu;

        // Speed (Delay)
        /// <summary>
        /// Game ennemies Speed
        /// </summary>
        private int _enemiesSpeed;

        /// <summary>
        /// the Game's player reaload time
        /// </summary>
        private readonly double _reloadTime = 0.8;

        // Default settings and score
        /// <summary>
        /// Game's game over state
        /// </summary>
        public bool _gameOver = false;

        /// <summary>
        /// Game's parameter for the music
        /// </summary>
        public bool _soundOn = true;

        /// <summary>
        /// Game's difficulty parameter
        /// </summary>
        public static int _difficulty = 0;

        // Timers
        /// <summary>
        /// Game's Timer for the bullet's speed
        /// </summary>
        private DateTime _bulletMove;

        /// <summary>
        /// Game's timer that sets the speed of ennemies and the frequency of checks
        /// </summary>
        private DateTime _moveEnnemyAndControlShoot;

        /// <summary>
        /// Game's timer to limit the shoot speed of the player
        /// </summary>
        private DateTime _timeBeforeShoot;

        // Entities arrays and lists
        /// <summary>
        /// Game's ennemies direction by axis [x,y]
        /// </summary>
        private int[] _direction = new int[] { -1, 0 };

        /// <summary>
        /// Game's array of ennemies. Default : 10, 4 Number of ennemies in the group by [row,col]
        /// </summary>
        private static Enemy[,] _enemiesArray = new Enemy[10, 4];

        /// <summary>
        /// Define the spawnpoint of the ennemies, calculated relatively to the window size
        /// </summary>
        private static readonly int[] _enemiesSpawnPoint = { _windowWidth / 2 - _enemiesArray.GetLength(0) / 2, _windowHeight / 2 - 5 - _enemiesArray.GetLength(1) / 2 };

        /// <summary>
        /// Game's ennemies movements wall limit by [xMin, xMax, yMin, yMax] (Ennemies can't go further than that)
        /// </summary>
        private int[] _enemiesLimits = { 5, _windowWidth - 5, _enemiesSpawnPoint[1] - 3, _enemiesSpawnPoint[1] + 10 }; 

        /// <summary>
        /// Game's block List
        /// </summary>
        private List<Block> _blockList = new List<Block>();

        /// <summary>
        /// Game's Shoot List
        /// </summary>
        public static List<Shoot> _bullets = new List<Shoot>();

        /// <summary>
        /// Game's counter of ennemies still alive, at start ake the numbers of ennemy and then decrement it each time one dies.
        /// </summary>
        private int _ennemyAlive = _enemiesArray.Length; 

        // Block Size
        /// <summary>
        /// Game Block's size in X
        /// </summary>
        private readonly int _blockXSize = 7;

        /// <summary>
        /// Game Block's size in Y
        /// </summary>
        private readonly int _blockYSize = 3;

        // State
        /// <summary>
        /// Game's state to set a break in the game
        /// </summary>
        public static bool _gamePaused = false;

        /// <summary>
        /// Custom Constructor
        /// </summary>
        /// <param name="menu">menu that launches the game</param>
        public Game(Menu menu)
        {
            this._menu = menu;
        }

        /// <summary>
        /// Run game
        /// </summary>
        public void RunGame()
        {
            GC.Collect();
            Console.SetWindowSize(_windowWidth, _windowHeight);
            Console.SetBufferSize(_windowWidth, _windowHeight);

            _ship = new Player(39, 45, 3, 3.0);
            Hud.PrintAllInfos(_ship.Score, _ship.Life);

            // Play Music
            Sound.PlaySound(Sound.Sounds.Song);

            // Dificulty system
            if (_difficulty == 0)
            {
                _enemiesSpeed = 400;
            }
            else
            {
                _enemiesSpeed = 150;
            }

            // Init/Spawn enemies
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
            System.Threading.Thread.Sleep(400);

            // Main while (player input, enemies moves, bullets moves, ...)
            do
            {
                // do if game is not paused
                if (_gamePaused == false)
                {
                    GlobalMovesAndChecks();

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

            //Calls the methods according if you won or lost
            if (_ship.Life < 1)
            {
                _menu.GameOver(_ship.Score);
            }
            else
            {
                Console.Clear();
                _menu.Win(_ship.Score);
            }
        }

        /// <summary>
        /// Moves ennemies and do some checks
        /// </summary>
        public void GlobalMovesAndChecks()
        {
            Enemy.MoveEnnemies(ref _moveEnnemyAndControlShoot, ref _enemiesSpeed, ref _direction, ref _enemiesArray, _random, ref _bullets, ref _enemiesLimits);
            Shoot.MoveBullets(ref _bulletMove, ref _bullets);

            if (DateTime.Now > _ship.TempInvicibility)
            {
                Console.SetCursorPosition(_ship.PosX, _ship.PosY);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(_ship.Sprite);
            }

            // check if as bullet hit ennemy then destroy ennemy and bullet
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
                        _ship.Score += 100;
                        Hud.PrintPlayerScore(_ship.Score);
                        _enemiesSpeed -= 4;
                    }
                }

                if (ennemy.IsAlive == true)
                {
                    _gameOver = false;
                }
            }

            // check if a bullet hit a block then destroy part of the block
            foreach (Block block in _blockList)
            {
                for (int i = 0; i < _bullets.Count; i++)
                {
                    if (_bullets[i] != null && block.IsInside(_bullets[i].PosX, _bullets[i].PosY))
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(_ship.Sprite);
                    Console.ForegroundColor = ConsoleColor.White;

                    // invincibility time (when player is hit) & decrement life
                    if (DateTime.Now > _ship.TempInvicibility)
                    {
                        _ship.Invicibility();
                        Sound.PlaySound(Sound.Sounds.Hit_Hurt);
                        _ship.Life--;
                        Hud.PrintPlayerLifes(_ship.Life);
                    }
                }
            }

            // if player has no more lives or if the ennemies are dead, stop the game and display gameOver
            if (_ship.Life < 1 || _ennemyAlive == 0)
            {
                _gameOver = true;
            }
        }
    }
}
