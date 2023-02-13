using UnityEngine;
using UnityEngine.Audio;

namespace Nazio_LT.Tools.Core
{
    [CreateAssetMenu(fileName = "NAudio", menuName = "Nazio_LT/Tools/NAudio")]
    public class NAudio : ScriptableObject
    {
        //Serializable vars
        [SerializeField] private AudioClip[] clip;

        [Header("Basic Audio Settings")]
        [SerializeField] private AudioMixerGroup mixerGroup;
        [SerializeField, Range(0f, 1f)] private float volume = 1f;
        [SerializeField, Range(-3f, 3f)] private float pitch = 1f;
        [SerializeField, Range(-1f, 1f)] private float stereoPan = 0f;
        [SerializeField, Range(0f, 1.1f)] private float reverbZoneMix = 1f;
        [SerializeField, Range(0f, 5f)] private float dopplerLevel = 1f;

        [Header("Advanced Audio Settings")]
        [SerializeField] private float maxPitchDelta = 0;

        //Outputs
        public AudioClip Clip => clip[Random.Range(0, clip.Length)];
        public string Name => Clip.name;
        public float Length => Clip.length;
        

        public AudioMixerGroup MixerGroup => mixerGroup;
        public float Volume => volume;
        public float Pitch => pitch;
        public float StereoPan => stereoPan;
        public float ReverbZoneMix => reverbZoneMix;
        public float DopplerLevel => dopplerLevel;

        public float MaxPitchDelta => maxPitchDelta;
    }
}