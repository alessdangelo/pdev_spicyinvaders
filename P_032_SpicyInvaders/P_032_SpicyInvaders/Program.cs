/*
 * ETML
 * Auteurs: Bruno Martins Constantino, Manuel Oro, Alessandro D'Angelo, Clément Sartoni
 * Description: Spicy Invaders program class, contains the overall architecture of the game and the game thread containing the checks. 
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

        // Objects from class
        private static Menu _menu;

        /// <summary>
        /// Main method: Display Menu
        /// </summary>
        static void Main()
        {
            _menu = new Menu();
            _menu.MainMenu();
        }

        /// <summary>
        /// Run game
        /// </summary>
        public static void RunGame()
        {
            System.GC.Collect();
            Game game = new Game(_menu);
            game.RunGame();
        }
    }
}
