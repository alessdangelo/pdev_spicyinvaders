﻿/*
	ETML
	Date: 11.09.20
	Auteur: Manuel Oro
	Description: Shoot class. Shoot can move and destroyed.
	Modifié le: 04.12.20
*/
using System;
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
    }
}
