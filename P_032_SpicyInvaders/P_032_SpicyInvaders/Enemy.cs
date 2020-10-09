/*
	ETML
	Date: 11.09.20
	Auteur: Manuel Oro
	Description: Enemy class. Enemy can moove and be destroyed
	Modifié le: --
*/
using System;

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
        private bool _isAlive = true;

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

        public bool IsAlive
        {
            get { return _isAlive; }
            set { _isAlive = value; }
        }

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="posX">Spawn enemy at position x</param>
        /// <param name="posY">Spawn enemy at position y</param>
        /// <param name="speed">Set enemy speed</param>
        public Enemy(int posX, int posY)
        {
            _posX = posX;
            _posY = posY;
        }

        /// <summary>
        /// Move enemy
        /// </summary>
        /// <param name="direction">Select in which direction enemy goes</param>
        public void Move(int[] direction)
        {
            Console.SetCursorPosition(_posX, _posY);
            Console.Write(" ");
            _posX += direction[0];
            _posY += direction[1];
            Console.SetCursorPosition(_posX, _posY);
            Console.Write("*");
        }

        /// <summary>
        /// Remove enemy char
        /// </summary>
        public void DestroyEnemy()
        {
            Console.SetCursorPosition(_posX, _posY);
            Console.Write(" ");
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Enemy()
        {

        }
    }
}
