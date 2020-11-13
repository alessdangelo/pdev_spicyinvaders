/*
 * Auteur:   Clément Sartoni
 * Date:     04.09.2020
 * Description: Classe "block" du projet Spicy Invaders, représente les bunkers derrière lesquels le vaisseau peut se cacher
 * 
 * Modifications:
 * Auteur:      CSI
 * Date:        11.09.2020
 * Description: remplacement de la méthode de test de la localisation pour quelque chose de plus propre.
 */
using System;
using NAudio.Wave;
using System.Linq;

namespace P_032_SpicyInvaders
{
    /// <summary>
    /// class Block, représente les blocs derrière lesquels le joueur peut s'abriter
    /// </summary>
    public class Block : Entity
    {
        /// <summary>
        /// attributs
        /// </summary>
        private int _sizeX;
        private int _sizeY;
        private Random _random = new Random();
        string barrierEffectPath;
        //public static readonly string barrierEffectPath = Environment.CurrentDirectory + $@"\{_wichSoundBarrier}.wav"; //A optimiser

        private LittleBlock[,] elements;

        /// <summary>
        /// properties
        /// </summary>
        public int SizeX
        {
            get { return _sizeX; }
        }

        public int SizeY
        {
            get { return _sizeY; }
        }

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="size">La taille du bloc</param>
        /// <param name="location"></param>
        public Block(int sizeX, int sizeY, int posX, int posY)
        {
            _sizeX = sizeX;
            _sizeY = sizeY;
            _posX = posX;
            _posY = posY;

            Initialize();
        }

        /// <summary>
        /// Initialiseur du bloc, le réinitialise si il est déja créé
        /// </summary>
        public void Initialize()
        {
            elements = new LittleBlock[_sizeX, _sizeY];

            for(int y = 0; y < _sizeY; y++)
            {
                for(int x = 0; x < _sizeX;x++)
                {
                    elements[x, y] = new LittleBlock( _posX + x, _posY + y );
                }
            }
        }

        /// <summary>
        /// méthode qui supprime le bloc à la position spécifiée si il n'est pas déja mort
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool IsInside(int posX, int posY)
        {
            foreach(LittleBlock block in elements)
            {
                if(posX == block.PosX && posY == block.PosY && block.IsAlive)
                {
                    if (_random.Next(2) == 1)
                    {
                        barrierEffectPath = Environment.CurrentDirectory + @"\barrier.wav";
                    }
                    else
                    {
                        barrierEffectPath = Environment.CurrentDirectory + @"\barrier2.wav";
                    }
                    DirectSoundOut shootingEffect = new DirectSoundOut();   // Create the sound object wich output sound
                    WaveFileReader shoot = new WaveFileReader(barrierEffectPath);   //Path of the file
                    shootingEffect.Init(new WaveChannel32(shoot));  //init the sound in a channel to be played
                    shootingEffect.Play();  //Play the sound
                    block.Delete();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// sous classe représentant chaque élément du gros bloc
        /// </summary>
        class LittleBlock : Entity
        {
            /// <summary>
            /// attributs
            /// </summary>
            private char _charDesign = '█';            //le caractère utilisé pour dessiner le bloc

            private bool _isAlive = true;              //si le block est vivant

            /// <summary>
            /// properties
            /// </summary>
            public bool IsAlive
            {
                get { return _isAlive; }
            }

            /// <summary>
            /// Constructeur renseigné
            /// </summary>
            /// <param name="location">la position à laquelle créer le mini-bloc</param>
            public LittleBlock(int posX, int posY)
            {
                _posX = posX;
                _posY = posY;

                Console.SetCursorPosition(_posX, _posY);
                Console.Write(_charDesign);
            }
            
            /// <summary>
            /// méthode pour supprimer le bloc
            /// </summary>
            public void Delete()
            {
                _isAlive = false;
                Console.SetCursorPosition(_posX, _posY);
                Console.Write(' ');
            }

        }
    }
    
}
