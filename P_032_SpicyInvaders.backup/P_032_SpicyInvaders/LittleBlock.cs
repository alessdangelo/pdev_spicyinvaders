/*
 * ETML
 * Auteur: Clément Sartoni
 * Date: 18.12.2020
 * Description: Class LittleBlock used in the Class Block of the Spicy Invaders Project
 */
using System;

namespace P_032_SpicyInvaders
{
    /// <summary>
    /// Class LittleBlock
    /// </summary>
    public class LittleBlock : Entity
    {
        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="location">Little block location</param>
        public LittleBlock(int posX, int posY)
        {
            Sprite = '█';
            _posX = posX;
            _posY = posY;

            Console.SetCursorPosition(_posX, _posY);
            Console.Write(Sprite);
        }
        /// <summary>
        /// Destroy little block
        /// </summary>
        public void Delete()
        {
            IsAlive = false;
            Console.SetCursorPosition(_posX, _posY);
            Console.Write(' ');
        }
    }
}
