using UnityEngine;
using UnityEngine.Audio;

namespace Nazio_LT.Tools.Core
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "NAudio", menuName = "Nazio_LT/Tools/NAudio")]
    public class NAudio : ScriptableObject
    {
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