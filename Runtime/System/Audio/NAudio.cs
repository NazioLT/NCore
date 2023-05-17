using UnityEngine;
using UnityEngine.Audio;

namespace Nazio_LT.Tools.Core
{
    [CreateAssetMenu(fileName = "NAudio", menuName = "Nazio_LT/Tools/NAudio")]
    public class NAudio : ScriptableObject
    {
        //Serializable vars
        [SerializeField] private AudioClip[] m_clip;

        [Header("Basic Audio Settings")]
        [SerializeField] private AudioMixerGroup m_mixerGroup;
        [SerializeField, Range(0f, 1f)] private float m_volume = 1f;
        [SerializeField, Range(-3f, 3f)] private float m_pitch = 1f;
        [SerializeField, Range(-1f, 1f)] private float m_stereoPan = 0f;
        [SerializeField, Range(0f, 1.1f)] private float m_reverbZoneMix = 1f;
        [SerializeField, Range(0f, 5f)] private float m_dopplerLevel = 1f;

        [Header("Advanced Audio Settings")]
        [SerializeField] private float m_maxPitchDelta = 0;

        //Outputs
        public AudioClip Clip => m_clip[Random.Range(0, m_clip.Length)];

        public AudioMixerGroup MixerGroup => m_mixerGroup;
        public float Volume => m_volume;
        public float Pitch => m_pitch;
        public float StereoPan => m_stereoPan;
        public float ReverbZoneMix => m_reverbZoneMix;
        public float DopplerLevel => m_dopplerLevel;

        public float MaxPitchDelta => m_maxPitchDelta;
    }
}