﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P_032_SpicyInvaders
{
    class Program
    {
        static string musicFile = "song";
        static string fileToPlay = Environment.CurrentDirectory + $@"\{musicFile}.wav";
        public static Player ship;
        public static bool canShoot = true;
        public static bool soundOn = true;
        public static int difficulty = 0;


        public static void RunAll()
        {
            Console.SetWindowSize(50, 50);
            ship = new Player(39, 45, 3);
            Hud hud = new Hud(80, 50);

            //Music
            var music = new System.Media.SoundPlayer();
            if (soundOn)
            {
                music.SoundLocation = fileToPlay; // Breakpoint here to see what fileToPlay is
                music.PlayLooping();
            }

            Enemy enemy = new Enemy(25, 10, 1000);

            bool gameOver = false;
            ConsoleKeyInfo keyEnterred;
            do
            {
                keyEnterred = Console.ReadKey(true);
                switch (keyEnterred.Key)
                {
                    case ConsoleKey.RightArrow:
                        ship.moveRight();
                        break;

                    case ConsoleKey.LeftArrow:
                        ship.moveLeft();
                        break;

                    case ConsoleKey.Spacebar:
                        if (canShoot)
                        {
                            Shoot bullet = new Shoot(ship.PosX, ship.PosY - 1, 200, 1);
                            canShoot = false;
                        }
                        break;
                }
            } while (gameOver == false);
        }
        static void Main(string[] args)
        {
            Menu menu = new Menu();
        }

        public static void ShootBulletFromEnemy(int x, int y)
        {
            Shoot shoot = new Shoot(x, y, 50, 0);
            while (shoot != null)
            {
                if (ship.PosX == shoot.PosX && ship.PosY == shoot.PosY)
                {
                    shoot.DestroyBullet();
                    ship.Life -= 1;
                }
            }
        }
    }
}
