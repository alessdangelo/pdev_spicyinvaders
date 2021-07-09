/*
	ETML
	Date: 11.09.20
	Auteur: Manuel Oro, Alessandro D'Angelo
	Description: Player class. Player can move.
	Modifié le: 14.12.20
*/
using System;

namespace P_032_SpicyInvaders
{
    /// <summary>
    /// Class Player
    /// </summary>
    public class Player : Entity
    {
        //private class fields
        /// <summary>
        /// Player's Life
        /// </summary>
        private int _life = 3;

        /// <summary>
        /// Player's score
        /// </summary>
        private int _score = 0;

        /// <summary>
        /// Bool used to know if we are at the right limit
        /// </summary>
        private bool _isPosLimitRight = false;

        /// <summary>
        /// Bool used to know if we are at the left limit
        /// </summary>
        private bool _isPosLimitLeft = false;

        /// <summary>
        /// DateTime used to allow a temporal invincibility to the player when he's hit
        /// </summary>
        private static DateTime _tempInvincibility = new DateTime();

        /// <summary>
        /// the duration of the temporal invincibility 
        /// </summary>
        private readonly double _invincibilityTime;

        //Properties
        /// <summary>
        /// Player Life's Property
        /// </summary>
        public int Life
        {
            get { return _life; }
            set { _life = value; }
        }

        /// <summary>
        /// Player's score property
        /// </summary>
        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        /// <summary>
        /// Player's temp invincibility property
        /// </summary>
        public DateTime TempInvicibility
        {
            get { return _tempInvincibility; }
            set { _tempInvincibility = value; }
        }

        //methods
        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="x">X position in console</param>
        /// <param name="y">Y position in console</param>
        /// <param name="life">Player lifes</param>
        public Player(int x, int y, int life, double invincibilityTime)
        {
            this._posX = x;
            this._posY = y;
            this._life = life;
            this._invincibilityTime = invincibilityTime;
            Sprite = '♠';

            Console.SetCursorPosition(_posX, _posY);
            Console.Write(Sprite);
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

            // Player can move to the right or left if he's not in the limits
            if ((direction == 1 && _isPosLimitRight == false) || (direction == -1 && _isPosLimitLeft == false))
            {
                _posX += direction;
            }

            Console.SetCursorPosition(_posX, _posY);

            //invicibility system
            if (DateTime.Now < TempInvicibility)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write(Sprite);
        }
    }
}
