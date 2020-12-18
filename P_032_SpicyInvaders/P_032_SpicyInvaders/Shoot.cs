/*
	ETML
	Date: 11.09.20
	Auteur: Manuel Oro
	Description: Shoot class. Shoot can move and destroyed.
	Modifié le: 18.12.20
*/
using System;
using System.Collections.Generic;

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
        private readonly int _direction;
        private static int _bulletSpeed;

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="x">Bullet x position</param>
        /// <param name="y">Bullet y position</param>
        /// <param name="direction">Bullet direction (up = -1 or down = 1)</param>
        /// <param name="sprite">Bullet sprite (char to display)</param>
        /// <param name="bulletSpeed">Bullet speed</param>
        public Shoot(int x, int y, int direction, char sprite = '|', int bulletSpeed = 25)
        {
            this._posX = x;
            this._posY = y;
            _bulletSpeed = bulletSpeed;
            this.Sprite = sprite;
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
            Game._bullets.Remove(this);
        }

        /// <summary>
        /// Move all bullets
        /// </summary>
        /// <param name="bulletMove">Freq of bullets move</param>
        /// <param name="bullets">Bullets list</param>
        public static void MoveBullets(ref DateTime bulletMove, ref List<Shoot> bullets)
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
                    }
                }
            }
        }
    }
}
