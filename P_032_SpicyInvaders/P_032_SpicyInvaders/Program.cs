﻿/*
 * ETML
 * Auteurs: Bruno Martins Constantino, Manuel Oro, Alessandro D'Angelo, Clément Sartoni
 * Description: Spicy Invaders program class, contains the overall architecture of the game and the game thread containing the checks.
 * Modif. Date: 18.12.20
 */

namespace P_032_SpicyInvaders
{
    /// <summary>
    /// Class Program
    /// </summary>
    class Program
    {
        /// <summary>
        /// Attributes
        /// </summary>
        private static Menu _menu;

        /// <summary>
        /// Main method: Display Menu
        /// </summary>
        static void Main()
        {
            _menu = new Menu();
            _menu.MainMenu();
        } 
    }
}
