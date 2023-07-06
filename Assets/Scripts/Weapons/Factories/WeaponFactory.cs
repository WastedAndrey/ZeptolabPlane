
using Assets.Scripts.GameSettings;
using UnityEngine;

namespace Assets.Scripts.Weapons.Factories
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "WeaponFactory", menuName = "Game/WeaponFactory")]
    public class WeaponFactory : ScriptableObject
    {
        [SerializeField]
        protected WeaponSettings _settings;
        [SerializeField]
        protected WeaponAISettings _aiSettings;

        public Weapon Create(LevelSettings levelSettings, Transform shootingPoint)
        {
            Weapon newWeapon = new Weapon(_settings, _aiSettings, levelSettings, shootingPoint);
            return newWeapon;
        }
    }
}