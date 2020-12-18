/*
 * Auteur:   Clément Sartoni
 * Date:     04.09.2020
 * Description: Class block. Protect player from bullets. Can be destroyed 
 * Modif. date: 18.12.20
 */
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
        private readonly int _sizeX;
        private readonly int _sizeY;
        private Random _random = new Random();
        private LittleBlock[,] _elements;

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
            _elements = new LittleBlock[_sizeX, _sizeY];

            for (int y = 0; y < _sizeY; y++)
            {
                for (int x = 0; x < _sizeX; x++)
                {
                    _elements[x, y] = new LittleBlock(_posX + x, _posY + y);
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
            foreach (LittleBlock block in _elements)
            {
                if (posX == block.PosX && posY == block.PosY && block.IsAlive)
                {
                    if (_random.Next(2) == 1)
                    {
                        Sound.PlaySound(Sound.Sounds.Barrier);
                    }
                    else
                    {
                        Sound.PlaySound(Sound.Sounds.Barrier2);
                    }

                    block.Delete();
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// Class LittleBlock
    /// </summary>
    public class LittleBlock : Entity
    {
        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="location">Little block location</param>
        public LittleBlock(int posX, int posY)
        {
            Sprite = '█';
            _posX = posX;
            _posY = posY;

            Console.SetCursorPosition(_posX, _posY);
            Console.Write(Sprite);
        }

        /// <summary>
        /// Destroy little block
        /// </summary>
        public void Delete()
        {
            IsAlive = false;
            Console.SetCursorPosition(_posX, _posY);
            Console.Write(' ');
        }
    }
}
