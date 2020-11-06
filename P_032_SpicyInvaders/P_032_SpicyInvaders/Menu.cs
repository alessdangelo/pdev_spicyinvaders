///   ETML 
/// 
///   Auteur       : Bruno Martins Constantino
///   Date         : 28.08.2020
///   Modification : 11.09.2020
///   Descrption   : Main menu of the Spicy Invaders.

using System;
using System.IO;

namespace P_032_SpicyInvaders
{
    /// <summary>
    /// Class Menu
    /// </summary>
    class Menu
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Menu()
        {
            MainMenu();
        }

        /// <summary>
        /// Prints the main menu
        /// </summary>
        public static void MainMenu()
        {
            ///Variables
            const int WINDOWSIZEX = 90;
            const int WINDOWSIZEY = 35;
            const int SPICYXAXETITLE = 28;
            const int INVADERSXAXETITLE = 15;

            const string SPICYLINEONE = "███████ ██████  ██  ██████ ██    ██";
            const string SPICYLINETWO = "██      ██   ██ ██ ██       ██  ██ ";
            const string SPICYLINETHREE = "███████ ██████  ██ ██        ████  ";
            const string SPICYLINEFOUR = "     ██ ██      ██ ██         ██   ";
            const string SPICYLINEFIVE = "███████ ██      ██  ██████    ██   ";

            const string INVADERSLINEONE = "██ ███    ██ ██    ██  █████  ██████  ███████ ██████  ███████";
            const string INVADERSLINETWO = "██ ████   ██ ██    ██ ██   ██ ██   ██ ██      ██   ██ ██     ";
            const string INVADERSLINETHREE = "██ ██ ██  ██ ██    ██ ███████ ██   ██ █████   ██████  ███████";
            const string INVADERSLINEFOUR = "██ ██  ██ ██  ██  ██  ██   ██ ██   ██ ██      ██   ██      ██";
            const string INVADERSLINEFIVE = "██ ██   ████   ████   ██   ██ ██████  ███████ ██   ██ ███████";

            const string PLAY = "1) JOUER";
            const string OPTIONS = "2) OPTIONS";
            const string HIGHSCORE = "3) HIGHSCORE";
            const string INFOS = "4) INFOS";
            const string LEAVE = "5) QUITTER";

            const int ENDLEAVEXPOSITION = 49;
            const int LEAVEYPOSITION = 28;

            int positionXSubMenu = 40;
            int positionYSubMenu = 20;

            int spicyYAxeTitle = 1;
            int invadersYAxeTitle = 7;

            ConsoleKeyInfo keyPressed;
            bool continueKey = false;

            ///Main program

            Console.Clear();

            Console.SetWindowSize(WINDOWSIZEX, WINDOWSIZEY);
            Console.SetBufferSize(WINDOWSIZEX, WINDOWSIZEY);

            Console.SetCursorPosition(SPICYXAXETITLE, spicyYAxeTitle);

            Console.CursorVisible = false;

            Console.ForegroundColor = ConsoleColor.Red;

            //Writing the "SPICY" game title
            Console.WriteLine(SPICYLINEONE);
            spicyYAxeTitle++;
            Console.SetCursorPosition(SPICYXAXETITLE, spicyYAxeTitle);
            Console.WriteLine(SPICYLINETWO);
            spicyYAxeTitle++;
            Console.SetCursorPosition(SPICYXAXETITLE, spicyYAxeTitle);
            Console.WriteLine(SPICYLINETHREE);
            Console.SetCursorPosition(SPICYXAXETITLE, spicyYAxeTitle);
            spicyYAxeTitle++;
            Console.SetCursorPosition(SPICYXAXETITLE, spicyYAxeTitle);
            Console.WriteLine(SPICYLINEFOUR);
            spicyYAxeTitle++;
            Console.SetCursorPosition(SPICYXAXETITLE, spicyYAxeTitle);
            Console.WriteLine(SPICYLINEFIVE);

            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkMagenta;

            Console.SetCursorPosition(INVADERSXAXETITLE, invadersYAxeTitle);

            //Writing the "INVADERS" game title
            Console.WriteLine(INVADERSLINEONE);
            invadersYAxeTitle++;
            Console.SetCursorPosition(INVADERSXAXETITLE, invadersYAxeTitle);
            Console.WriteLine(INVADERSLINETWO);
            invadersYAxeTitle++;
            Console.SetCursorPosition(INVADERSXAXETITLE, invadersYAxeTitle);
            Console.WriteLine(INVADERSLINETHREE);
            Console.SetCursorPosition(INVADERSXAXETITLE, invadersYAxeTitle);
            invadersYAxeTitle++;
            Console.SetCursorPosition(INVADERSXAXETITLE, invadersYAxeTitle);
            Console.WriteLine(INVADERSLINEFOUR);
            invadersYAxeTitle++;
            Console.SetCursorPosition(INVADERSXAXETITLE, invadersYAxeTitle);
            Console.WriteLine(INVADERSLINEFIVE);

            Console.ResetColor();

            //Moving the cursor to write the line
            Console.SetCursorPosition(10, 16);
            Console.Write("Appuyez sur '1', '2', '3', '4' ou '5' selon ce que vous voulez accéder.");

            //Moving the cursor to write the SubMenu
            Console.SetCursorPosition(positionXSubMenu, positionYSubMenu);

            //Writing the sub menu
            Console.WriteLine(PLAY);
            positionYSubMenu += 2;
            Console.SetCursorPosition(positionXSubMenu -= 1, positionYSubMenu);
            Console.WriteLine(OPTIONS);
            positionYSubMenu += 2;
            Console.SetCursorPosition(positionXSubMenu -= 1, positionYSubMenu);
            Console.WriteLine(HIGHSCORE);
            Console.SetCursorPosition(positionXSubMenu, positionYSubMenu);
            positionYSubMenu += 2;
            Console.SetCursorPosition(positionXSubMenu += 2, positionYSubMenu);
            Console.WriteLine(INFOS);
            positionYSubMenu += 2;
            Console.SetCursorPosition(positionXSubMenu -= 1, positionYSubMenu);
            Console.WriteLine(LEAVE);

            Console.SetCursorPosition(ENDLEAVEXPOSITION, LEAVEYPOSITION);

            //The player can only press 1,2,3,4,5,ESC on the menu
            while (!continueKey)
            {
                keyPressed = Console.ReadKey(true);
                //Sub menu movement
                switch (keyPressed.Key)
                {
                    case ConsoleKey.D1:
                        PlayGame();
                        break;

                    case ConsoleKey.D2:
                        GameOptions();
                        break;

                    case ConsoleKey.D3:
                        GameHighscore();
                        break;

                    case ConsoleKey.D4:
                        Infos();
                        break;

                    case ConsoleKey.D5:
                        Environment.Exit(1);
                        break;

                    case ConsoleKey.Escape:
                        Environment.Exit(1);
                        break;

                    default:

                        Console.Write(" ");
                        continueKey = false;
                        break;
                }
            }

        }

        /// <summary>
        /// Spacy Invaders game
        /// </summary>
        public static void PlayGame()
        {
            
            Console.Clear();
            Program.RunAll();
        }

        /// <summary>
        /// Prints the game options
        /// </summary>
        public static void GameOptions()
        {
            //Variables
            const int WINDOWSIZEX = 90;
            const int WINDOWSIZEY = 35;
            const int OPTIONSXAXETITLE = 17;

            const string OPTIONSLINEONE = " ██████  ██████  ████████ ██  ██████  ███    ██ ███████ ";
            const string OPTIONSLINETWO = "██    ██ ██   ██    ██    ██ ██    ██ ████   ██ ██      ";
            const string OPTIONSLINETHREE = "██    ██ ██████     ██    ██ ██    ██ ██ ██  ██ ███████ ";
            const string OPTIONSLINEFOUR = "██    ██ ██         ██    ██ ██    ██ ██  ██ ██      ██ ";
            const string OPTIONSLINEFIVE = " ██████  ██         ██    ██  ██████  ██   ████ ███████ ";

            int optionsYAxeTitle = 1;
            int index = 0;

            ConsoleKeyInfo keyPressed;
            bool continueKey = false;

            ///Main program

            Console.Clear();

            Console.SetWindowSize(WINDOWSIZEX, WINDOWSIZEY);
            Console.SetBufferSize(WINDOWSIZEX, WINDOWSIZEY);

            Console.CursorVisible = false;

            Console.SetCursorPosition(OPTIONSXAXETITLE, optionsYAxeTitle);

            //Writing the "OPTIONS"
            for (int i = 0; i != 1; i++)
            {
                Console.WriteLine(OPTIONSLINEONE);
                optionsYAxeTitle++;
                Console.SetCursorPosition(OPTIONSXAXETITLE, optionsYAxeTitle);
                Console.WriteLine(OPTIONSLINETWO);
                optionsYAxeTitle++;
                Console.SetCursorPosition(OPTIONSXAXETITLE, optionsYAxeTitle);
                Console.WriteLine(OPTIONSLINETHREE);
                Console.SetCursorPosition(OPTIONSXAXETITLE, optionsYAxeTitle);
                optionsYAxeTitle++;
                Console.SetCursorPosition(OPTIONSXAXETITLE, optionsYAxeTitle);
                Console.WriteLine(OPTIONSLINEFOUR);
                optionsYAxeTitle++;
                Console.SetCursorPosition(OPTIONSXAXETITLE, optionsYAxeTitle);
                Console.WriteLine(OPTIONSLINEFIVE);

            }

            WriteOptions(index);


            Console.SetCursorPosition(21, 30);
            Console.Write("Appuyez sur ESC pour revenir au menu principal...");

            while (!continueKey)
            {
                keyPressed = Console.ReadKey(true);
                //Sub menu movement
                switch (keyPressed.Key)
                {
                    case ConsoleKey.UpArrow:
                        index = 0;
                        WriteOptions(index);
                        break;

                    case ConsoleKey.DownArrow:
                        index = 1;
                        WriteOptions(index);
                        break;

                    case ConsoleKey.Enter:
                        if(index == 0)
                        {
                            if (Program.soundOn)
                            {
                                Program.soundOn = false;
                                WriteOptions(index);
                            }
                            else
                            {
                                Program.soundOn = true;
                                WriteOptions(index);
                            }
                        }
                        else if (index == 1)
                        {
                            if(Program.difficulty == 0)
                            {
                                Program.difficulty = 1;
                                WriteOptions(index);
                            }
                            else
                            {
                                Program.difficulty = 0;
                                WriteOptions(index);
                            }
                        }
                        break;

                    case ConsoleKey.Escape:
                        MainMenu();
                        break;

                    default:

                        Console.Write(" ");
                        continueKey = false;
                        break;
                }
            }
        }

        /// <summary>
        /// Write sound and difficulty options
        /// </summary>
        /// <param name="index">Get selected options (sound or difficulty)      </param>
        public static void WriteOptions(int index)
        {
            Console.SetCursorPosition(35, 15);
            if(index == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            if (Program.soundOn)
            {
                Console.Write("Son actif: Oui");
            }
            else
            {
                Console.Write("Son actif: Non");
            }

            Console.ResetColor();

            if (index == 1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            Console.SetCursorPosition(35, 18);
            if(Program.difficulty == 0)
            {
                Console.WriteLine("Difficulté: Facile   ");
            }
            else
            {
                Console.WriteLine("Difficulté: Difficile");
            }
            Console.ResetColor();
        }
        /// <summary>
        /// Show the highscore
        /// </summary>
        public static void GameHighscore()
        {
            //Variables
            const int WINDOWSIZEX = 90;
            const int WINDOWSIZEY = 35;
            const int HIGHSCOREXAXETITLE = 11;

            const string HIGHSCORELINEONE = "██   ██ ██  ██████  ██   ██ ███████  ██████  ██████  ██████  ███████";
            const string HIGHSCORELINETWO = "██   ██ ██ ██       ██   ██ ██      ██      ██    ██ ██   ██ ██     ";
            const string HIGHSCORELINETHREE = "███████ ██ ██   ███ ███████ ███████ ██      ██    ██ ██████  █████  ";
            const string HIGHSCORELINEFOUR = "██   ██ ██ ██    ██ ██   ██      ██ ██      ██    ██ ██   ██ ██     ";
            const string HIGHSCORELINEFIVE = "██   ██ ██  ██████  ██   ██ ███████  ██████  ██████  ██   ██ ███████";

            int highscoreYAxeTitle = 1;

            ConsoleKeyInfo keyPressed;
            bool continueKey = false;

            ///Main program

            Console.Clear();

            Console.SetWindowSize(WINDOWSIZEX, WINDOWSIZEY);
            Console.SetBufferSize(WINDOWSIZEX, WINDOWSIZEY);

            Console.CursorVisible = false;

            Console.SetCursorPosition(HIGHSCOREXAXETITLE, highscoreYAxeTitle);

            //Writing the "HIGHSCORE"
            for (int i = 0; i != 1; i++)
            {
                Console.WriteLine(HIGHSCORELINEONE);
                highscoreYAxeTitle++;
                Console.SetCursorPosition(HIGHSCOREXAXETITLE, highscoreYAxeTitle);
                Console.WriteLine(HIGHSCORELINETWO);
                highscoreYAxeTitle++;
                Console.SetCursorPosition(HIGHSCOREXAXETITLE, highscoreYAxeTitle);
                Console.WriteLine(HIGHSCORELINETHREE);
                Console.SetCursorPosition(HIGHSCOREXAXETITLE, highscoreYAxeTitle);
                highscoreYAxeTitle++;
                Console.SetCursorPosition(HIGHSCOREXAXETITLE, highscoreYAxeTitle);
                Console.WriteLine(HIGHSCORELINEFOUR);
                highscoreYAxeTitle++;
                Console.SetCursorPosition(HIGHSCOREXAXETITLE, highscoreYAxeTitle);
                Console.WriteLine(HIGHSCORELINEFIVE);

            }

            // read highscore in txt file
            string highsore = $"Votre meilleur score est de: " + File.ReadAllText(Environment.CurrentDirectory + "/highscore.txt");
            Console.SetCursorPosition((Console.WindowWidth)/2-(highsore.Length/2), 20);
            Console.Write(highsore);

            Console.SetCursorPosition(21, 30);
            Console.Write("Appuyez sur ESC pour revenir au menu principal...");

            while (!continueKey)
            {
                keyPressed = Console.ReadKey(true);
                //Sub menu movement
                switch (keyPressed.Key)
                {

                    case ConsoleKey.Escape:
                        MainMenu();
                        break;

                    default:

                        Console.Write(" ");
                        continueKey = false;
                        break;
                }
            }
        }

        /// <summary>
        /// Prints the infos
        /// </summary>
        public static void Infos()
        {
            //Variables
            const int WINDOWSIZEX = 90;
            const int WINDOWSIZEY = 35;
            const int DEVELOPPEDXAXETITLE = 5;
            const int BYXAXETITLE = 38;

            const string DEVELOPPEDLINEONE = "██████  ███████ ██    ██ ███████ ██       ██████  ██████  ██████  ███████ ██████  ";
            const string DEVELOPPEDLINETWO = "██   ██ ██      ██    ██ ██      ██      ██    ██ ██   ██ ██   ██ ██      ██   ██ ";
            const string DEVELOPPEDLINETHREE = "██   ██ █████   ██    ██ █████   ██      ██    ██ ██████  ██████  █████   ██   ██ ";
            const string DEVELOPPEDLINEFOUR = "██   ██ ██       ██  ██  ██      ██      ██    ██ ██      ██      ██      ██   ██ ";
            const string DEVELOPPEDLINEFIVE = "██████  ███████   ████   ███████ ███████  ██████  ██      ██      ███████ ██████  ";

            const string BYLINEONE = "██████  ██    ██ ";
            const string BYLINETWO = "██   ██  ██  ██  ";
            const string BYLINETHREE = "██████    ████   ";
            const string BYLINEFOUR = "██   ██    ██    ";
            const string BYLINEFIVE = "██████     ██    ";

            const string DEVONE = "Bruno Martins Constantino";
            const string DEVTWO = "Manuel Oro";
            const string DEVTHREE = "Alessandro D'Angelo";
            const string DEVFOUR = "Clément Sartoni";

            int positionXDeveloppers = 33;
            int positionYDeveloppers = 18;

            int developpedYAxeTitle = 1;
            int byYAxeTitle = 8;


            ConsoleKeyInfo keyPressed;
            bool continueKey = false;

            ///Main program

            Console.Clear();

            Console.SetWindowSize(WINDOWSIZEX, WINDOWSIZEY);
            Console.SetBufferSize(WINDOWSIZEX, WINDOWSIZEY);

            Console.CursorVisible = false;

            Console.SetCursorPosition(DEVELOPPEDXAXETITLE, developpedYAxeTitle);

            //Writing the "DEVELOPPED"
            for (int i = 0; i != 1; i++)
            {
                Console.WriteLine(DEVELOPPEDLINEONE);
                developpedYAxeTitle++;
                Console.SetCursorPosition(DEVELOPPEDXAXETITLE, developpedYAxeTitle);
                Console.WriteLine(DEVELOPPEDLINETWO);
                developpedYAxeTitle++;
                Console.SetCursorPosition(DEVELOPPEDXAXETITLE, developpedYAxeTitle);
                Console.WriteLine(DEVELOPPEDLINETHREE);
                Console.SetCursorPosition(DEVELOPPEDXAXETITLE, developpedYAxeTitle);
                developpedYAxeTitle++;
                Console.SetCursorPosition(DEVELOPPEDXAXETITLE, developpedYAxeTitle);
                Console.WriteLine(DEVELOPPEDLINEFOUR);
                developpedYAxeTitle++;
                Console.SetCursorPosition(DEVELOPPEDXAXETITLE, developpedYAxeTitle);
                Console.WriteLine(DEVELOPPEDLINEFIVE);

            }

            Console.SetCursorPosition(BYXAXETITLE, byYAxeTitle);

            //Writing the "BY"
            for (int i = 0; i != 1; i++)
            {
                Console.WriteLine(BYLINEONE);
                byYAxeTitle++;
                Console.SetCursorPosition(BYXAXETITLE, byYAxeTitle);
                Console.WriteLine(BYLINETWO);
                byYAxeTitle++;
                Console.SetCursorPosition(BYXAXETITLE, byYAxeTitle);
                Console.WriteLine(BYLINETHREE);
                Console.SetCursorPosition(BYXAXETITLE, byYAxeTitle);
                byYAxeTitle++;
                Console.SetCursorPosition(BYXAXETITLE, byYAxeTitle);
                Console.WriteLine(BYLINEFOUR);
                byYAxeTitle++;
                Console.SetCursorPosition(BYXAXETITLE, byYAxeTitle);
                Console.WriteLine(BYLINEFIVE);

            }

            Console.SetCursorPosition(positionXDeveloppers, positionYDeveloppers);

            //Writing the sub menu
            for (int i = 0; i != 1; i++)
            {
                Console.WriteLine(DEVONE);
                positionYDeveloppers += 2;
                Console.SetCursorPosition(positionXDeveloppers += 7, positionYDeveloppers);
                Console.WriteLine(DEVTWO);
                positionYDeveloppers += 2;
                Console.SetCursorPosition(positionXDeveloppers -= 4, positionYDeveloppers);
                Console.WriteLine(DEVTHREE);
                Console.SetCursorPosition(positionXDeveloppers, positionYDeveloppers);
                positionYDeveloppers += 2;
                Console.SetCursorPosition(positionXDeveloppers += 2, positionYDeveloppers);
                Console.WriteLine(DEVFOUR);
            }

            Console.SetCursorPosition(22, 30);
            Console.Write("Appuyez sur ESC pour revenir au menu principal...");

            while (!continueKey)
            {
                keyPressed = Console.ReadKey(true);
                //Sub menu movement
                switch (keyPressed.Key)
                {

                    case ConsoleKey.Escape:
                        MainMenu();
                        break;

                    default:

                        Console.Write(" ");
                        continueKey = false;
                        break;
                }
            }
        }
    }
}
