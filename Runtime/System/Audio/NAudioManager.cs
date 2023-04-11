using UnityEngine;
using System.Collections.Generic;

namespace Nazio_LT.Tools.Core
{
    [AddComponentMenu("Nazio_LT/Core/NAudioManager")]
    public class NAudioManager : Singleton<NAudioManager>
    {
        [SerializeField, Range(1, 32)] private int audioPoolLength = 1;
        [SerializeField] private bool createAudioIfFullyAssigned = true;
        private List<AudioManagerChild> audioPool;

        public void PlayAudioOneShot(NAudio _clip)
        {
            if (_clip == null) return;

            if (!IsAudioAvailableInPool(out AudioManagerChild _audioSource))
            {
                Debug.LogWarningFormat("No audio pool available");
                return;
            }

            _audioSource.SetAudio(_clip);
        }

        public AudioManagerChild AssignAudioForTime(NAudio _clip, float _time)
        {
            if (_clip == null) return null;

            if (!IsAudioAvailableInPool(out AudioManagerChild _audioSource))
            {
                Debug.LogWarningFormat("No audio pool available");
                return null;
            }

            _audioSource.SetAudioLoop(_clip, _time);

            return _audioSource;
        }

        private void Start()
        {
            audioPool = new();
            for (var i = 0; i < audioPoolLength; i++)
            {
                audioPool.Add(CreateChild(i));
            }
        }

        private bool IsAudioAvailableInPool(out AudioManagerChild _audio)
        {
            _audio = null;
            foreach (var _audioItem in audioPool)//Audio disponible
            {
                if (_audioItem.Available)
                {
                    _audio = _audioItem;
                    return true;
                }
            }

            if (createAudioIfFullyAssigned)
            {
                audioPool.Add(CreateChild(audioPool.Count));
                return true;
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