using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    [System.Serializable]
    public class SecondOrderDynamics
    {
        public SecondOrderDynamics(float _f, float _z, float _r, Vector3 _initialPosition)
        {
            //Init variables
            Init(_initialPosition);

            data = new SecondOrderDynamicsData(_f, _z, _r);
        }

        [SerializeField] private SecondOrderDynamicsData data;

        private Vector3 xp;//Previous Input
        private Vector3 y, yd;//State variables

        public void Init(Vector3 _initialPosition)
        {
            xp = _initialPosition;
            y = _initialPosition;
            yd = Vector3.zero;
        }

        public Vector3 Update(float _t, Vector3 x)
        {
            //Estimated Velocity
            Vector3 xd = (x - xp) / _t;
            xp = x;

            float k2_stable = Mathf.Max(data.K2, _t * _t / 2 + _t * data.K1 / 2, _t * data.K1);//clamp k2 to guarantee stabity without jitter
            y = y + _t * yd;//integrate position by velocity
            yd = yd + _t * (x + data.K3 * xd - y - data.K1 * yd) / k2_stable;//integrate velocity by acceleration

            return y;
        }
    }

}