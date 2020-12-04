/*
	ETML
	Date: 11.09.20
	Auteur: Manuel Oro
	Description: Enemy class. Enemy can move, shoot and be destroyed
	Modifié le: 20.11.20
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
        /// Custom constructor
        /// </summary>
        /// <param name="posX">Spawn enemy at position x</param>
        /// <param name="posY">Spawn enemy at position y</param>
        public Enemy(int posX, int posY)
        {
            _posX = posX;
            _posY = posY;
            Sprite = '*';
        }

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="posX">Spawn enemy at position x</param>
        /// <param name="posY">Spawn enemy at position y</param>
        /// <param name="ennemyCharacter">Character displayed (represent the enemy)</param>
        public Enemy(int posX, int posY, char ennemyCharacter = '*')
        {
            _posX = posX;
            _posY = posY;
            Sprite = ennemyCharacter;
        }


        /// <summary>
        /// Move enemy
        /// </summary>
        /// <param name="direction">Select in which direction enemy goes</param>
        public void Move(int[] direction)
        {
            if (IsAlive)
            {
                Console.SetCursorPosition(_posX, _posY);
                Console.Write(" ");
                _posX += direction[0];
                _posY += direction[1];
                Console.SetCursorPosition(_posX, _posY);
                Console.Write(Sprite);
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
            //Program._ship.Score += 100;
            //Program._hud.PrintPlayerScore();
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Enemy()
        {

        }
    }
}
