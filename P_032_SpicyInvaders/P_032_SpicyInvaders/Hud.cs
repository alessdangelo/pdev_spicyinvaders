﻿/*
	ETML
	Date: 25.09.20
	Auteur: Manuel Oro
	Description: Display infos about player: lifes, score and keybinds
	Modifié le: 04.12.20
*/
using System;

namespace P_032_SpicyInvaders
{
    /// <summary>
    /// Class hud
    /// </summary>
    public static class Hud
    {

        /// <summary>
        /// Print all Infos
        /// </summary>
        public static void PrintAllInfos(int score, int lives)
        {
            Console.SetCursorPosition(3, 3);
            Console.Write("<: Gauche");
            Console.SetCursorPosition(3, 4);
            Console.Write(">: Droite");
            Console.SetCursorPosition(3, 5);
            Console.Write("Espace: Tir");
            Console.SetCursorPosition(3, 6);
            Console.Write("Escape: Pause");

            PrintPlayerLifes(lives);
            PrintPlayerScore(score);
        }

        /// <summary>
        /// Print player score
        /// </summary>
        /// <param name="score">Player score</param>
        public static void PrintPlayerScore(int score)
        {
            Console.SetCursorPosition(65, 3);
            Console.WriteLine("Score: {0}", score);
        }

        /// <summary>
        /// Print player lifes
        /// </summary>
        public static void PrintPlayerLifes(int lives)
        {
            Console.SetCursorPosition(35, 3);
            Console.Write("      ");
            Console.SetCursorPosition(35, 3);

            for (int i = 0; i < lives; i++)
            {
                Console.Write("♥ ");
            }
        }
    }
}
