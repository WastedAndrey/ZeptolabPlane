
using UnityEngine;

namespace Assets.Scripts.Weapons.Missiles
{
    [System.Serializable]
    public class MissileSettings
    {
        public LayerMask LayerMask;
        public float MovementSpeed;
        public float RotationSpeed;
        public int Damage;
        public float Lifetime;
        public float Health;
        public bool DestroyInWater = true;
    }
}