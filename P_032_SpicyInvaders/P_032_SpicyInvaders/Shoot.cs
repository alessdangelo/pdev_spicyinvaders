/*
	ETML
	Date: 11.09.20
	Auteur: Manuel Oro
	Description: Shoot class. Shoot can moove and destroyed
	Modifié le: --
*/
using System;
using System.Diagnostics;

namespace P_032_SpicyInvaders
{
    /// <summary>
    /// Class Shoot
    /// </summary>
    class Shoot
    {
        /// <summary>
        /// Attributes
        /// </summary>
        private int _posX;
        private int _posY;
        private int _direction;

        /// <summary>
        /// Properties
        /// </summary>
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
        /// <param name="x">Position x in console</param>
        /// <param name="y">Position y in console</param>
        /// <param name="direction">Direction (-1 Up | 1 Down)</param>
        public Shoot(int x, int y, int direction)
        {
            this._posX = x;
            this._posY = y;
            this._direction = direction;
        }

        /// <summary>
        /// Move bullet
        /// </summary>
        public void Move()
        {
            Program.canShoot = false;

            Console.SetCursorPosition(_posX, _posY);
            Console.Write(" ");
            _posY += _direction;
            Console.SetCursorPosition(_posX, _posY);
            Console.Write("■");

        }

        /// <summary>
        /// Destroy bullet char
        /// </summary>
        public void DestroyBullet()
        {
            Console.SetCursorPosition(_posX, _posY);
            Console.Write(" ");
            Program.bullets.Remove(this);
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Shoot()
        {
            Debug.WriteLine("Destructor");
        }
    }
}
