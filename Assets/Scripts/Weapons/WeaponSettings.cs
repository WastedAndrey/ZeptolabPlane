
using Assets.Scripts.Weapons.Missiles;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [System.Serializable]
    public class WeaponSettings
    {
        [Header("Prefabs")]
        public MissileBase MissilePrefab;
        [Header("Values")]
        public float Cooldown = 1;
        public bool AmmoLimited = false;
        public int Ammo = 1;
    }
}