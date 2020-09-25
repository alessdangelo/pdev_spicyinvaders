using System;
using System.Threading;

namespace P_032_SpicyInvaders
{
    /// <summary>
    /// Class Enemy
    /// </summary>
    class Enemy
    {
        /// <summary>
        /// Attributes
        /// </summary>
        private int _posX;
        private int _posY;
        private int _speed;
        private Random _randomizer = new Random();
        private bool _goToLeft = true;
        private bool _isAlive = true;
        private Thread _moveEnemy;

        /// <summary>
        /// Properties
        /// </summary>
        public int Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public int PosX
        {
            get { return _posX; }
            set { _posX = value; }
        }

        public int PosY
        {
            get { return _posY; }
            set { _posY = value; }
        }


        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="posX">Spawn enemy at position x</param>
        /// <param name="posY">Spawn enemy at position y</param>
        /// <param name="speed">Set enemy speed</param>
        public Enemy(int posX, int posY, int speed)
        {
            _posX = posX;
            _posY = posY;
            _speed = speed;
            _moveEnemy = new Thread(Cycle);
            _moveEnemy.Start();
        }


        private static void WaitToMove(int speed)
        {
            Thread.Sleep(speed);
        }

        public void Cycle()
        {
            while (_isAlive)
            {
                if (_randomizer.Next(30) == 1)
                {
                    Program.ShootBulletFromEnemy(_posX, _posY);
                }
                if (_posX != Console.WindowLeft + 5)
                {
                    if (_goToLeft == true)
                    {
                        Console.SetCursorPosition(_posX, _posY);
                        Console.Write(" ");
                        _posX -= 2;
                        Console.SetCursorPosition(_posX, _posY);
                        Console.Write("*");
                    }
                }
                else
                {
                    _goToLeft = false;
                }

                if (_posX != Console.WindowWidth - 5)
                {
                    if (_goToLeft != true)
                    {
                        Console.SetCursorPosition(_posX, _posY);
                        Console.Write(" ");
                        _posX += 2;
                        Console.SetCursorPosition(_posX, _posY);
                        Console.Write("*");
                    }
                }
                else
                {
                    _goToLeft = true;
                }
                WaitToMove(_speed);
            }
            if (!_isAlive)
            {
                GC.Collect();
            }
        }

        private static void WaitToFire()
        {
            Thread.Sleep(200);
        }

        public void DestroyEntity()
        {
            GC.Collect();
        }

        ~Enemy()
        {

        }
    }
}
