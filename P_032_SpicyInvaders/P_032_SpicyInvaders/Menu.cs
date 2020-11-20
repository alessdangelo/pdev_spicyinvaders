///   ETML 
/// 
///   Auteur     : Bruno Martins Constantino, Manus
///   Date       : 28.08.2020
///   Modif      : 06.11.2020
///   Descrption : Ceci est le menu principal de notre Spicy Invaders

using System;
using System.IO;
using System.Threading;
using NAudio.Wave;
using System.Threading;

namespace P_032_SpicyInvaders
{
    public class Menu
    {
        // Variables
        private static ConsoleKeyInfo _keyPressed;
        private static bool _continueKey = false;
        const int WINDOWSIZEX = 90;
        const int WINDOWSIZEY = 35;
        private static readonly string _selectSoundPath = Environment.CurrentDirectory + @"\Blip_Select.wav"; //"Select" sound effect location

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Menu()
        {
            Console.SetWindowSize(WINDOWSIZEX, WINDOWSIZEY);
            Console.SetBufferSize(WINDOWSIZEX, WINDOWSIZEY);

            Console.CursorVisible = false;
        }

        public void PauseMenu()
        {
            DirectSoundOut _selectSound = new DirectSoundOut();
            WaveFileReader _selectSoundLocation = new WaveFileReader(_selectSoundPath); //Path of the file (== _selectSoundPath)

            //Sound in the menu
            _selectSound.Init(new WaveChannel32(_selectSoundLocation)); //Put the song in _selectSoundLocation and put it in the channel

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
                        _selectSound.Play();
                        index = 0;
                        Console.SetCursorPosition(31, 25);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Retourner au jeu");

                        Console.SetCursorPosition(36, 27);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Quitter");
                        break;

                    case ConsoleKey.DownArrow:
                        _selectSound.Play();
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
                            _selectSound.Play();
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
            ///Variables
            //const int WINDOWSIZEX = 90;
            //const int WINDOWSIZEY = 35;
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

            ///Main program

            Console.Clear();

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
            //Variables
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

            //Writing the "OPTIONS"
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
                //Sub menu movement
                switch (_keyPressed.Key)
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
                        if (index == 0)
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
                            if (Program.difficulty == 0)
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
                }
            }
        }

        /// <summary>
        /// Select in where menu you want to go
        /// </summary>
        private void MenuSelection()
        {
            DirectSoundOut _selectSound = new DirectSoundOut();
            WaveFileReader _selectSoundLocation = new WaveFileReader(_selectSoundPath); //Path of the file (== _selectSoundPath)

            //Sound in the menu
            _selectSound.Init(new WaveChannel32(_selectSoundLocation)); //Put the song in _selectSoundLocation and put it in the channel
            while (!_continueKey)
                {
                    _keyPressed = Console.ReadKey(true);
                    //Sub menu movement
                    switch (_keyPressed.Key)
                    {
                        case ConsoleKey.D1:
                            _selectSound.Play();
                            PlayGame();
                            break;

                        case ConsoleKey.D2:
                            _selectSound.Play();
                            GameOptions();
                            break;

                        case ConsoleKey.D3:
                            _selectSound.Play();
                            GameHighscore();
                            break;

                        case ConsoleKey.D4:
                            _selectSound.Play();
                            Infos();
                            break;

                        case ConsoleKey.D5:
                            _selectSound.Play();
                            Environment.Exit(1);
                            break;

                        case ConsoleKey.Escape:
                            _selectSound.Play();
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

        private void GameOver()
        {
            // Variables
            const int GAMEOVERXTITLE = 17;
            int gameOverYTitle = 1;

            string[] optionsArray = new string[5]
            {
                 " ██████   █████  ███    ███ ███████      ██████  ██    ██ ███████ ██████ ",  
                 "██       ██   ██ ████  ████ ██          ██    ██ ██    ██ ██      ██   ██", 
                 "██   ███ ███████ ██ ████ ██ █████       ██    ██ ██    ██ █████   ██████ ",  
                 "██    ██ ██   ██ ██  ██  ██ ██          ██    ██  ██  ██  ██      ██   ██",
                 " ██████  ██   ██ ██      ██ ███████      ██████    ████   ███████ ██   ██"
            };

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < optionsArray.Length; i++)
            {
                Thread.Sleep(500);
                Console.SetCursorPosition(GAMEOVERXTITLE, gameOverYTitle);
                Console.WriteLine(optionsArray[i]);
                gameOverYTitle++;
            }
            Console.ResetColor();
        }

        private void BackToMainMenu()
        {
            DirectSoundOut _selectSound = new DirectSoundOut();
            WaveFileReader _selectSoundLocation = new WaveFileReader(_selectSoundPath); //Path of the file (== _selectSoundPath)

            _selectSound.Init(new WaveChannel32(_selectSoundLocation)); //Put the song in _selectSoundLocation and put it in the channel

            while (!_continueKey)
            {
                _keyPressed = Console.ReadKey(true);

                switch (_keyPressed.Key)
                {
                    case ConsoleKey.Escape:
                        _selectSound.Play();
                        MainMenu();
                        break;
                }
            }
        }
    }
}
