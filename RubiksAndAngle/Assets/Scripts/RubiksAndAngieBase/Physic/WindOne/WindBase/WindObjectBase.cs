using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YProjectBase;

namespace RubiksAndAngie.RPhysic
{ 
    [RequireComponent(typeof(Rigidbody))]
    [DisallowMultipleComponent]
    public abstract class WindObjectBase : MonoBehaviour, IInit
    {

        protected Rigidbody rig;

        
        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            rig = GetComponent<Rigidbody>();
        }

        public abstract void SetWindVelocity(Transform _windTrans, Vector3 _velocity);


    }

}
