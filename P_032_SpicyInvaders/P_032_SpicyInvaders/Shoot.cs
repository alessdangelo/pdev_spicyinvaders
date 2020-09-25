﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P_032_SpicyInvaders
{
    class Shoot
    {
        private int _posX;
        private int _posY;
        private int _tempPosX;
        private int _tempPosY;
        private static int _speed;
        private Thread missileLaunch;

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


        public Shoot(int x, int y, int speed, int direction)
        {
            this._posX = x;
            this._posY = y;
            _speed = speed;
            missileLaunch = new Thread(delegate() { LaunchMissile(direction); });
            missileLaunch.Start();
        }

        private void LaunchMissile(int direction)
        {
            if(direction == 0)
            {
                while (_posY != Console.WindowHeight - 5)
                {
                    _posY++;
                    _tempPosX = _posX;
                    _tempPosY = _posY;
                    Console.SetCursorPosition(_posX, _posY);
                    Console.Write("■");
                    WaitToFire();
                    Console.SetCursorPosition(_tempPosX--, _tempPosY--);
                    Console.Write(" ");
                }
            }
            else if(direction == 1)
            {
                while (_posY != Console.WindowTop + 10)
                {
                    _posY--;
                    _tempPosX = _posX;
                    _tempPosY = _posY;
                    Console.SetCursorPosition(_posX, _posY);
                    Console.Write("■");
                    WaitToFire();
                    Console.SetCursorPosition(_tempPosX++, _tempPosY++);
                    Console.Write(" ");
                }
            }
            Program.canShoot = true;
            GC.Collect();
        }

        private static void WaitToFire()
        {
            Thread.Sleep(_speed);
        }

        public void DestroyBullet()
        {
            GC.Collect();
        }
    }
}