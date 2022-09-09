using UnityEngine;
using UnityEngine.Audio;

namespace Nazio_LT.Tools.Core
{
    [System.Serializable]
    public class NAudio
    {
        #region Constructors

        public NAudio(AudioClip _clip)
        {
            clip = _clip;
            volume = 1f;
        }

        public NAudio(AudioClip _clip, AudioMixerGroup _mixerGroup)
        {
            clip = _clip;
            mixerGroup = _mixerGroup;
            volume = 1f;
        }

        public NAudio(AudioClip _clip, AudioMixerGroup _mixerGroup, float _volume)
        {
            clip = _clip;
            mixerGroup = _mixerGroup;
            volume = _volume;
        }

        #endregion

        //Serializable vars
        [SerializeField] private AudioClip clip;
        [SerializeField] private AudioMixerGroup mixerGroup;
        [SerializeField] private float volume = 1f;

        //Outputs
        public AudioClip Clip => clip;
        public AudioMixerGroup MixerGroup => mixerGroup;
        public float Length => Clip.length;
        public string Name => Clip.name;
        public float Volume => volume;
    }
}