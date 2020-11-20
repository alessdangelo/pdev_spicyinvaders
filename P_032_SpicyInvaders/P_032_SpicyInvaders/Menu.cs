﻿///   ETML 
/// 
///   Auteur     : Bruno Martins Constantino, Manus
///   Date       : 28.08.2020
///   Modif      : 06.11.2020
///   Descrption : Ceci est le menu principal de notre Spicy Invaders

using System;
using System.IO;
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
            //Sound in the menu
             DirectSoundOut _selectSound = new DirectSoundOut();
             WaveFileReader _selectSoundLocation = new WaveFileReader(_selectSoundPath); //Path of the file (== _selectSoundPath)

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
            //Sound in the menu
            DirectSoundOut _selectSound = new DirectSoundOut();
            WaveFileReader _selectSoundLocation = new WaveFileReader(_selectSoundPath); //Path of the file (== _selectSoundPath)
            _selectSound.Init(new WaveChannel32(_selectSoundLocation)); //Put the song in _selectSoundLocation and put it in the channel
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
            //Music files, maybe add another one for this?
            DirectSoundOut _selectSound = new DirectSoundOut();
            WaveFileReader _selectSoundLocation = new WaveFileReader(_selectSoundPath); //Path of the file (== _selectSoundPath)
            _selectSound.Init(new WaveChannel32(_selectSoundLocation)); //Put the song in _selectSoundLocation and put it in the channel
           
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
                while (!_continueKey)
                {
                    _keyPressed = Console.ReadKey(true);
                    //Sub menu movement
                    switch (_keyPressed.Key)
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

        /// <summary>
        /// Game Over Menu
        /// </summary>
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
            //Sound in the menu
            DirectSoundOut _selectSound = new DirectSoundOut();
            WaveFileReader _selectSoundLocation = new WaveFileReader(_selectSoundPath); //Path of the file (== _selectSoundPath)
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
