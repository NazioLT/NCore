using System.Collections;
using UnityEngine;

namespace Nazio_LT.Tools.Core.Internal
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManagerChild : MonoBehaviour
    {
        private AudioSource source;

        public void SetAudio(AudioClip _clip)
        {
            source = GetComponent<AudioSource>();

            source.PlayOneShot(_clip);

            StartCoroutine(WaitForDestroy(_clip.length));
        }

        private IEnumerator WaitForDestroy(float _s)
        {
            yield return new WaitForSeconds(_s);
            Destroy(gameObject);
        }
    }
}