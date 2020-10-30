/*
	ETML
	Date: 11.09.20
	Auteur: Manuel Oro
	Description: Player class. Player can moove
	Modifié le: --
*/
using System;

namespace P_032_SpicyInvaders
{
    class Player
    {
        /// <summary>
        /// Attributs
        /// </summary>
        private readonly char player = 'x';
        private int _life = 3;
        private int _score = 0;
        private int _posX = 20;
        private int _posY = 20;
        private bool _canShoot = true;

        /// <summary>
        /// Properties
        /// </summary>
        public int Life
        {
            get { return _life; }
            set { _life = value; }
        }
        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }
        public int PosX
        {
            get { return _posX; }
        }
        public int PosY
        {
            get { return _posY; }
        }

        public bool CanShoot
        {
            get { return _canShoot; }
            set { _canShoot = value; }
        }

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="x">X position in console</param>
        /// <param name="y">Y position in console</param>
        /// <param name="life">Player lifes</param>
        public Player(int x, int y, int life)
        {
            this._posX = x;
            this._posY = y;
            this._life = life;

            Console.SetCursorPosition(_posX, _posY);
            Console.Write(player);
        }

        /// <summary>
        /// Move player
        /// </summary>
        /// <param name="direction">Direction (-1 = left | 1 = right)</param>
        public void Move(int direction)
        {
            Console.SetCursorPosition(_posX, _posY);
            Console.Write(" ");
            if (_posX != Console.WindowWidth - 1 && _posX != 0)
            {
                _posX += direction;
            }
            else
            {
                _posX -= direction;
            }
            Console.SetCursorPosition(_posX, _posY);
            Console.Write(player);
        }       
    }
}
