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
    /// <summary>
    /// Class Player
    /// </summary>
    class Player
    {
        /// <summary>
        /// Attributs
        /// </summary>
        private readonly char _playerChar = 'x';
        private int _life = 3;
        private int _score = 0;
        private int _posX = 20;
        private int _posY = 20;
        private bool _isPosLimitRight = false;
        private bool _isPosLimitLeft = false;
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

        public char PlayerChar
        {
            get { return _playerChar; }
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
            Console.Write(_playerChar);
        }

        /// <summary>
        /// Move player
        /// </summary>
        /// <param name="direction">Direction (-1 = left | 1 = right)</param>
        public void Move(int direction)
        {
            Console.SetCursorPosition(_posX, _posY);
            Console.Write(" ");
            if (_posX >= Console.WindowWidth - 1) //Check if player is in the right border
            {
                _isPosLimitRight = true;    //Player can't move to the right
                //_posX = Console.WindowWidth - 2;
            }
            else if (_posX == Console.WindowWidth - 2) //Check if player is not in the right border
            {
                _isPosLimitRight = false;   //Player can move to the right
            }
            if (_posX <= 0) //Check if player is in the left border
            {
                _isPosLimitLeft = true; //Player can't move to the left
                //_posX = 1;
            }
            else if (_posX == 1)    //Check if player is in the left border
            {
                _isPosLimitLeft = false;    //Player can move to the left
            }

            //Player move to the right or left if he's not in the limits
            if (direction == 1 && _isPosLimitRight == false)
            {
                _posX += direction;
            }
            if (direction == -1 && _isPosLimitLeft == false)
            {
                _posX += direction;
            }
            Console.SetCursorPosition(_posX, _posY);
            Console.Write(_playerChar);
        }       
    }
}
