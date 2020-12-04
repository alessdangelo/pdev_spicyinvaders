/*
	ETML
	Date: 11.09.20
	Auteur: Manuel Oro
	Description: Shoot class. Shoot can move and destroyed.
	Modifié le: --
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace P_032_SpicyInvaders
{
    /// <summary>
    /// Class Shoot
    /// </summary>
    public class Shoot : Entity
    {
        /// <summary>
        /// Attributes
        /// </summary>
        private int _direction;
        private readonly static int _bulletSpeed = 25;

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="x">Position x in console</param>
        /// <param name="y">Position y in console</param>
        /// <param name="direction">Direction (-1 Up | 1 Down)</param>
        public Shoot(int x, int y, int direction, char sprite = '|')
        {
            this._posX = x;
            this._posY = y;
            Sprite = sprite;
            this._direction = direction;
        }

        /// <summary>
        /// Move bullet
        /// </summary>
        public void Move()
        {
            Console.SetCursorPosition(_posX, _posY);
            Console.Write(" ");
            _posY += _direction;
            Console.SetCursorPosition(_posX, _posY);
            Console.Write(Sprite);

        }

        /// <summary>
        /// Destroy bullet char
        /// </summary>
        public void DestroyBullet()
        {
            Console.SetCursorPosition(_posX, _posY);
            Console.Write(" ");
            Program._bullets.Remove(this);
        }

        /// <summary>
        /// Move all bullets at the same time
        /// </summary>
        static public void MoveBullets(ref DateTime bulletMove, ref List<Shoot> bullets)
        {
            // wait some time before execute
            if (DateTime.Now.Ticks > bulletMove.Ticks)
            {
                bulletMove = DateTime.Now.AddMilliseconds(_bulletSpeed);
                for (int i = 0; i < bullets.Count; i++)
                {
                    // if bullet is in a specific range then move it, else destroy bullet
                    if (bullets[i].PosY > 10 && bullets[i].PosY < 45)
                    {
                        bullets[i].Move();
                    }
                    else
                    {
                        bullets[i].DestroyBullet();
                        GC.Collect();
                    }
                }
            }
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Shoot()
        {
            Debug.WriteLine("Destructor");
        }
    }
}
