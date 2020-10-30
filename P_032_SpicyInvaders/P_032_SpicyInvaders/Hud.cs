/*
	ETML
	Date: 25.09.20
	Auteur: Manuel Oro
	Description: 
	Modifié le: --
*/
using System;

namespace P_032_SpicyInvaders
{
    class Hud
    {
        /// <summary>
        /// Attributes
        /// </summary>
        private int _windowXSize = 80;
        private int _windowYSize = 50;

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="xConsoleSize">Console x size</param>
        /// <param name="yConsoleSize">Console y size</param>
        public Hud(int xConsoleSize, int yConsoleSize)
        {
            this._windowXSize = xConsoleSize;
            this._windowYSize = yConsoleSize;

            Console.SetWindowSize(_windowXSize, _windowYSize);
            Console.SetBufferSize(_windowXSize, _windowYSize);
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;
            PrintAllInfos();
        }

        /// <summary>
        /// Print all Infos
        /// </summary>
        public void PrintAllInfos()
        {
            Console.SetCursorPosition(3, 3);
            Console.Write("<: Gauche");
            Console.SetCursorPosition(3, 4);
            Console.Write(">: Droite");
            Console.SetCursorPosition(3, 5);
            Console.Write("Espace: Tir");

            PrintPlayerLifes();
            PrintPlayerScore();
        }

        /// <summary>
        /// Print player score
        /// </summary>
        /// <param name="score">Player score</param>
        public void PrintPlayerScore()
        {
            Console.SetCursorPosition(65, 3);
            Console.WriteLine("Score: {0}", Program.ship.Score);
        }

        /// <summary>
        /// Print player lifes
        /// </summary>
        public static void PrintPlayerLifes()
        {
            Console.SetCursorPosition(35, 3);
            Console.Write("      ");
            Console.SetCursorPosition(35, 3);

            for (int i = 0; i < Program.ship.Life; i++)
            {
                Console.Write("♥ ");
            }
        }

        /// <summary>
        /// Print game over screen
        /// </summary>
        public static void PrintGameOver()
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 2);
            Console.Write("GAME OVER !");
        }
    }
}
