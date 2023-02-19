using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public struct SecondOrderDynamics
    {
        public SecondOrderDynamics(float _f, float _z, float _r, Vector3 _x0)
        {
            float PI2F = 2 * Mathf.PI * _f;
            //compute constants
            k1 = _z / (Mathf.PI * _f);
            k2 = 1 / (PI2F * PI2F);
            k3 = _r * _z / PI2F;

            //Init variables
            xp = _x0;
            y = _x0;
            yd = Vector3.zero;
        }

        private Vector3 xp;//Previous Input
        private Vector3 y, yd;//State variables
        private readonly float k1, k2, k3;

        public Vector3 Update(float _t, Vector3 x)
        {
            //Estimated Velocity
            Vector3 xd = (x - xp) / _t;
            xp = x;

            float k2_stable = Mathf.Max(k2, _t * _t / 2 + _t * k1 / 2, _t * k1);//clamp k2 to guarantee stabity without jitter
            y = y + _t * yd;//integrate position by velocity
            yd = yd + _t * (x + k3 * xd - y - k1 * yd) / k2_stable;//integrate velocity by acceleration

            return y;
        }
    }

}