///   ETML 
/// 
///   Auteur     : Bruno Martins Constantino, Manus
///   Date       : 28.08.2020
///   Modif      : 04.12.2020
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
        private static ConsoleKeyInfo _keyPressed;
        private static bool _continueKey = false;
        const int _WINDOWSIZEX = 90;
        const int _WINDOWSIZEY = 35;
        private static readonly string _selectSound = "Blip_Select";
        private readonly string _path = Environment.CurrentDirectory + "/highscore.txt";

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
        /// Display Pause Menu
        /// </summary>
        public void PauseMenu()
        {
            //variables
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
                                    Program._gamePaused = false;
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
        /// Prints the main menu
        /// </summary>
        public void MainMenu()
        {
            // Variables
            const int SPICYXAXETITLE = 28;
            const int INVADERSXAXETITLE = 15;

            const string PLAY = "JOUER";
            const string OPTIONS = "OPTIONS";
            const string HIGHSCORE = "HIGHSCORE";
            const string INFOS = "INFOS";
            const string LEAVE = "QUITTER";

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

            //Console.SetCursorPosition(10, 16);
            //Console.Write("Appuyez sur '1', '2', '3', '4' ou '5' selon ce que vous voulez accéder.");
            //Console.SetCursorPosition(positionXSubMenu, positionYSubMenu);
            //Writing the sub menu
            for (int i = 0; i != 1; i++)
            {
                Console.SetCursorPosition(positionXSubMenu, positionYSubMenu);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(PLAY);
                Console.ResetColor();
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
            MenuSelection1();
        }

        /// <summary>
        /// Spacy Invaders game
        /// </summary>
        private void PlayGame()
        {
            Console.Clear();
            Program.RunGame();
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
            if (Sound.SoundOn)
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

            //Writing the "HIGHSCORE" title
            for (int i = 0; i < highscoreArray.Length; i++)
            {
                Console.SetCursorPosition(HIGHSCOREXAXETITLE, highscoreYAxeTitle);
                Console.WriteLine(highscoreArray[i]);
                highscoreYAxeTitle++;
            }

            //ToDo : Print the previous score, like in arcade game, with a name
            // read the highscore in txt file, if file doesn't exist, create it
            string result = "0";
            if(!File.Exists(_path))
            {
                File.Create(_path).Close();
            }
            if(File.ReadAllText(_path) != String.Empty)
            {
                result = File.ReadAllText(_path);
            }
            highscore = $"Votre meilleur score est de: " + result;

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
            {
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
        }
        /// <summary>
        /// GameOver Menu
        /// </summary>
        public void GameOver()
        {
            // Variables
            const int GAMEOVERXTITLE = 4;
            const int NEXTTIMEXPOSITION = 27;
            const int BACKTOMAINMENUXPOSITION = 15;

            int gameOverYTitle = 4;
            int nextTimeYPosition = 18;
            int backToMainMenuYPosition = 30;
            string nextTime = "We'll get them next time...";
            string backToMainMenu = "Appuyez sur ESCAPE pour revenir au menu principal...";
            string[] optionsArray = new string[5]
            {
                 " ██████   █████  ███    ███ ███████     ██████  ██    ██ ███████ ██████ ",  
                 "██       ██   ██ ████  ████ ██         ██    ██ ██    ██ ██      ██   ██", 
                 "██   ███ ███████ ██ ████ ██ █████      ██    ██ ██    ██ █████   ██████ ",  
                 "██    ██ ██   ██ ██  ██  ██ ██         ██    ██  ██  ██  ██      ██   ██",
                 " ██████  ██   ██ ██      ██ ███████     ██████    ████   ███████ ██   ██"
            };
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
                Thread.Sleep(15);
                Console.Write(backToMainMenu[i]);
            }
            BackToMainMenu();
        }

        private void MenuSelection1()
        {
            const string PLAY = "JOUER";
            const string OPTIONS = "OPTIONS";
            const string HIGHSCORE = "HIGHSCORE";
            const string INFOS = "INFOS";
            const string LEAVE = "QUITTER";

            int positionXSubMenu = 40;
            int positionYSubMenu = 20;
            int posXMenu = 40;
            int posYMenu = 20;

            int index = 0;
            while (!_continueKey)
            {
                _keyPressed = Console.ReadKey(true);
                Console.SetCursorPosition(posXMenu, posYMenu);
                //Sub menu movement
                switch (_keyPressed.Key)
                {
                    case ConsoleKey.UpArrow:
                        Sound.PlaySound(Sound.Sounds.Blip_Select);

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
                        positionXSubMenu = 40;
                        positionYSubMenu = 20;
                        if (index > 0)
                        {
                            index--;
                            Console.SetCursorPosition(posXMenu, posYMenu -= 2);
                        }
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        switch (index)
                        {
                            case 0:
                                Console.SetCursorPosition(posXMenu, posYMenu);
                                Console.Write("JOUER");
                                break;
                            case 1:
                                Console.SetCursorPosition(posXMenu -1 , posYMenu);
                                Console.Write("OPTIONS");
                                break;
                            case 2:
                                Console.SetCursorPosition(posXMenu -2 , posYMenu);
                                Console.Write("HIGHSCORE");
                                break;
                            case 3:
                                Console.SetCursorPosition(posXMenu , posYMenu);
                                Console.Write("INFOS");
                                break;
                            case 4:
                                Console.Write("QUITTER");
                                break;
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case ConsoleKey.DownArrow:
                        Sound.PlaySound(Sound.Sounds.Blip_Select);

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

                        positionXSubMenu = 40;
                        positionYSubMenu = 20;
                        if (index < 4)
                        {
                            index++;
                            Console.SetCursorPosition(posXMenu, posYMenu += 2);
                        }
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        switch (index)
                        {
                            case 0:
                                Console.SetCursorPosition(posXMenu, posYMenu);
                                Console.Write("JOUER");
                                break;
                            case 1:
                                Console.SetCursorPosition(posXMenu -1 , posYMenu);
                                Console.Write("OPTIONS");
                                break;
                            case 2:
                                Console.SetCursorPosition(posXMenu -2 , posYMenu);
                                Console.Write("HIGHSCORE");
                                break;
                            case 3:
                                Console.SetCursorPosition(posXMenu, posYMenu);
                                Console.Write("INFOS");
                                break;
                            case 4:
                                Console.SetCursorPosition(posXMenu - 1, posYMenu);
                                Console.Write("QUITTER");
                                break;
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case ConsoleKey.Enter:
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
                    default:
                        
                        break;
                }
                posXMenu = 40;
            }
        }

        /// <summary>
        /// Select where you want to go in the menu
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
                        Sound.PlaySound(Sound.Sounds.Blip_Select);
                        PlayGame();
                        break;

                    case ConsoleKey.D2:
                        Sound.PlaySound(Sound.Sounds.Blip_Select);
                        GameOptions();
                        break;

                    case ConsoleKey.D3:
                        Sound.PlaySound(Sound.Sounds.Blip_Select);
                        GameHighscore();
                        break;

                    case ConsoleKey.D4:
                        Sound.PlaySound(Sound.Sounds.Blip_Select);
                        Infos();
                        break;

                    case ConsoleKey.D5:
                        Sound.PlaySound(Sound.Sounds.Blip_Select);
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