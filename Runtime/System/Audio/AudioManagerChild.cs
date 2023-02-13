using System.Collections;
using UnityEngine;

namespace Nazio_LT.Tools.Core.Internal
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManagerChild : MonoBehaviour
    {
        private AudioSource source;

        public void SetAudio(NAudio _clip)
        {
            source.outputAudioMixerGroup = _clip.MixerGroup;
            source.volume = _clip.Volume;
            source.pitch = _clip.Pitch + Random.Range(-_clip.MaxPitchDelta, _clip.MaxPitchDelta);
            source.panStereo = _clip.StereoPan;
            source.reverbZoneMix = _clip.ReverbZoneMix;
            source.dopplerLevel = _clip.DopplerLevel;

            source.clip = _clip.Clip;
            source.Play();
        }

        private void Awake()
        {
            source = GetComponent<AudioSource>();
        }

        public bool Available => !source.isPlaying;
    }
}