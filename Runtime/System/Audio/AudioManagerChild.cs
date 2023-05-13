using System.Collections;
using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManagerChild : MonoBehaviour
    {
        private AudioSource m_source;

        public void SetAudio(NAudio clip)
        {
            m_source.outputAudioMixerGroup = clip.MixerGroup;
            m_source.volume = clip.Volume;
            m_source.pitch = clip.Pitch + Random.Range(-clip.MaxPitchDelta, clip.MaxPitchDelta);
            m_source.panStereo = clip.StereoPan;
            m_source.reverbZoneMix = clip.ReverbZoneMix;
            m_source.dopplerLevel = clip.DopplerLevel;

            m_source.clip = clip.Clip;
            m_source.Play();
        }

        public void SetAudioLoop(NAudio clip, float timeToStop = float.MaxValue)
        {
            m_source.loop = true;

            if(timeToStop != float.MaxValue) StartCoroutine(StopAfterTime(timeToStop));

            SetAudio(clip);
        }

        private void Awake()
        {
            m_source = GetComponent<AudioSource>();
        }

        private IEnumerator StopAfterTime(float time)
        {
            yield return new WaitForSeconds(time);
            m_source.Stop();
        }

        public bool Available => !m_source.isPlaying;
    }
}