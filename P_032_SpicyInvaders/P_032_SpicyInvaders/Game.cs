using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Resources;

namespace P_032_SpicyInvaders
{
    public class Game
    {
        /// <summary>
        /// Attributes
        /// </summary>
        private const int _hudSizeX = 80, _hudSizeY = 50;

        // Music
        private static DirectSoundOut _soundPlayer = new DirectSoundOut();
        private static ResourceManager _resMan = new ResourceManager(typeof(AppResources.SoundFiles));
        private static readonly string _mainSong = "song";
        private static readonly string _shootingEffect = "Laser_Shoot";
        private static readonly string _shotEffect = "Hit_Hurt";

        // Objects from class
        private static readonly Random _random = new Random();
        public static Player _ship;
        private static Menu _menu;

        // Speed (Delay)
        private static int _enemiesSpeed;
        private readonly static int _bulletSpeed = 25;
        private readonly static double _reloadTime = 0.8;

        // Settings and score
        private static bool _gameOver = false;
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
        private static int[] _enemiesSpawnPoint = { _hudSizeX / 2 - _enemiesArray.GetLength(0) / 2, _hudSizeY / 2 - 5 - _enemiesArray.GetLength(1) / 2 };
        private readonly static int[] _enemiesLimits = { 5, _hudSizeX - 5, _enemiesSpawnPoint[1] - 3, _enemiesSpawnPoint[1] + 10 }; //les limites du déplacemenmt, en [xMin, xMax, yMin, yMax]
        private static List<Block> _blockList = new List<Block>();
        public static List<Shoot> _bullets = new List<Shoot>();
        private static int _ennemyAlive = _enemiesArray.Length;  //Take the numbers of ennemy and decrement it each time one dies.

        // State
        private static bool _gamePaused = false;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Game()
        {

        }


    }
}
