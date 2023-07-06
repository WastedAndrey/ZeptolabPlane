using Assets.Scripts.GameSettings;
using Assets.Scripts.Services;
using UnityEngine;


namespace Assets.Scripts.Weapons
{
    [System.Serializable]
    public class Weapon
    {
        protected WeaponSettings _settings;
        protected WeaponAISettings _aiSettings;
        protected LevelSettings _levelSettings;
        protected Transform _shootingPoint;
        protected float _cooldownCurrent = 0;
        protected float _cooldownAICurrent = 0;
        protected int _ammo = 0;

        protected bool CheckAmmo => _settings.AmmoLimited == false || _ammo > 0;
        protected bool CheckCooldown => _cooldownCurrent <= 0;
        protected bool CheckAICooldown => _cooldownAICurrent <= 0;

        public Weapon(WeaponSettings settings, WeaponAISettings aiSettings, LevelSettings levelSettings, Transform shootingPoint)
        {
            _settings = settings;
            _aiSettings = aiSettings;
            _levelSettings = levelSettings;
            _shootingPoint = shootingPoint;
        }

        public void Init()
        {
            _ammo = _settings.Ammo;
        }

        public void Update(float time, float attackSpeed)
        {
            if(_cooldownCurrent > 0)
                _cooldownCurrent -= time * attackSpeed;

            if (_cooldownAICurrent > 0)
                _cooldownAICurrent -= time * attackSpeed;
        }

        public void Shoot(Team team, Vector3 direction)
        {
            if (!CheckAmmo || !CheckCooldown)
                return;

            _cooldownCurrent = _settings.Cooldown;
            _cooldownAICurrent = _settings.Cooldown + Random.Range(_aiSettings.RandomCooldown.x, _aiSettings.RandomCooldown.y);
            
            Vector3 position = _shootingPoint.position;
            Quaternion rotation = _shootingPoint.rotation;
            var missile = ServiceLocator.Instance.GetService<ObjectManager>().InstantiatePrefab(_settings.MissilePrefab, position, rotation, null);
            missile.Init(team, direction, _levelSettings);
        }

        public void ShootAI(Team team, Vector3 direction)
        {
            if (!CheckAICooldown)
                return;

            Shoot(team, direction);
        }
    }
}
