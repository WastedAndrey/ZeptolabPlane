
using Assets.Scripts.GameSettings;
using Assets.Scripts.Weapons.Factories;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [System.Serializable]
    public class WeaponInitData
    {
        [SerializeField]
        private WeaponFactory _factory;
        [SerializeField]
        private Transform _shootingPoint;

        public Weapon Create(LevelSettings levelSettings) => _factory.Create(levelSettings, _shootingPoint);
    }
}