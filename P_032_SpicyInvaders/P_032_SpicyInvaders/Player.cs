/*
	ETML
	Date: 11.09.20
	Auteur: Manuel Oro, Alessandro D'Angelo
	Description: Player class. Player can move.
	Modifié le: --
*/
using System;

namespace P_032_SpicyInvaders
{
    /// <summary>
    /// Class Player
    /// </summary>
    public class Player : Entity
    {
        /// <summary>
        /// Attributes
        /// </summary>
        private readonly char _playerChar = '♠';
        private int _life = 3;
        private int _score = 0;
        private bool _isPosLimitRight = false;
        private bool _isPosLimitLeft = false;
        private bool _canShoot = true;
        private static DateTime _tempInvincibility = new DateTime();
        private readonly double _invincibilityTime = 3.0;
        
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
        public bool CanShoot
        {
            get { return _canShoot; }
            set { _canShoot = value; }
        }

        public char PlayerChar
        {
            get { return _playerChar; }
        }

        public DateTime TempInvicibility
        {
            get{ return _tempInvincibility; }
            set{ _tempInvincibility = value; }
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
        /// Procure invincibility to player
        /// </summary>
        public void Invicibility()
        {
            _tempInvincibility = DateTime.Now.AddSeconds(_invincibilityTime);
        }

        /// <summary>
        /// Move player
        /// </summary>
        /// <param name="direction">Direction (-1 = left | 1 = right)</param>
        public void Move(int direction)
        {
            Console.SetCursorPosition(_posX, _posY);
            Console.Write(" ");

            // Check if player is in the right border
            if (_posX >= Console.WindowWidth - 1) 
            {
                _isPosLimitRight = true;    
            }

            // Check if player is not in the right border
            else if (_posX == Console.WindowWidth - 2) 
            {
                _isPosLimitRight = false;
            }

            // Check if player is in the left border
            if (_posX <= 0) 
            {
                _isPosLimitLeft = true; 
            }

            // Check if player is in the left border
            else if (_posX == 1)    
            {
                _isPosLimitLeft = false;
            }

            // Player move to the right or left if he's not in the limits
            if (direction == 1 && _isPosLimitRight == false)
            {
                _posX += direction;
            }

            else if (direction == -1 && _isPosLimitLeft == false)
            {
                _posX += direction;
            }

            Console.SetCursorPosition(_posX, _posY);
            Console.Write(_playerChar);
        }       
    }
}
