﻿///   ETML 
/// 
///   Auteur     : Bruno Martins Constantino, Manus
///   Date       : 28.08.2020
///   Modif      : 18.12.2020
///   Descrption : This is the main menu of our Spicy Invaders
using System;
using System.IO;
using System.Threading;

namespace P_032_SpicyInvaders
{
    /// <summary>
    /// Class Menu
    /// </summary>
    public class Menu
    {
        // Variables
        private Game _game;
        private static ConsoleKeyInfo _keyPressed;
        private static bool _continueKey = false;
        const int _WINDOWSIZEX = 90;
        const int _WINDOWSIZEY = 35;
        private static readonly string _path = Environment.CurrentDirectory + "/highscore.txt";

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Menu()
        {
            Console.CursorVisible = false;
        }

        /// <summary>
        /// Set console window size
        /// </summary>
        private void SetConsoleWindowSize()
        {
            Console.SetWindowSize(_WINDOWSIZEX, _WINDOWSIZEY);
            Console.SetBufferSize(_WINDOWSIZEX, _WINDOWSIZEY);
        }

        /// <summary>
        /// Write/Read text from txt file
        /// </summary>
        /// <param name="path">text file path</param>
        /// <param name="score">Score to write</param>
        /// <returns>Return score from text file</returns>
        public static string WriteOrReadHighscore(string path, int score = 0)
        {
            if (File.Exists(path))
            {
                Int32.TryParse(File.ReadAllText(path), out int result);
                if (score > result)
                {
                    File.WriteAllText(path, score.ToString());
                    return score.ToString();
                }
                return result.ToString();
            }
            else
            {
                File.Create(path).Close();
                return "0";
            }
        }

        /// <summary>
        /// Display Pause Menu
        /// </summary>
        public void PauseMenu()
        {
            // Variables
            string[] options = new string[]
            {
                "   Retourner au jeu   ",
                "   Retourner au menu  ",
                "    Quitter le jeu    "
            };
            int posX = 28;
            int posY = 22;
            int Y;
            int index = 1;
            _continueKey = false;

            //Sound in the menu
            Sound.PlaySound(Sound.Sounds.Blip_Select);


            Console.SetCursorPosition(posX, posY++);
            Console.WriteLine("                      ");
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine("      GAME PAUSED     ");

            while (!_continueKey)
            {
                Y = posY + 1;

                for (int i = 0; i < options.Length; i++)
                {
                    Console.SetCursorPosition(posX, Y++);
                    Console.WriteLine("                      ");

                    if (i == index - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }

                    Console.SetCursorPosition(posX, Y++);
                    Console.WriteLine($"{options[i]}");

                    Console.ResetColor();
                }


                _keyPressed = Console.ReadKey(true);

                //Sub menu movement
                switch (_keyPressed.Key)
                {
                    case ConsoleKey.UpArrow:
                        index--;
                        if (index <= 0)
                        {
                            index = options.Length;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        index++;
                        if (index > options.Length)
                        {
                            index = 1;
                        }
                        break;
                    case ConsoleKey.Enter:
                        switch (index)
                        {
                            case 1:
                                {
                                    Sound.PlaySound(Sound.Sounds.Blip_Select);
                                    for (int i = 0; i < options.Length + 2; i++)
                                    {
                                        Console.SetCursorPosition(posX, posY + (2 * i));
                                        Console.WriteLine("                      ");
                                    }
                                    _continueKey = true;
                                    break;
                                }
                            case 2:
                                {
                                    Sound.PlaySound(Sound.Sounds.Blip_Select);
                                    Sound.Music.Stop();
                                    Game._gamePaused = false;
                                    MainMenu();
                                    break;
                                }
                            case 3:
                                {
                                    Environment.Exit(0);
                                    break;
                                }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Print main menu
        /// </summary>
        public void MainMenu()
        {
            // Variables
            const int SPICYXAXETITLE = 28;
            const int INVADERSXAXETITLE = 15;
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
            int spicyYAxeTitle = 1;
            int invadersYAxeTitle = 7;
            Console.Clear();

            _game = new Game(this);
            SetConsoleWindowSize();

            Console.SetCursorPosition(SPICYXAXETITLE, spicyYAxeTitle);

            Console.ForegroundColor = ConsoleColor.Red;

            //Writing the "SPICY" game title
            for (int i = 0; i < spicyArray.Length; i++)
            {
                Console.SetCursorPosition(SPICYXAXETITLE, spicyYAxeTitle);
                Console.WriteLine(spicyArray[i]);
                spicyYAxeTitle++;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;

            //Writing the "INVADERS" game title
            for (int i = 0; i < invadersArray.Length; i++)
            {
                Console.SetCursorPosition(INVADERSXAXETITLE, invadersYAxeTitle);
                Console.WriteLine(invadersArray[i]);
                invadersYAxeTitle++;
            }
            Console.ForegroundColor = ConsoleColor.White;
            MenuSelection();
        }

        /// <summary>
        /// Start a game
        /// </summary>
        private void PlayGame()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            _game.RunGame();
        }

        /// <summary>
        /// Print game options
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
                        Sound.PlaySound(Sound.Sounds.Blip_Select);
                        index = 0;
                        WriteOptions(index);
                        break;

                    case ConsoleKey.DownArrow:
                        Sound.PlaySound(Sound.Sounds.Blip_Select);
                        index = 1;
                        WriteOptions(index);
                        break;

                    case ConsoleKey.Enter:
                        Sound.PlaySound(Sound.Sounds.Blip_Select);
                        if (index == 0)
                        {
                            Sound.SoundOn = !Sound.SoundOn;
                            WriteOptions(index);
                        }
                        else if (index == 1)
                        {
                            if (_game.Difficulty == 0)
                            {
                                _game.Difficulty = 1;
                                WriteOptions(index);
                            }
                            else
                            {
                                _game.Difficulty = 0;
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
        /// Write sound and difficulty options
        /// </summary>
        /// <param name="index">Get selected options (sound or difficulty)</param>
        private void WriteOptions(int index)
        {
            string soundActiveText = "Son actif: ";
            string difficultyText = "Difficulté: ";
            Console.SetCursorPosition(Console.WindowWidth / 2 - soundActiveText.Length / 2 - 2, 15);
            if (index == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            if (Sound.SoundOn)
            {
                Console.Write($"{soundActiveText}Oui");
            }
            else
            {
                Console.Write($"{soundActiveText}Non");
            }

            Console.ForegroundColor = ConsoleColor.White;

            if (index == 1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            Console.SetCursorPosition(Console.WindowWidth / 2 - difficultyText.Length / 2 - 3, 18);

            if (_game.Difficulty == 0)
            {
                Console.WriteLine($"{difficultyText}Facile   ");
            }
            else
            {
                Console.WriteLine($"{difficultyText}Difficile");
            }
            Console.ForegroundColor = ConsoleColor.White;
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

            //Writing the "HIGHSCORE" title
            for (int i = 0; i < highscoreArray.Length; i++)
            {
                Console.SetCursorPosition(HIGHSCOREXAXETITLE, highscoreYAxeTitle);
                Console.WriteLine(highscoreArray[i]);
                highscoreYAxeTitle++;
            }

            highscore = $"Votre meilleur score est de: " + WriteOrReadHighscore(_path);

            Console.SetCursorPosition((Console.WindowWidth) / 2 - (highscore.Length / 2), 20);
            Console.Write(highscore);

            Console.SetCursorPosition(21, 30);
            Console.Write("Appuyez sur ESC pour revenir au menu principal...");

            BackToMainMenu();
        }

        /// <summary>
        /// Print infos
        /// </summary>
        private void Infos()
        {
            // Variables
            const int DEVELOPPEDXAXETITLE = 5;
            const int BYXAXETITLE = 38;
            const string DEVONE = "Bruno Martins Constantino";
            const string DEVTWO = "Manuel Oro";
            const string DEVTHREE = "Alessandro D'Angelo";
            const string DEVFOUR = "Clément Sartoni";
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
            Console.Clear();

            // Writing the "DEVELOPPED"
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

            // Writing developpers infos
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
        /// Print win
        /// </summary>
        /// <param name="score">Player score</param>
        public void Win(int score)
        {
            string[] victoryArray = new string[5]
            {
            "██    ██ ██  ██████ ████████  ██████  ██████  ██    ██",
            "██    ██ ██ ██         ██    ██    ██ ██   ██  ██  ██",
            "██    ██ ██ ██         ██    ██    ██ ██████    ████",
            " ██  ██  ██ ██         ██    ██    ██ ██   ██    ██",
            "  ████   ██  ██████    ██     ██████  ██   ██    ██"
            };

            string[] textToWrite = new string[]
            {
                $"Votre score est de {score}",
                "Bien joué ! Vous avez exterminé la force ennemie",
                "Appuyez sur ESCAPE pour revenir au menu principal..."
            };

            int posX = Console.WindowWidth / 2 - victoryArray[0].Length / 2;
            int posY = 5;
            int nextTimeYPosition = 28;
            int backToMainMenuYPosition = 35;
            int scoreYPosition = 20;

            // Write victory text
            for (int i = 0; i < victoryArray.Length; i++)
            {
                Thread.Sleep(450);
                Console.SetCursorPosition(posX, posY);
                Console.WriteLine(victoryArray[i]);
                posY++;
            }

            Console.SetCursorPosition(Console.WindowWidth / 2 - textToWrite[0].Length / 2, scoreYPosition);
            for (int i = 0; i < textToWrite[0].Length; i++)
            {
                Thread.Sleep(50);
                Console.Write(textToWrite[0][i]);
            }

            // Write The next time text
            Console.SetCursorPosition(Console.WindowWidth / 2 - textToWrite[1].Length / 2, nextTimeYPosition);
            for (int i = 0; i < textToWrite[1].Length; i++)
            {
                Thread.Sleep(50);
                Console.Write(textToWrite[1][i]);
            }

            // Write the return to main menu text
            Console.SetCursorPosition(Console.WindowWidth / 2 - textToWrite[2].Length / 2, backToMainMenuYPosition);
            for (int i = 0; i < textToWrite[2].Length; i++)
            {
                Thread.Sleep(15);
                Console.Write(textToWrite[2][i]);
            }
            BackToMainMenu();
        }

        /// <summary>
        /// Print GameOver
        /// </summary>
        /// <param name="score">Player score</param>
        public void GameOver(int score)
        {
            // Variables
            const int GAMEOVERXTITLE = 4;

            int gameOverYTitle = 4;
            int nextTimeYPosition = 28;
            int backToMainMenuYPosition = 35;
            int scoreYPosition = 20;

            string[] textToWrite = new string[]
            {
                $"Votre score est de {score}",
                "We'll get them next time...",
                "Appuyez sur ESCAPE pour revenir au menu principal..."
            };

            string[] optionsArray = new string[5]
            {
                 " ██████   █████  ███    ███ ███████     ██████  ██    ██ ███████ ██████ ",
                 "██       ██   ██ ████  ████ ██         ██    ██ ██    ██ ██      ██   ██",
                 "██   ███ ███████ ██ ████ ██ █████      ██    ██ ██    ██ █████   ██████ ",
                 "██    ██ ██   ██ ██  ██  ██ ██         ██    ██  ██  ██  ██      ██   ██",
                 " ██████  ██   ██ ██      ██ ███████     ██████    ████   ███████ ██   ██"
            };
            Console.Clear();

            // Write the GameOver Text
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < optionsArray.Length; i++)
            {
                Thread.Sleep(450);
                Console.SetCursorPosition(GAMEOVERXTITLE, gameOverYTitle);
                Console.WriteLine(optionsArray[i]);
                gameOverYTitle++;
            }
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(Console.WindowWidth / 2 - textToWrite[0].Length / 2, scoreYPosition);
            for (int i = 0; i < textToWrite[0].Length; i++)
            {
                Thread.Sleep(50);
                Console.Write(textToWrite[0][i]);
            }

            // Write The next time text
            Console.SetCursorPosition(Console.WindowWidth / 2 - textToWrite[1].Length / 2, nextTimeYPosition);
            for (int i = 0; i < textToWrite[1].Length; i++)
            {
                Thread.Sleep(50);
                Console.Write(textToWrite[1][i]);
            }

            // Write the return to main menu text
            Console.SetCursorPosition(Console.WindowWidth / 2 - textToWrite[2].Length / 2, backToMainMenuYPosition);
            for (int i = 0; i < textToWrite[2].Length; i++)
            {
                Thread.Sleep(15);
                Console.Write(textToWrite[2][i]);
            }
            BackToMainMenu();
        }

        /// <summary>
        /// Menu selection
        /// </summary>
        private void MenuSelection()
        {
            int posYMenu = 20;
            string howToMove = "Appuyez sur les flèches directionnels ↑ ↓ pour vous déplacer.";
            Console.SetCursorPosition(Console.WindowWidth / 2 - howToMove.Length / 2, 16);
            Console.WriteLine(howToMove);
            string[] menuOption = new string[]
            {
                "JOUER",
                "OPTIONS",
                "HIGHSCORE",
                "INFOS",
                "QUITTER"
            };

            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < menuOption.Length; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - menuOption[i].Length / 2, posYMenu);
                Console.WriteLine(menuOption[i]);
                posYMenu += 2;
                Console.ForegroundColor = ConsoleColor.White;
            }

            posYMenu = 20;
            int index = 0;
            while (!_continueKey)
            {
                _keyPressed = Console.ReadKey(true);

                // Sub menu movement
                switch (_keyPressed.Key)
                {
                    case ConsoleKey.UpArrow:
                        Sound.PlaySound(Sound.Sounds.Blip_Select);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(Console.WindowWidth / 2 - menuOption[index].Length / 2, posYMenu);
                        Console.Write(menuOption[index]);
                        if (index > 0)
                        {
                            index--;
                            Console.SetCursorPosition(Console.WindowWidth / 2 - menuOption[index].Length / 2, posYMenu -= 2);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        Sound.PlaySound(Sound.Sounds.Blip_Select);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(Console.WindowWidth / 2 - menuOption[index].Length / 2, posYMenu);
                        Console.Write(menuOption[index]);
                        if (index < 4)
                        {
                            index++;
                            Console.SetCursorPosition(Console.WindowWidth / 2 - menuOption[index].Length / 2, posYMenu += 2);
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.ForegroundColor = ConsoleColor.White;
                        if (index == 0)
                        {
                            PlayGame();
                        }
                        if (index == 1)
                        {
                            GameOptions();
                        }
                        if (index == 2)
                        {
                            GameHighscore();
                        }
                        if (index == 3)
                        {
                            Infos();
                        }
                        if (index == 4)
                        {
                            Environment.Exit(0);
                        }
                        else
                        {
                            Sound.PlaySound(Sound.Sounds.Blip_Select);
                            _continueKey = true;
                        }
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    default:

                        break;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                switch (index)
                {
                    case 0:
                        Console.SetCursorPosition(Console.WindowWidth / 2 - menuOption[0].Length / 2, posYMenu);
                        Console.Write(menuOption[0]);
                        break;
                    case 1:
                        Console.SetCursorPosition(Console.WindowWidth / 2 - menuOption[1].Length / 2, posYMenu);
                        Console.Write(menuOption[1]);
                        break;
                    case 2:
                        Console.SetCursorPosition(Console.WindowWidth / 2 - menuOption[2].Length / 2, posYMenu);
                        Console.Write(menuOption[2]);
                        break;
                    case 3:
                        Console.SetCursorPosition(Console.WindowWidth / 2 - menuOption[3].Length / 2, posYMenu);
                        Console.Write(menuOption[3]);
                        break;
                    case 4:
                        Console.SetCursorPosition(Console.WindowWidth / 2 - menuOption[4].Length / 2, posYMenu);
                        Console.Write(menuOption[4]);
                        break;
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
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
                        Sound.PlaySound(Sound.Sounds.Blip_Select);
                        MainMenu();
                        break;
                }
            }
        }
    }
}