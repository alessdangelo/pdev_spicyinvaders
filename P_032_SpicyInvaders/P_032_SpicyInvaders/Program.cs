using System;
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
        public static Player ship = new Player(39, 45, 3);
        public static bool canShoot = true;

        static void Hud()
        {
            int score = 0000;

            Console.SetWindowSize(80, 50);
            Console.SetBufferSize(80, 50);
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(3, 3);
            Console.Write("<: Gauche");
            Console.SetCursorPosition(3, 4);
            Console.Write(">: Droite");
            Console.SetCursorPosition(3, 5);
            Console.Write("Espace: Tir");

            Console.SetCursorPosition(35, 3);
            for (int i = 0; i < ship.Life; i++)
            {
                Console.Write("♥ ");
            }

            Console.SetCursorPosition(65, 3);
            Console.WriteLine("Score: {0}", score);
        }

        static void Main(string[] args)
        {
            var music = new System.Media.SoundPlayer();
            music.SoundLocation = fileToPlay; // Breakpoint here to see what fileToPlay is
            //music.SoundLocation = @"F:\01-projets\032-p_spicey\P_032_SpicyInvaders\song.wav";
            music.PlayLooping();

            Hud();
            Enemy enemy = new Enemy(25, 40, 1000);

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
                        if(canShoot)
                        {
                            Shoot bullet = new Shoot(ship.PosX, ship.PosY - 1, 200, 1);
                            canShoot = false;
                        }
                        break;
                }
            } while (gameOver == false);
        }

        public static void ShootBulletFromEnemy(int x, int y)
        {
            Shoot shoot = new Shoot(x, y, 200, 0);
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
