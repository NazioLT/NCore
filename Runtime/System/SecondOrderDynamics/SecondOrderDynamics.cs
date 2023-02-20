using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public static class SecondOrderDynamicsUtils
    {
        public static Vector3 Update(this SecondOrderDynamics<Vector3> _dynamics, float _t, Vector3 _x) => SecondOrderDynamics<Vector3>.Update(_t, _dynamics, _x);
        public static Vector2 Update(this SecondOrderDynamics<Vector2> _dynamics, float _t, Vector2 _x) => SecondOrderDynamics<Vector2>.Update(_t, _dynamics, _x);
        public static float Update(this SecondOrderDynamics<float> _dynamics, float _t, float _x) => SecondOrderDynamics<float>.Update(_t, _dynamics, _x);
    }

    [System.Serializable]
    public class SecondOrderDynamics<T>
    {
        public SecondOrderDynamics(float _f, float _z, float _r, T _initialPosition)
        {
            //Init variables
            Init(_initialPosition);

            data = new SecondOrderDynamicsData(_f, _z, _r);
        }

        [SerializeField] private SecondOrderDynamicsData data;

        private T xp;//Previous Input
        private T y, yd;//State variables

        public void Init(T _initialPosition)
        {
            xp = _initialPosition;
            y = _initialPosition;
            yd = (T)default;
        }

        public static Vector3 Update(float _t, SecondOrderDynamics<Vector3> _dynamics, Vector3 _x)
        {
            SecondOrderDynamicsData _data = _dynamics.data;

            //Estimated Velocity
            Vector3 xd = (_x - _dynamics.xp) / _t;
            _dynamics.xp = _x;

            _dynamics.y += _t * _dynamics.yd;//integrate position by velocity
            _dynamics.yd += _t * (_x + _data.K3 * xd - _dynamics.y - _data.K1 * _dynamics.yd) / _data.K2Stable(_t);//integrate velocity by acceleration

            return _dynamics.y;
        }

        public static Vector2 Update(float _t, SecondOrderDynamics<Vector2> _dynamics, Vector2 _x)
        {
            SecondOrderDynamicsData _data = _dynamics.data;

            //Estimated Velocity
            Vector2 xd = (_x - _dynamics.xp) / _t;
            _dynamics.xp = _x;

            _dynamics.y += _t * _dynamics.yd;//integrate position by velocity
            _dynamics.yd += _t * (_x + _data.K3 * xd - _dynamics.y - _data.K1 * _dynamics.yd) / _data.K2Stable(_t);//integrate velocity by acceleration

            return _dynamics.y;
        }

        public static float Update(float _t, SecondOrderDynamics<float> _dynamics, float _x)
        {
            SecondOrderDynamicsData _data = _dynamics.data;

            //Estimated Velocity
            float xd = (_x - _dynamics.xp) / _t;
            _dynamics.xp = _x;

            _dynamics.y += _t * _dynamics.yd;//integrate position by velocity
            _dynamics.yd += _t * (_x + _data.K3 * xd - _dynamics.y - _data.K1 * _dynamics.yd) / _data.K2Stable(_t);//integrate velocity by acceleration

            return _dynamics.y;
        }
    }

}