/*
	ETML
	Date: 06.11.2020
	Auteur: CSI
	Description: Classe de base pour les entités présentes dans le jeu
	Modifié le: --
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		/// Property for the PosX
		/// </summary>
		public int PosX
		{
			get { return _posX; }
			set { _posX = value; }
		}

		/// <summary>
		/// Property for the PosY
		/// </summary>
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
