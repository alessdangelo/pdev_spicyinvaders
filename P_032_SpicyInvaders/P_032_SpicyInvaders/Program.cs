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
        public static Player ship = new Player(39, 45, 3);
        public static Enemy[,] enemiesArray = new Enemy[5, 2];
        public static Thread moveEnnemys;
        public static int[] enemiesSpawnPoint = {Console.WindowWidth/2-enemiesArray.GetLength(0)/2, Console.WindowHeight/2- enemiesArray.GetLength(1)/2 };
        public static bool gameOver = false;
        public static bool canShoot = true;
        public static Random random = new Random();

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
            Hud();

            //initialisation des ennemis
            for(int y = 0; y<enemiesArray.GetLength(1);y++)
            {
                for (int x = 0; x < enemiesArray.GetLength(0); x++)
                {
                    enemiesArray[x, y] = new Enemy(enemiesSpawnPoint[0] + (5 * x), enemiesSpawnPoint[1] + (1 * y), 200);
                }
            }

            moveEnnemys = new Thread(MoveEnnemys);
            moveEnnemys.Start();


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
                            Shoot bullet = new Shoot(ship.PosX, ship.PosY - 1, 100, 1);
                            canShoot = false;
                        }
                        break;
                }
            } while (gameOver == false);
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

        static public void MoveEnnemys()
        {
            int[] direction = new int[] { -1, 0 }; //la direction du pack en [x,y]
            int[] enemiesLimits = { 5, Console.WindowWidth - 5, enemiesSpawnPoint[1] - 6, enemiesSpawnPoint[1] + 6 }; //les limites du déplacemenmt, en [xMin, xMax, yMin, yMax]
            while (!gameOver)
            {
                foreach (Enemy ennemy in enemiesArray)
                {
                    if(ennemy.IsAlive)
                    {
                        /*if (random.Next(ennemy.ShootProbability) == 1)
                        {
                            ShootBulletFromEnemy(ennemy.PosX, ennemy.PosY);
                        }*/
                        ennemy.Move(direction);
                    } 
                }
                if (enemiesArray[0, 0].PosX + direction[0] <= enemiesLimits[0])
                {
                    direction = new int[] { 0, 1 };
                }
                if (enemiesArray[enemiesArray.GetLength(0) - 1, 0].PosX + direction[0] >= enemiesLimits[1])
                {
                    direction = new int[] { 0, -1 };
                }
                if (enemiesArray[0, 0].PosY + direction[1] <= enemiesLimits[2])
                {
                    direction = new int[] { -1, 0 };
                }
                if (enemiesArray[0, enemiesArray.GetLength(1) - 1].PosY + direction[1] >= enemiesLimits[3])
                {
                    direction = new int[] { 1, 0 };
                }
                Thread.Sleep(100);
            }
        }
    }
}
