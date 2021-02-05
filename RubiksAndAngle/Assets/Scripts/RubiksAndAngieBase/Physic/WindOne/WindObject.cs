using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RubiksAndAngie.RPhysic
{

    public class WindObject : WindObjectBase
    {
        public override void SetWindVelocity(Transform _windTrans, Vector3 _velocity)
        {
            if (rig == null) return;
            rig.AddForce(_velocity, ForceMode.Impulse);
        }


        public bool IsByWind()
        {
            return false;
        }
    }

}
