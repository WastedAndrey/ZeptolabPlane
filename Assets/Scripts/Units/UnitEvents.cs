
using System;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class UnitEvents
    {
        public Action OnEnable;
        public Action OnDisable;
        public Action OnDestroy;
        public Action<float> Update;
        public Action<float> FixedUpdate;
        public Action<Collider> OnTriggerEnter;
        public Action<Collider> OnTriggerExit;
    }
}