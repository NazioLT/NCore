using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nazio_LT.Tools.Core
{
    [AddComponentMenu("Nazio_LT/Core/NSceneManager")]
    public class NSceneManager : Singleton<NSceneManager>
    {
        [SerializeField] private NSceneGroup[] sceneGroups;
        [SerializeField] private float transitionDuration = 0.5f;
        private static Dictionary<string, NSceneGroup> sceneGroupDict = new Dictionary<string, NSceneGroup>();

        #region Public Methods

        /// <summary>
        /// Load some scene in additive mode.
        /// </summary>
        public static void LoadAdditiveSceneGroup(string _sceneGroupKeys)
        {
            if (!sceneGroupDict.ContainsKey(_sceneGroupKeys))
            {
                Debug.LogError($"{_sceneGroupKeys} doesn't exist.");
                return;
            }
            NSceneGroup _group = sceneGroupDict[_sceneGroupKeys];

            LoadAdditiveSceneGroup(_group);
        }

        public static void LoadAdditiveSceneGroup(NSceneGroup _group) => ExecuteIfInstance(() => LoadScenes(_group.Scenes, true, true));

        /// <summary>
        /// Unload the current scene and load new scene group.
        /// </summary>
        public static void UnloadAndLoadNew(string _sceneGroupKeys)
        {
            if (!sceneGroupDict.ContainsKey(_sceneGroupKeys)) return;

            ExecuteIfInstance(() => instance.StartCoroutine(instance.UnloadAndLoadNew(sceneGroupDict[_sceneGroupKeys])));
        }

        #endregion

        #region Overridable Methods

        /// <summary>
        /// Function wich is executed before loading the scene group.
        /// </summary>
        /// <param name="_sceneGroup"></param>
        protected virtual void BeforeLoading(NSceneGroup _sceneGroup) { return; }

        /// <summary>
        /// Function wich is executed during the scene group loading.
        /// </summary>
        /// <param name="_sceneGroup"></param>
        protected virtual void SetLoadingPercent(float _loadingPercent) { return; }

        /// <summary>
        /// Function wich is executed when the scene group is loaded.
        /// </summary>
        /// <param name="_sceneGroup"></param>
        protected virtual void AfterLoading(NSceneGroup _sceneGroup) => MusicManager.ChangePlaylist(_sceneGroup.ScenePlayList);

        #endregion

        #region Protected Fix Methods

        protected override void Awake()
        {
            base.Awake();

            InitSceneGroups();
        }

        protected void InitSceneGroups()
        {
            foreach (var _group in sceneGroups)
            {
                if (sceneGroupDict.ContainsKey(_group.Key)) continue;
                sceneGroupDict.Add(_group.Key, _group);
            }
        }

        #endregion

        #region Coroutines

        private IEnumerator UnloadAndLoadNew(NSceneGroup _sceneGroup)
        {
            string[] _scenes = _sceneGroup.Scenes;

            BeforeLoading(_sceneGroup);

            yield return new WaitForSeconds(transitionDuration);

            AsyncOperation[] _loadingOps = LoadScenes(_scenes, false, false);
            yield return StartCoroutine(WaitAllOpsFinished(_loadingOps));

            AfterLoading(_sceneGroup);
        }

        private IEnumerator WaitAllOpsFinished(AsyncOperation[] _ops)
        {
            float _loadingPercent = 0f;
            float _factor = 1f / (float)_ops.Length;
            for (int i = 0; i < _ops.Length; i++) _ops[i].allowSceneActivation = true;

            for (int i = 0; i < _ops.Length; i++)
            {
                while (!_ops[i].isDone)
                {
                    yield return null;
                    SetLoadingPercent(_ops[i].progress * _factor + _loadingPercent);
                }
                _loadingPercent += _factor;
            }
        }

        #endregion

        #region Private Methods

        private static AsyncOperation[] LoadScenes(string[] _sceneKeys, bool _allowSceneActivation, bool _onlyAdditive)
        {
            AsyncOperation[] _ops = new AsyncOperation[_sceneKeys.Length];
            for (int i = 0; i < _sceneKeys.Length; i++)
            {
                _ops[i] = SceneManager.LoadSceneAsync(_sceneKeys[i], (i == 0 && !_onlyAdditive) ? LoadSceneMode.Single : LoadSceneMode.Additive);
                _ops[i].allowSceneActivation = _allowSceneActivation;
            }
            return _ops;
        }

        #endregion
    }
}