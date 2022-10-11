using UnityEngine;
using UnityEngine.VFX;

namespace Nazio_LT.Tools.Core
{
    public static partial class NInstantiate
    {
        #region VFX

        /// <summary>Instantiate a Visual Effect in the world.</summary>
        public static VisualEffect InstantiateVFX(VisualEffectAsset _template, string _name, Vector3 _pos, Quaternion _rotation, Transform _parent)
        {
            Transform _obj = new GameObject(_name).transform;
            _obj.transform.position = _pos;
            _obj.transform.rotation = _rotation;
            if (_parent != null) _obj.transform.SetParent(_parent);

            VisualEffect _vfx = _obj.gameObject.AddComponent<VisualEffect>();
            _vfx.visualEffectAsset = _template;

            return _vfx;
        }

        /// <inheritdoc cref="NInstantiate.InstantiateVFX(VisualEffectAsset, string, Vector3, Quaternion, Transform)AverageZero(float)"/>
        public static VisualEffect InstantiateVFX(VisualEffectAsset _template, string _name, Vector3 _pos) => InstantiateVFX(_template, _name, _pos, Quaternion.identity, null);

        /// <inheritdoc cref="NInstantiate.InstantiateVFX(VisualEffectAsset, string, Vector3, Quaternion, Transform)AverageZero(float)"/>
        public static VisualEffect InstantiateVFX(VisualEffectAsset _template, string _name, Vector3 _pos, Transform _parent) => InstantiateVFX(_template, _name, _pos, Quaternion.identity, _parent);
    
        #endregion
    }
}