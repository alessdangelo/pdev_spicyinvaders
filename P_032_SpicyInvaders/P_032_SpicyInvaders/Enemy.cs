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
    public class Enemy : Entity
    {
        /// <summary>
        /// Attributes
        /// </summary>
        private bool _isAlive = true;
        private static char _ennemyChar;

        /// <summary>
        /// Properties
        /// </summary>
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

        public Enemy(int posX, int posY,char ennemyCharacter)
        {
            _posX = posX;
            _posY = posY;
            _ennemyChar = ennemyCharacter;
        }


        /// <summary>
        /// Move enemy
        /// </summary>
        /// <param name="direction">Select in which direction enemy goes</param>
        public void Move(int[] direction)
        {
            if(_isAlive)
            {
                Console.SetCursorPosition(_posX, _posY);
                Console.Write(" ");
                _posX += direction[0];
                _posY += direction[1];
                Console.SetCursorPosition(_posX, _posY);
                Console.Write("*");
            }
            else
            {
                _posX += direction[0];
                _posY += direction[1];
            }
        }

        /// <summary>
        /// Remove enemy char
        /// </summary>
        public void DestroyEnemy()
        {
            Console.SetCursorPosition(_posX, _posY);
            Console.Write(" ");
            Program.ship.Score += 100;
            Program.hud.PrintPlayerScore();
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Enemy()
        {

        }
    }
}
