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
            source = GetComponent<AudioSource>();

            source.outputAudioMixerGroup = _clip.MixerGroup;
            source.volume = _clip.Volume;
            source.pitch = _clip.Pitch + Random.Range(-_clip.MaxPitchDelta, _clip.MaxPitchDelta);
            source.panStereo = _clip.StereoPan;
            source.reverbZoneMix = _clip.ReverbZoneMix;
            source.dopplerLevel = _clip.DopplerLevel;

            source.PlayOneShot(_clip.Clip);
            StartCoroutine(WaitForDestroy(_clip.Length));
        }

        private IEnumerator WaitForDestroy(float _s)
        {
            yield return new WaitForSeconds(_s);
            Destroy(gameObject);
        }
    }
}