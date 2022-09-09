using System.Collections.Generic;
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

            curMusic = GetRandomMusicInPlaylist();
            PlayMusic();
        }

        public int GetRandomMusicInPlaylist()
        {
            if(curMusicsInPlaylist.Count <= 0)
            {
                Debug.LogError("No musics in playlist.");
                return 0;
            }
            if (curMusicsInPlaylist.Count == 1) return 0;

            int _rndm = Random.Range(0, curMusicsInPlaylist.Count);
            while(_rndm == curMusic)
            {
                _rndm = Random.Range(0, curMusicsInPlaylist.Count);
            }

            return _rndm;
        }

        public void PlayMusic()
        {
            source.PlayOneShot(curMusicsInPlaylist[curMusic].clip);
        }
    }
}