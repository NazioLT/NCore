using UnityEngine;
using Nazio_LT.Tools.Core.Internal;

namespace Nazio_LT.Tools.Core
{
    [AddComponentMenu("Nazio_LT/Core/NAudioManager")]
    public class NAudioManager : Singleton<NAudioManager>
    {
        [SerializeField, Range(1, 16)] private int audioPoolLength = 1;
        private AudioManagerChild[] audioPool;

        public void PlayAudio(NAudio _clip)
        {
            if (_clip == null) return;

            if (!IsAudioAvailableInPool(out AudioManagerChild _audioSource))
            {
                Debug.LogWarningFormat("No audio pool available");
                return;
            }

            _audioSource.SetAudio(_clip);
        }

        private void Start()
        {
            audioPool = new AudioManagerChild[audioPoolLength];
            for (var i = 0; i < audioPoolLength; i++)
            {
                audioPool[i] = CreateChild(i);
            }
        }

        private bool IsAudioAvailableInPool(out AudioManagerChild _audio)
        {
            _audio = null;
            foreach (var _audioItem in audioPool)
            {
                if (_audioItem.Available)
                {
                    _audio = _audioItem;
                    return true;
                }
            }

            return false;
        }

        private AudioManagerChild CreateChild(int _id)
        {
            GameObject _obj = new GameObject($"NAudio - {_id}");
            _obj.transform.parent = transform;

            return _obj.AddComponent<AudioManagerChild>();
        }
    }
}