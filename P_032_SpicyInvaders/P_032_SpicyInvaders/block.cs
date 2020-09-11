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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_032_SpicyInvaders
{
    /// <summary>
    /// class Block, représente les blocs derrière lesquels le joueur peut s'abriter
    /// </summary>
    class Block
    {
        /// <summary>
        /// attributs
        /// </summary>
        private int[] _size;                       //la taille du block (x,y)

        private int[] _location;                   //la position du bloc (x,y)

        private LittleBlock[,] elements;

        /// <summary>
        /// properties
        /// </summary>
        public int[] Size
        {
            get { return _size; }
        }

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="size">La taille du bloc</param>
        /// <param name="location"></param>
        public Block(int sizeX, int sizeY, int locationX, int locationY)
        {
            _size = new int[] {sizeX,sizeY};
            _location = new int[] {locationX,locationY};

            Initialize();
        }

        /// <summary>
        /// deuxième constructeur renseigné
        /// </summary>
        /// <param name="sizeX"></param>
        /// <param name="sizeY"></param>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        public Block(int[] size, int[] location)
        {
            _size = size;
            _location = location;

            Initialize();
        }

        /// <summary>
        /// Initialiseur du bloc, le réinitialise si il est déja créé
        /// </summary>
        public void Initialize()
        {
            elements = new LittleBlock[_size[0], _size[1]];

            for(int y = 0; y < _size[1]; y++)
            {
                for(int x = 0; x < _size[0];x++)
                {
                    elements[x, y] = new LittleBlock(new int[] { _location[0] + x, _location[1] + y });
                }
            }
        }

        /// <summary>
        /// méthode qui supprime le bloc à la position spécifiée si il n'est pas déja mort
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool IsInside(int[] location)
        {
            foreach(LittleBlock block in elements)
            {
                //la méthode Array.Equals ne fonctionnait pas, donc retour à la bonne vielle méthode
                if(location.SequenceEqual(block.Location))
                {
                    block.Delete();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// sous classe représentant chaque élément du gros bloc
        /// </summary>
        class LittleBlock
        {
            /// <summary>
            /// attributs
            /// </summary>
            private char _charDesign = '█';            //le caractère utilisé pour dessiner le bloc

            private int[] _location;                   //la position en x,y

            private bool _isAlive = true;              //si le block est vivant

            /// <summary>
            /// properties
            /// </summary>
            public int[] Location
            {
                get { return _location; }
            }

            public bool IsAlive
            {
                get { return _isAlive; }
            }

            /// <summary>
            /// Constructeur renseigné
            /// </summary>
            /// <param name="location">la position à laquelle créer le mini-bloc</param>
            public LittleBlock(int[] location)
            {
                _location = location;
                Console.SetCursorPosition(_location[0], _location[1]);
                Console.Write(_charDesign);
            }
            
            /// <summary>
            /// méthode pour supprimer le bloc
            /// </summary>
            public void Delete()
            {
                _isAlive = false;
                Console.SetCursorPosition(_location[0], _location[1]);
                Console.Write(' ');
            }

        }
    }
    
}
