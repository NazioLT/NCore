using UnityEngine;
using System.Collections.Generic;

namespace Nazio_LT.Tools.Core
{
    [AddComponentMenu("Nazio_LT/Core/NAudioManager")]
    public class NAudioManager : Singleton<NAudioManager>
    {
        [SerializeField, Range(1, 32)] private int m_audioPoolLength = 1;
        [SerializeField] private bool m_createAudioIfFullyAssigned = true;
        private List<AudioManagerChild> m_audioPool;

        public void PlayAudioOneShot(NAudio clip)
        {
            if (clip == null) return;

            if (!IsAudioAvailableInPool(out AudioManagerChild audioSource))
            {
                Debug.LogWarningFormat("No audio pool available");
                return;
            }

            audioSource.SetAudio(clip);
        }

        public AudioManagerChild AssignAudioForTime(NAudio clip, float time)
        {
            if (clip == null) return null;

            if (!IsAudioAvailableInPool(out AudioManagerChild audioSource))
            {
                Debug.LogWarningFormat("No audio pool available");
                return null;
            }

            audioSource.SetAudioLoop(clip, time);

            return audioSource;
        }

        private void Start()
        {
            m_audioPool = new();
            for (var i = 0; i < m_audioPoolLength; i++)
            {
                m_audioPool.Add(CreateChild(i));
            }
        }

        private bool IsAudioAvailableInPool(out AudioManagerChild audio)
        {
            audio = null;
            foreach (var audioItem in m_audioPool)//Audio disponible
            {
                if (audioItem.Available)
                {
                    audio = audioItem;
                    return true;
                }
            }

            if (m_createAudioIfFullyAssigned)
            {
                m_audioPool.Add(CreateChild(m_audioPool.Count));
                return true;
            }

            return false;
        }

        private AudioManagerChild CreateChild(int id)
        {
            GameObject obj = new GameObject($"NAudio - {id}");
            obj.transform.parent = transform;

            return obj.AddComponent<AudioManagerChild>();
        }
    }
}