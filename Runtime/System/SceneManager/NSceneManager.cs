using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nazio_LT.Tools.Core
{
    public class NSceneManager : Singleton<NSceneManager>
    {
        [SerializeField]private NSceneGroup[] sceneGroups;
        private static Dictionary<string, NSceneGroup> sceneGroupDict = new Dictionary<string, NSceneGroup>();

        protected override void Awake()
        {
            base.Awake();

            foreach (var _group in sceneGroups) 
            {
                if(sceneGroupDict.ContainsKey(_group.Key)) continue;
                sceneGroupDict.Add(_group.Key, _group);
            }
        }

        public static void LoadAdditiveSceneGroup(string _sceneGroupKeys)
        {
            if(!sceneGroupDict.ContainsKey(_sceneGroupKeys))
            {
                Debug.LogError($"{_sceneGroupKeys} doesn't exist.");
                return;
            }
            NSceneGroup _group = sceneGroupDict[_sceneGroupKeys];

            PrepareLoading(_group.Scenes, true, true);
        }

        public static void UnloadAndLoadNew(string _sceneGroupKeys)
        {
            if (!sceneGroupDict.ContainsKey(_sceneGroupKeys)) return;

            instance.StartCoroutine(instance.UnloadAndLoadNew(sceneGroupDict[_sceneGroupKeys].Scenes, 0.5f));
        }

        #region Customisable Methods

        protected virtual void BeforeLoading() { return; }
        protected virtual void AfterLoading() { return; }

        #endregion

        private static AsyncOperation[] PrepareLoading(string[] _sceneKeys, bool _allowSceneActivation, bool _onlyAdditive)
        {
            AsyncOperation[] _ops = new AsyncOperation[_sceneKeys.Length];
            for (int i = 0; i < _sceneKeys.Length; i++)
            {
                _ops[i] = SceneManager.LoadSceneAsync(_sceneKeys[i], (i == 0 && !_onlyAdditive) ? LoadSceneMode.Single : LoadSceneMode.Additive);
                _ops[i].allowSceneActivation = _allowSceneActivation;
                Debug.Log("Load " + _sceneKeys[i]);
            }
            return _ops;
        }

        #region Coroutines

        private IEnumerator UnloadAndLoadNew(string[] _sceneKeys, float _waitingDuration)
        {
            BeforeLoading();

            yield return new WaitForSeconds(_waitingDuration);

            AsyncOperation[] _loadingOps = PrepareLoading(_sceneKeys, false, false);
            yield return StartCoroutine(WaitAllOpsFinished(_loadingOps));

            AfterLoading();
        }

        private IEnumerator WaitAllOpsFinished(AsyncOperation[] _ops)
        {
            for (int i = 0; i < _ops.Length; i++) _ops[i].allowSceneActivation = true;

            for (int i = 0; i < _ops.Length; i++)
            {
                while (!_ops[i].isDone)
                {
                    yield return null;
                }
            }
        }

        #endregion
    }
}