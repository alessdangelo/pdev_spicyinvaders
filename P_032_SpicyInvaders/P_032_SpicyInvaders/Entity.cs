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

		/// <summary>
		/// Properties
		/// </summary>
		public int PosX
		{
			get { return _posX; }
			set { _posX = value; }
		}

		public int PosY
		{
			get { return _posY; }
			set { _posY = value; }
		}

		/// <summary>
		/// Default Constructor
		/// </summary>
		public Entity()
		{

		}
	}
}
