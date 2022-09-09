using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    [System.Serializable]
    public struct NMusic
    {
        public AudioClip clip;
        public int playlist;
    }

    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : Singleton<MusicManager>
    {
        [SerializeField] private NMusic[] music;
        [SerializeField] private List<string> playlists = new List<string>();
        public static string[] playlistArray;

        private List<NMusic> curMusicsInPlaylist;

        private int curPlaylist = 0;
        private int curMusic;

        private AudioSource source;

        protected override void Awake()
        {
            base.Awake();

            source = GetComponent<AudioSource>();
            ChangePlaylist(playlists[0]);
        }

        private void OnValidate()
        {
            List<string> _corrected = new List<string>();
            for (int i = 0; i < playlists.Count; i++)
            {
                if (!_corrected.Contains(playlists[i])) _corrected.Add(playlists[i]);
            }

            playlistArray = new string[_corrected.Count];
            for (int i = 0; i < _corrected.Count; i++)
            {
                playlistArray[i] = _corrected[i];
            }
        }

        public void ChangePlaylist(string _playlist)
        {
            curPlaylist = playlists.IndexOf(_playlist);
            curMusicsInPlaylist = new List<NMusic>();
            for (int i = 0; i < music.Length; i++)
            {
                if (music[i].playlist == curPlaylist) curMusicsInPlaylist.Add(music[i]);
            }
            PlayNewMusic();
        }

        public int GetRandomMusicInPlaylist()
        {
            if (curMusicsInPlaylist.Count <= 0)
            {
                Debug.LogError("No musics in playlist.");
                return 0;
            }
            if (curMusicsInPlaylist.Count == 1) return 0;

            int _rndm = Random.Range(0, curMusicsInPlaylist.Count);
            while (_rndm == curMusic)
            {
                _rndm = Random.Range(0, curMusicsInPlaylist.Count);
            }

            return _rndm;
        }

        public void PlayNewMusic()
        {
            curMusic = GetRandomMusicInPlaylist();
            StartCoroutine(PlayMusic(5f));
        }

        private IEnumerator PlayMusic(float _fadeDuration)
        {
            source.volume = 0f;
            AudioClip _clip = curMusicsInPlaylist[curMusic].clip;
            source.PlayOneShot(_clip);
            float _time = _clip.length;

            StartCoroutine(LerpVolume(0f, 1f, _fadeDuration));

            yield return new WaitForSeconds(_time - _fadeDuration);

            StartCoroutine(LerpVolume(1f, 0f, _fadeDuration));

            yield return new WaitForSeconds(_fadeDuration);

            PlayNewMusic();
        }

        private IEnumerator LerpVolume(float _start, float _end, float _duration)
        {
            float _t = 0f;
            while (true)
            {
                _t += Time.deltaTime / _duration;

                yield return null;

                if (_t >= 1f) break;

                source.volume = Mathf.Lerp(_start, _end, _t);
            }

            source.volume = _end;
        }
    }
}