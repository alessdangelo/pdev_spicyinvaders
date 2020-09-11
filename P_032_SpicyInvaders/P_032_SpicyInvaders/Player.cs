using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_032_SpicyInvaders
{
    class Player
    {
        /// <summary>
        /// Attributs
        /// </summary>
        const char player = 'x';
        private int _posX = 20;

        /// <summary>
        /// Properties
        /// </summary>


        /// <summary>
        /// Constructor
        /// </summary>
        public Player() { }

        /// <summary>
        /// Methods
        /// </summary>
        public void moveRight()
        {
            Console.SetCursorPosition(_posX, 20);
            Console.Write(" ");
            if (_posX != 79)
            {
                _posX++;
            }
            Console.SetCursorPosition(_posX, 20);
            Console.Write(player);
        }

        public void moveLeft()
        {
            Console.SetCursorPosition(_posX, 20);
            Console.Write(" ");
            if (_posX != 0)
            {
                _posX--;
            }
            Console.SetCursorPosition(_posX, 20);
            Console.Write(player);
        }
    }
}
