using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nazio_LT.Tools.Core
{
    public class NSceneManager : Singleton<NSceneManager>
    {
        public static void AddScene(string _sceneKey) => SceneManager.LoadSceneAsync(_sceneKey, LoadSceneMode.Additive);
        public static void UnloadAndLoadNew(string[] _sceneKeys)
        {
            if (_sceneKeys == null || _sceneKeys.Length == 0) return;

            instance.StartCoroutine(instance.UnloadAndLoadNew(_sceneKeys, 0.5f));
        }

        #region Customisable Methods

        protected virtual void BeforeLoading() { return; }
        protected virtual void AfterLoading() { return; }

        #endregion

        private AsyncOperation[] PrepareLoading(string[] _sceneKeys)
        {
            AsyncOperation[] _ops = new AsyncOperation[_sceneKeys.Length];
            for (int i = 0; i < _sceneKeys.Length; i++)
            {
                _ops[i] = SceneManager.LoadSceneAsync(_sceneKeys[i], i == 0 ? LoadSceneMode.Single : LoadSceneMode.Additive);
                _ops[i].allowSceneActivation = false;
            }
            return _ops;
        }

        #region Coroutines

        private IEnumerator UnloadAndLoadNew(string[] _sceneKeys, float _waitingDuration)
        {
            BeforeLoading();

            yield return new WaitForSeconds(_waitingDuration);

            AsyncOperation[] _loadingOps = PrepareLoading(_sceneKeys);
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