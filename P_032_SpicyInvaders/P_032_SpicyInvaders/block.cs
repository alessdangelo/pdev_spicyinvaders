/*
 * Auteur:   Clément Sartoni
 * Date:     04.09.2020
 * Description: Class block. Protect player from bullets. Can be destroyed
 * 
 * Modifications:
 * Auteur:      CSI
 * Date:        11.09.2020
 * Description: remplacement de la méthode de test de la localisation pour quelque chose de plus propre.
 */
using NAudio.Wave;
using System;

namespace P_032_SpicyInvaders
{
    /// <summary>
    /// Class Block
    /// </summary>
    public class Block : Entity
    {
        /// <summary>
        /// Attributes
        /// </summary>
        private int _sizeX;
        private int _sizeY;

        private Random _random = new Random();
        private string[] barrierSoundPaths = new string[2] { $"{Environment.CurrentDirectory}\barrier.wav", "{Environment.CurrentDirectory}\barrier2.wav"};
        private DirectSoundOut soundPlayer = new DirectSoundOut();
        private WaveFileReader blockSoundToPlay;


        private LittleBlock[,] elements;

        /// <summary>
        /// Properties
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
        /// Custom constructor
        /// </summary>
        /// <param name="size">Block size</param>
        /// <param name="location">Block location</param>
        public Block(int sizeX, int sizeY, int posX, int posY)
        {
            _sizeX = sizeX;
            _sizeY = sizeY;
            _posX = posX;
            _posY = posY;

            Initialize();
        }

        /// <summary>
        /// Initialize the block (by creating little blocks)
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
        /// Destroy little block when hit by bullet
        /// </summary>
        /// <param name="location">Block location</param>
        /// <returns>Return true if block is destroyed</returns>
        public bool IsInside(int posX, int posY)
        {
            foreach(LittleBlock block in elements)
            {
                if(posX == block.PosX && posY == block.PosY && block.IsAlive)
                {
                    if (_random.Next(2) == 1)
                    {
                        blockSoundToPlay = new WaveFileReader(barrierSoundPaths[0]);
                    }
                    else
                    {
                        blockSoundToPlay = new WaveFileReader(barrierSoundPaths[1]);
                    }

                    soundPlayer.Init(new WaveChannel32(blockSoundToPlay));
                    soundPlayer.Play();
                    block.Delete();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Class LittleBlock
        /// </summary>
        class LittleBlock : Entity
        {
            /// <summary>
            /// Attributes
            /// </summary>
            private char _charDesign = '█';
            private bool _isAlive = true;
        
            /// <summary>
            /// Properties
            /// </summary>
            public bool IsAlive
            {
                get { return _isAlive; }
            }

            /// <summary>
            /// Custom constructor
            /// </summary>
            /// <param name="location">Little block location</param>
            public LittleBlock(int posX, int posY)
            {
                _posX = posX;
                _posY = posY;

                Console.SetCursorPosition(_posX, _posY);
                Console.Write(_charDesign);
            }
            
            /// <summary>
            /// Destroy little block
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
