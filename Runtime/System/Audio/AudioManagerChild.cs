using System.Collections;
using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManagerChild : MonoBehaviour
    {
        private AudioSource source;

        private void Awake()
        {
            source = GetComponent<AudioSource>();
        }

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

        public void SetAudioLoop(NAudio _clip, float _timeToStop = float.MaxValue)
        {
            source.loop = true;

            if(_timeToStop != float.MaxValue) StartCoroutine(StopAfterTime(_timeToStop));

            SetAudio(_clip);
        }

        private IEnumerator StopAfterTime(float _time)
        {
            yield return new WaitForSeconds(_time);
            source.Stop();
        }

        public bool Available => !source.isPlaying;
    }
}