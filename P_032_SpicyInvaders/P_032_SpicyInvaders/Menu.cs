///   ETML 
/// 
///   Auteur     : Bruno Martins Constantino, Manus
///   Date       : 28.08.2020
///   Modif      : 06.11.2020
///   Descrption : This is the main menu of our Spicy Invaders

using System;
using System.IO;
using System.Threading;
using NAudio.Wave;

namespace P_032_SpicyInvaders
{
    /// <summary>
    /// Class Menu
    /// </summary>
    public class Menu
    {
        // Variables
        private static ConsoleKeyInfo _keyPressed;
        private static bool _continueKey = false;
        const int _WINDOWSIZEX = 90;
        const int _WINDOWSIZEY = 35;
        private static readonly string _selectSound = "Blip_Select";

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Menu()
        {
            Console.SetWindowSize(_WINDOWSIZEX, _WINDOWSIZEY);
            Console.SetBufferSize(_WINDOWSIZEX, _WINDOWSIZEY);

            Console.CursorVisible = false;
        }

        /// <summary>
        /// Display Pause Menu
        /// </summary>
        public void PauseMenu()
        {
            //Sound in the menu
            Program.PlaySound(_selectSound);

            Console.SetCursorPosition(28, 22);
            Console.WriteLine("                      ");
            Console.SetCursorPosition(28, 23);
            Console.WriteLine("      GAME PAUSED     ");
            Console.SetCursorPosition(28, 24);
            Console.WriteLine("                      ");
            Console.SetCursorPosition(28, 25);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("   Retourner au jeu   ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(28, 26);
            Console.WriteLine("                      ");
            Console.SetCursorPosition(28, 27);
            Console.WriteLine("        Quitter       ");
            Console.SetCursorPosition(28, 28);
            Console.WriteLine("                      ");

            int index = 0;
            _continueKey = false;

            while (!_continueKey)
            {
                _keyPressed = Console.ReadKey(true);
                //Sub menu movement
                switch (_keyPressed.Key)
                {
                    case ConsoleKey.UpArrow:
                        Program.PlaySound(_selectSound);
                        index = 0;
                        Console.SetCursorPosition(31, 25);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Retourner au jeu");

                        Console.SetCursorPosition(36, 27);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Quitter");
                        break;
                    case ConsoleKey.DownArrow:
                        Program.PlaySound(_selectSound);
                        index = 1;
                        Console.SetCursorPosition(36, 27);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Quitter");

                        Console.SetCursorPosition(31, 25);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Retourner au jeu");
                        break;
                    case ConsoleKey.Enter:
                        if (index == 1)
                        {
                            Environment.Exit(0);
                        }
                        else
                        {
                            Program.PlaySound(_selectSound);
                            _continueKey = true;
                        }
                        break;
                }
            }
            Console.SetCursorPosition(31, 23);
            Console.WriteLine("                   ");
            Console.SetCursorPosition(31, 25);
            Console.WriteLine("                   ");
            Console.SetCursorPosition(31, 27);
            Console.WriteLine("                   ");
        }

        /// <summary>
        /// Prints the main menu
        /// </summary>
        public void MainMenu()
        {
            // Variables
            const int SPICYXAXETITLE = 28;
            const int INVADERSXAXETITLE = 15;

            const string PLAY = "1) JOUER";
            const string OPTIONS = "2) OPTIONS";
            const string HIGHSCORE = "3) HIGHSCORE";
            const string INFOS = "4) INFOS";
            const string LEAVE = "5) QUITTER";

            const int ENDLEAVEXPOSITION = 49;
            const int LEAVEYPOSITION = 28;
            string[] spicyArray = new string[5]
            {
                "███████ ██████  ██  ██████ ██    ██",
                "██      ██   ██ ██ ██       ██  ██ ",
                "███████ ██████  ██ ██        ████  ",
                "     ██ ██      ██ ██         ██   ",
                "███████ ██      ██  ██████    ██   "
            };
            string[] invadersArray = new string[5]
            {
                "██ ███    ██ ██    ██  █████  ██████  ███████ ██████  ███████",
                "██ ████   ██ ██    ██ ██   ██ ██   ██ ██      ██   ██ ██     ",
                "██ ██ ██  ██ ██    ██ ███████ ██   ██ █████   ██████  ███████",
                "██ ██  ██ ██  ██  ██  ██   ██ ██   ██ ██      ██   ██      ██",
                "██ ██   ████   ████   ██   ██ ██████  ███████ ██   ██ ███████"
            };
            int positionXSubMenu = 40;
            int positionYSubMenu = 20;

            int spicyYAxeTitle = 1;
            int invadersYAxeTitle = 7;

            Console.Clear();

            MenuWindowSize();

            Console.SetCursorPosition(SPICYXAXETITLE, spicyYAxeTitle);

            Console.CursorVisible = false;

            Console.ForegroundColor = ConsoleColor.Red;

            //Writing the "SPICY" game title
            for (int i = 0; i < spicyArray.Length; i++)
            {
                Console.SetCursorPosition(SPICYXAXETITLE, spicyYAxeTitle);
                Console.WriteLine(spicyArray[i]);
                spicyYAxeTitle++;
            }

            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;

            //Writing the "INVADERS" game title
            for (int i = 0; i < invadersArray.Length; i++)
            {
                Console.SetCursorPosition(INVADERSXAXETITLE, invadersYAxeTitle);
                Console.WriteLine(invadersArray[i]);
                invadersYAxeTitle++;
            }
            Console.ResetColor();

            Console.SetCursorPosition(10, 16);
            Console.Write("Appuyez sur '1', '2', '3', '4' ou '5' selon ce que vous voulez accéder.");

            Console.SetCursorPosition(positionXSubMenu, positionYSubMenu);
            //Writing the sub menu
            for (int i = 0; i != 1; i++)
            {
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
            }
            Console.SetCursorPosition(ENDLEAVEXPOSITION, LEAVEYPOSITION);
            MenuSelection();
        }

        /// <summary>
        /// Spacy Invaders game
        /// </summary>
        private void PlayGame()
        {
            Console.Clear();
            Program.RunAll();
        }

        /// <summary>
        /// Prints the game options
        /// </summary>
        private void GameOptions()
        {  
            // Variables
            const int OPTIONSXAXETITLE = 17;
            int optionsYAxeTitle = 1;
            int index = 0;
            string[] optionsArray = new string[5]
            {
                " ██████  ██████  ████████ ██  ██████  ███    ██ ███████ ",
                "██    ██ ██   ██    ██    ██ ██    ██ ████   ██ ██      ",
                "██    ██ ██████     ██    ██ ██    ██ ██ ██  ██ ███████ ",
                "██    ██ ██         ██    ██ ██    ██ ██  ██ ██      ██ ",
                " ██████  ██         ██    ██  ██████  ██   ████ ███████ "
            };

            Console.Clear();

            // Writing the "OPTIONS"
            for (int i = 0; i < optionsArray.Length; i++)
            {
                Console.SetCursorPosition(OPTIONSXAXETITLE, optionsYAxeTitle);
                Console.WriteLine(optionsArray[i]);
                optionsYAxeTitle++;
            }

            WriteOptions(index);

            Console.SetCursorPosition(21, 30);
            Console.Write("Appuyez sur ESC pour revenir au menu principal...");
            while (!_continueKey)
            {
                _keyPressed = Console.ReadKey(true);
                // Sub menu movement
                switch (_keyPressed.Key)
                {
                    case ConsoleKey.UpArrow:
                        Program.PlaySound(_selectSound);
                        index = 0;
                        WriteOptions(index);
                        break;

                    case ConsoleKey.DownArrow:
                        Program.PlaySound(_selectSound);
                        index = 1;
                        WriteOptions(index);
                        break;

                    case ConsoleKey.Enter:
                        Program.PlaySound(_selectSound);
                        if (index == 0)
                        {
                            if (Program._soundOn)
                            {
                                Program._soundOn = false;
                                WriteOptions(index);
                            }
                            else
                            {
                                Program._soundOn = true;
                                WriteOptions(index);
                            }
                        }
                        else if (index == 1)
                        {
                            if (Program._difficulty == 0)
                            {
                                Program._difficulty = 1;
                                WriteOptions(index);
                            }
                            else
                            {
                                Program._difficulty = 0;
                                WriteOptions(index);
                            }
                        }
                        break;

                    case ConsoleKey.Escape:
                        MainMenu();
                        break;
                }
            }
        }

        /// <summary>
        /// Select in where menu you want to go
        /// </summary>
        private void MenuSelection()
        {
            while (!_continueKey)
                {
                    _keyPressed = Console.ReadKey(true);

                //Sub menu movement
                switch (_keyPressed.Key)
                    {
                        case ConsoleKey.D1:
                            Program.PlaySound(_selectSound);
                            PlayGame();
                            break;

                        case ConsoleKey.D2:
                            Program.PlaySound(_selectSound);
                            GameOptions();
                            break;

                        case ConsoleKey.D3:
                            Program.PlaySound(_selectSound);
                            GameHighscore();
                            break;

                        case ConsoleKey.D4:
                            Program.PlaySound(_selectSound);
                            Infos();
                            break;

                        case ConsoleKey.D5:
                            Program.PlaySound(_selectSound);
                            Environment.Exit(1);
                            break;

                        case ConsoleKey.Escape:
                            Environment.Exit(1);
                            break;

                        default:
                            Console.Write(" ");
                            _continueKey = false;
                            break;
                    }
                }
        }

        /// <summary>
        /// Write sound and difficulty options
        /// </summary>
        /// <param name="index">Get selected options (sound or difficulty)</param>
        private static void WriteOptions(int index)
        {
            Console.SetCursorPosition(35, 15);
            if(index == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            if (Program._soundOn)
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

            if(Program._difficulty == 0)
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
        private void GameHighscore()
        {
            //Variables
            string highscore;
            const int HIGHSCOREXAXETITLE = 11;
            int highscoreYAxeTitle = 1;
            string[] highscoreArray = new string[5]
            {
                "██   ██ ██  ██████  ██   ██ ███████  ██████  ██████  ██████  ███████",
                "██   ██ ██ ██       ██   ██ ██      ██      ██    ██ ██   ██ ██     ",
                "███████ ██ ██   ███ ███████ ███████ ██      ██    ██ ██████  █████  ",
                "██   ██ ██ ██    ██ ██   ██      ██ ██      ██    ██ ██   ██ ██     ",
                "██   ██ ██  ██████  ██   ██ ███████  ██████  ██████  ██   ██ ███████"
            };

            Console.Clear();           

            //Writing the "HIGHSCORE"
            for (int i = 0; i < highscoreArray.Length; i++)
            {
                Console.SetCursorPosition(HIGHSCOREXAXETITLE, highscoreYAxeTitle);
                Console.WriteLine(highscoreArray[i]);
                highscoreYAxeTitle++;
            }

            // read highscore in txt file
            highscore = $"Votre meilleur score est de: " + File.ReadAllText(Environment.CurrentDirectory + "/highscore.txt");
            Console.SetCursorPosition((Console.WindowWidth)/2-(highscore.Length/2), 20);
            Console.Write(highscore);

            Console.SetCursorPosition(21, 30);
            Console.Write("Appuyez sur ESC pour revenir au menu principal...");

            BackToMainMenu();
        }

        /// <summary>
        /// Prints the infos
        /// </summary>
        private void Infos()
        {
            // Variables
            const int DEVELOPPEDXAXETITLE = 5;
            const int BYXAXETITLE = 38;
            int positionXDeveloppers = 33;
            int positionYDeveloppers = 18;
            int developpedYAxeTitle = 1;
            int byYAxeTitle = 8;

            string[] byArray = new string[5]
            {
                "██████  ██    ██ ",
                "██   ██  ██  ██  ",
                "██████    ████   ",
                "██   ██    ██    ",
                "██████     ██    "
            };

            string[] developpedArray = new string[5]
            {
                "██████  ███████ ██    ██ ███████ ██       ██████  ██████  ██████  ███████ ██████  ",
                "██   ██ ██      ██    ██ ██      ██      ██    ██ ██   ██ ██   ██ ██      ██   ██ ",
                "██   ██ █████   ██    ██ █████   ██      ██    ██ ██████  ██████  █████   ██   ██ ",
                "██   ██ ██       ██  ██  ██      ██      ██    ██ ██      ██      ██      ██   ██ ",
                "██████  ███████   ████   ███████ ███████  ██████  ██      ██      ███████ ██████  "
            };

            const string DEVONE = "Bruno Martins Constantino";
            const string DEVTWO = "Manuel Oro";
            const string DEVTHREE = "Alessandro D'Angelo";
            const string DEVFOUR = "Clément Sartoni";

            Console.Clear();

            //Writing the "DEVELOPPED"
            for (int i = 0; i < developpedArray.Length; i++)
            {
                Console.SetCursorPosition(DEVELOPPEDXAXETITLE, developpedYAxeTitle);
                Console.WriteLine(developpedArray[i]);
                developpedYAxeTitle++;
            }

            // Writing the "BY"
            for (int i = 0; i < byArray.Length; i++)
            {
                Console.SetCursorPosition(BYXAXETITLE, byYAxeTitle);
                Console.WriteLine(byArray[i]);
                byYAxeTitle++;
            }


            //Writing developpers infos
            Console.SetCursorPosition(positionXDeveloppers, positionYDeveloppers);

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

            Console.SetCursorPosition(22, 30);
            Console.Write("Appuyez sur ESC pour revenir au menu principal...");

            BackToMainMenu();
        }

        /// <summary>
        /// Win Menu
        /// </summary>
        public void Win()
        {
            //GC.Collect();
            int posX = Console.WindowWidth/8;
            int posY = 5;
            Console.SetCursorPosition(posX, posY);
            string victory = @"  ██    ██ ██  ██████ ████████  ██████  ██████  ██    ██\
██    ██ ██ ██         ██    ██    ██ ██   ██  ██  ██\
██    ██ ██ ██         ██    ██    ██ ██████    ████\ 
██  ██  ██ ██         ██    ██    ██ ██   ██    ██\  
████   ██  ██████    ██     ██████  ██   ██    ██";
            foreach (char c in victory)
            {
                Thread.Sleep(1);
                if (c == '█')
                {
                    Console.SetCursorPosition(posX, posY);
                    Enemy victoryBlock = new Enemy(posX, posY, '█');
                    Console.Write('█');
                }
                if (c == '\\')
                {
                    posY++;
                    posX = Console.WindowWidth/8;
                }
                else
                {
                    posX++;
                }

            }

            //ToDo : Victory Animation..
            //Console.SetCursorPosition(39, 45);
            //Player shipAnimation = new Player(Console.WindowWidth / 8, 45, 3);
            //foreach (char c in victory)
            //{
            //    Thread.Sleep(7);
            //    if (c == '█')
            //    {
            //        Shoot shootAnimation = new Shoot(shipAnimation.PosX, shipAnimation.PosY - 1, -1);
            //    }
            //    shipAnimation.Move(+1);
            //    if (c == '\\')
            //    {
            //        while (shipAnimation.PosX != Console.WindowWidth / 8)
            //        {
            //            Thread.Sleep(7);
            //            shipAnimation.Move(-1);
            //        }
            //    }
            //}
            //Console.ReadKey();

        }

        /// <summary>
        /// GameOver Menu
        /// </summary>
        public void GameOver()
        {
            // Variables
            const int GAMEOVERXTITLE = 4;
            int gameOverYTitle = 4;

            const int NEXTTIMEXPOSITION = 27;
            int nextTimeYPosition = 18;

            const int BACKTOMAINMENUXPOSITION = 15;
            int backToMainMenuYPosition = 30;

            string[] optionsArray = new string[5]
            {
                 " ██████   █████  ███    ███ ███████     ██████  ██    ██ ███████ ██████ ",  
                 "██       ██   ██ ████  ████ ██         ██    ██ ██    ██ ██      ██   ██", 
                 "██   ███ ███████ ██ ████ ██ █████      ██    ██ ██    ██ █████   ██████ ",  
                 "██    ██ ██   ██ ██  ██  ██ ██         ██    ██  ██  ██  ██      ██   ██",
                 " ██████  ██   ██ ██      ██ ███████     ██████    ████   ███████ ██   ██"
            };

            string nextTime = "We'll get them next time...";
            string backToMainMenu = "Appuyez sur ESCAPE pour revenir au menu principal...";

            Console.Clear();

            //Write the GameOver Text
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < optionsArray.Length; i++)
            {
                Thread.Sleep(500);
                Console.SetCursorPosition(GAMEOVERXTITLE, gameOverYTitle);
                Console.WriteLine(optionsArray[i]);
                gameOverYTitle++;
            }
            Console.ResetColor();

            //Write The next time text
            Console.SetCursorPosition(NEXTTIMEXPOSITION, nextTimeYPosition);
            for (int i = 0; i < nextTime.Length; i++)
            {
                Thread.Sleep(50);
                Console.Write(nextTime[i]);
            }

            //Write the return to main menu text
            Console.SetCursorPosition(BACKTOMAINMENUXPOSITION, backToMainMenuYPosition);
            for (int i = 0; i < backToMainMenu.Length; i++)
            {
                Thread.Sleep(20);
                Console.Write(backToMainMenu[i]);
            }

            BackToMainMenu();
        }

        /// <summary>
        /// Back to main menu
        /// </summary>
        private void BackToMainMenu()
        {
            while (!_continueKey)
            {
                _keyPressed = Console.ReadKey(true);
                switch (_keyPressed.Key)
                {
                    case ConsoleKey.Escape:
                        Program.PlaySound(_selectSound);
                        MainMenu();
                        break;
                }
            }
        }

        /// <summary>
        /// Set menu window size
        /// </summary>
        private void MenuWindowSize()
        {
            Console.SetWindowSize(_WINDOWSIZEX, _WINDOWSIZEY);
            Console.SetBufferSize(_WINDOWSIZEX, _WINDOWSIZEY);
        }
    }
}
