using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_032_SpicyInvaders
{
    class Program
    {
        static void Main(string[] args)
        {
            Player ship = new Player();
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

                        break;
                }
            } while (gameOver == false);
        }
    }
}
