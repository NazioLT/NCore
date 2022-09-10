using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    [CreateAssetMenu(fileName = "SceneGroup", menuName = "Nazio_LT/Tools/SceneGroup")]
    public class NSceneGroup : ScriptableObject
    {
        [SerializeField] private string key;
        [SerializeField] private string mainScene;
        [SerializeField] private string[] additivesScenes;

        public string[] Scenes
        {
            get
            {
                string[] _result = new string[additivesScenes.Length + 1];
                _result[0] = mainScene;
                for (int i = 0; i < additivesScenes.Length; i++)
                {
                    _result[i+ 1] = additivesScenes[i];
                }

                return _result;
            }
        }

        public string Key => key;
    }
}