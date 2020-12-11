/*
 * ETML
 * Auteur: Clément Sartoni
 * Date: 11.12.2020
 * Description: Class Sound qui gère tous les sons et musiques du jeu
 */
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace P_032_SpicyInvaders
{
    /// <summary>
    /// Class Sound
    /// </summary>
    public static class Sound
    {
        //Class variables
        /// <summary>
        /// The ResourceManager for the sounds
        /// </summary>
        private static readonly ResourceManager _resMan = new ResourceManager(typeof(AppResources.SoundFiles));
        
        /// <summary>
        /// The name of all the sounds in the rRessouceManager
        /// </summary>
        public enum Sounds
        {
            Barrier, 
            Barrier2,
            Blip_Select,
            Hit_Hurt,
            Laser_Shoot,
            Laser_Shoot2,
            Song
        }

        /// <summary>
        /// The SoundPlayer for the main music
        /// </summary>
        private static SoundPlayer _music = new SoundPlayer();

        /// <summary>
        /// The DirectSoundOut for the other sounds
        /// </summary>
        private static DirectSoundOut _soundPlayer = new DirectSoundOut();

        private static bool _soundOn = true;

        //Properties
        /// <summary>
        /// Property for the SoundOn option
        /// </summary>
        public static bool SoundOn
        {
            get { return _soundOn; }
            set 
            {
                _soundOn = value; 
                if(!_soundOn)
                {
                    _music.Stop();
                }
            }
        }

        /// <summary>
        /// Property for the music player
        /// </summary>
        public static SoundPlayer Music
        {
            get { return _music; }
        }

        //Methods
        /// <summary>
        /// Play a sound effect
        /// </summary>
        /// <param name="path">Sound to play</param>
        public static void PlaySound(Sounds sound)
        {
            if (_soundOn)
            {
                if (sound == Sounds.Song)
                {
                    _music.Stream = _resMan.GetStream(sound.ToString());
                    _music.PlayLooping();
                }
                else
                {
                    _soundPlayer.Init(new WaveChannel32(new WaveFileReader(_resMan.GetStream(sound.ToString()))));
                    _soundPlayer.Play();
                }
            }
        }
    }
}
