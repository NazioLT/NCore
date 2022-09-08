using UnityEngine;
using Nazio_LT.Tools.Core.Internal;

namespace Nazio_LT.Tools.Core
{
    public class AudioManager : Singleton<AudioManager>
    {
        public void PlayAudio(AudioClip _clip) => CreateChild(_clip.name).SetAudio(_clip);

        private AudioManagerChild CreateChild(string _name)
        {
            GameObject _obj = new GameObject($"NAudio - {_name}");
            _obj.transform.parent = transform;

            return _obj.AddComponent<AudioManagerChild>();
        }
    }
}