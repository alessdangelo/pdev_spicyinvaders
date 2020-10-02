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
        /// Constructor
        /// </summary>
        public Player(int x, int y, int life)
        {
            this._posX = x;
            this._posY = y;
            this._life = life;

            Console.SetCursorPosition(_posX, _posY);
            Console.Write(player);
        }

        /// <summary>
        /// Methods
        /// </summary>
        public void moveRight()
        {
            Console.SetCursorPosition(_posX, _posY);
            Console.Write(" ");
            if (_posX != Console.WindowWidth-1)
            {
                _posX++;
            }
            Console.SetCursorPosition(_posX, _posY);
            Console.Write(player);
        }

        public void moveLeft()
        {
            Console.SetCursorPosition(_posX, _posY);
            Console.Write(" ");
            if (_posX != 0)
            {
                _posX--;
            }
            Console.SetCursorPosition(_posX, _posY);
            Console.Write(player);
        }
    }
}
