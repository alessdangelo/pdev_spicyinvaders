﻿using System;
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
        private int _shootProbability = 30;
        private Random _randomizer = new Random();
        private bool _goToLeft = true;
        private bool _isAlive = true;
        //private Thread _moveEnemy;

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

        public bool IsAlive
        {
            get { return _isAlive; }
            set { _isAlive = value; }
        }

        public int ShootProbability
        {
            get { return _shootProbability; }
            set { _shootProbability = value; }
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
            /*_moveEnemy = new Thread(Cycle);
            _moveEnemy.Start();*/
        }


        public void WaitToMove(int speed)
        {
            Thread.Sleep(speed);
        }

        public void Move(int[] direction)
        {
            Console.SetCursorPosition(_posX, _posY);
            Console.Write(" ");
            _posX += direction[0];
            _posY += direction[1];
            Console.SetCursorPosition(_posX, _posY);
            Console.Write("*");
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
