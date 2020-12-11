/*
	ETML
	Date: 11.09.20
	Auteur: Manuel Oro
	Description: Enemy class. Enemy can move, shoot and be destroyed
	Modifié le: 04.12.20
*/
using System;
using System.Collections.Generic;

namespace P_032_SpicyInvaders
{
    /// <summary>
    /// Class Enemy
    /// </summary>
    public class Enemy : Entity
    {
        /// <summary>
        /// Attributes
        /// </summary>

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="posX">Spawn enemy at position x</param>
        /// <param name="posY">Spawn enemy at position y</param>
        public Enemy(int posX, int posY)
        {
            _posX = posX;
            _posY = posY;
            Sprite = '*';
        }

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="posX">Spawn enemy at position x</param>
        /// <param name="posY">Spawn enemy at position y</param>
        /// <param name="ennemyCharacter">Character displayed (represent the enemy)</param>
        public Enemy(int posX, int posY, char ennemyCharacter = '*')
        {
            _posX = posX;
            _posY = posY;
            Sprite = ennemyCharacter;
        }


        /// <summary>
        /// Move enemy
        /// </summary>
        /// <param name="direction">Select in which direction enemy goes</param>
        public void Move(int[] direction)
        {
            if (IsAlive)
            {
                Console.SetCursorPosition(_posX, _posY);
                Console.Write(" ");
                _posX += direction[0];
                _posY += direction[1];
                Console.SetCursorPosition(_posX, _posY);
                Console.Write(Sprite);
            }
            else
            {
                _posX += direction[0];
                _posY += direction[1];
            }
        }

        public static void MoveEnnemies(ref DateTime moveEnnemyAndControlShoot, ref int ennemiesSpeed, ref int[] direction, ref Enemy[,] ennemiesArray, Random random, ref List<Shoot> bullets, ref int[] ennemiesLimits)
        {
            if (DateTime.Now.Ticks > moveEnnemyAndControlShoot.Ticks)
            {
                moveEnnemyAndControlShoot = DateTime.Now.AddMilliseconds(ennemiesSpeed);
                if (direction[1] == 1)
                {
                    for (int y = ennemiesArray.GetLength(1) - 1; y >= 0; y--)
                    {
                        for (int x = 0; x < ennemiesArray.GetLength(0); x++)
                        {
                            if (ennemiesArray[x, y].IsAlive)
                            {
                                if (random.Next(50) == 1)
                                {
                                    bullets.Add(new Shoot(ennemiesArray[x, y].PosX, ennemiesArray[x, y].PosY + 5, +1));
                                }
                            }
                            ennemiesArray[x, y].Move(direction);
                        }
                    }
                }
                else
                {
                    foreach (Enemy ennemy in ennemiesArray)
                    {
                        if (ennemy.IsAlive)
                        {
                            if (random.Next(50) == 1)
                            {
                                bullets.Add(new Shoot(ennemy.PosX, ennemy.PosY + 5, +1));
                            }
                        }
                        ennemy.Move(direction);
                    }
                }

                if (ennemiesArray[0, 0].PosX + direction[0] <= ennemiesLimits[0])
                {
                    direction = new int[] { 0, 1 };
                }
                if (ennemiesArray[ennemiesArray.GetLength(0) - 1, 0].PosX + direction[0] >= ennemiesLimits[1])
                {
                    direction = new int[] { 0, -1 };
                }
                if (ennemiesArray[0, 0].PosY + direction[1] <= ennemiesLimits[2])
                {
                    direction = new int[] { -1, 0 };
                }
                if (ennemiesArray[0, ennemiesArray.GetLength(1) - 1].PosY + direction[1] >= ennemiesLimits[3])
                {
                    direction = new int[] { 1, 0 };
                }
            }
        }

        /// <summary>
        /// Remove enemy char
        /// </summary>
        public void DestroyEnemy()
        {
            Console.SetCursorPosition(_posX, _posY);
            Console.Write(" ");
            //Program._ship.Score += 100;
            //Program._hud.PrintPlayerScore();
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Enemy()
        {

        }
    }
}
