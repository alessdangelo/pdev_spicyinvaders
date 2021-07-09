/*
	ETML
	Date: 06.11.2020
	Auteur: CSI
	Description: Basic class for entity (parent of Block.cs, Enemy.cs, Player.cs and Shoot.cs).
	Modifié le: 20.11.20
*/
namespace P_032_SpicyInvaders
{
    /// <summary>
    /// Class Entity, base for Heritage
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Class Variables
        /// </summary>
        protected int _posX;
        protected int _posY;
        private char _sprite;
        private bool _isAlive = true;
        private bool _canShoot = true;

        /// <summary>
        /// Position X of the entity. Property of _posX
        /// </summary>
        public int PosX
        {
            get { return _posX; }
            set { _posX = value; }
        }

        /// <summary>
        /// Position Y of the entity. Property of _posY
        /// </summary>
        public int PosY
        {
            get { return _posY; }
            set { _posY = value; }
        }

        /// <summary>
        /// Sprite of the entity. Property of _sprite
        /// </summary>
        public char Sprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }

        /// <summary>
        /// Alive state of the entity. Property of _isAlive
        /// </summary>
        public bool IsAlive
        {
            get { return _isAlive; }
            set { _isAlive = value; }
        }

        /// <summary>
        /// Shoot state of the entity. Property of _canShoot
        /// </summary>
        public bool CanShoot
        {
            get { return _canShoot; }
            set { _canShoot = value; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Entity() { }
    }
}
