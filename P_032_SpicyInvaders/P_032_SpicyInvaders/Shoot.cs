using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P_032_SpicyInvaders
{
    class Shoot
    {
        private static int _posX;
        private static int _posY;
        private static int _direction;

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


        public Shoot(int x, int y, int direction)
        {
            _posX = x;
            _posY = y;
            _direction = direction;
        }

        public void Move()
        {
            Program.canShoot = false;

            Console.SetCursorPosition(_posX, _posY);
            Console.Write(" ");
            _posY += _direction;
            Console.SetCursorPosition(_posX, _posY);
            Console.Write("■");

        }

        public void DestroyBullet()
        {
            Console.SetCursorPosition(_posX, _posY);
            Console.Write(" ");
            Program.bullets.Remove(this);
        }

        ~Shoot()
        {
            Debug.WriteLine("Destructor");
        }
    }
}
