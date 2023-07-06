
using Assets.Scripts.GameSettings;
using Assets.Scripts.Units;
using UnityEngine;

namespace Assets.Scripts.Weapons.Missiles
{
    public class MissileBase : MonoBehaviour, IDamagable
    {
        [Header("Dependencies")]
        [SerializeField]
        protected Rigidbody _rigidBody;
        [SerializeField]
        protected GameObject _explosionEffect;
        [Header("Values")]
        [SerializeField]
        protected MissileSettings _settings;
        protected LevelSettings _levelSettings;
        protected Team _team;
        protected Vector3 _direction;
        protected bool _isAlive = true;

        public Team Team => _team;
        public GameObject GameObject => this.gameObject;

        public void Init(Team team, Vector3 direction, LevelSettings levelSettings)
        {
            _team = team;
            _direction = direction;
            _levelSettings = levelSettings;

            InitInternal();
            UpdateMovement();
          
        }

        protected virtual void InitInternal()
        { }

        private void Update()
        {
            UpdateUnderwater();
            UpdateInternal();
        }
        protected virtual void UpdateInternal() { }

        protected virtual void UpdateMovement()
        {
            _rigidBody.velocity = _direction * _settings.MovementSpeed;
        }

        protected virtual void UpdateUnderwater()
        {
            if (_settings.DestroyInWater && this.transform.position.y <= _levelSettings.WaterHeight)
            {
                Die();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isAlive == false)
                return;

            Rigidbody targetRb = other.attachedRigidbody;
            if (targetRb == null)
                return;

            IDamagable target = targetRb.GetComponent<IDamagable>();
            if (target != null && target.Team != Team)
            {
                DealDamage(target);
                CreateExplosionEffect();
                Die();
            }
               
        }

        protected void DealDamage(IDamagable target)
        {
            target.ReceiveDamage(_settings.Damage);
        }

        public void ReceiveDamage(int damage)
        {
            if (_isAlive == false)
                return;

            _settings.Health--;
            if (_settings.Health <= 0)
                Die();
        }

        protected void CreateExplosionEffect()
        {
            if (_explosionEffect != null) 
                Instantiate(_explosionEffect, this.transform.position, Quaternion.identity);
        }

        protected void Die()
        {
            if (_isAlive == false)
                return;

            _isAlive = false;
            Destroy(this.gameObject);
        }
    }
}