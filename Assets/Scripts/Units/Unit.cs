using Assets.Scripts.GameSettings;
using Assets.Scripts.Units.Components;
using Assets.Scripts.Weapons;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Unit : MonoBehaviour, IDamagable
    {
        [SerializeField]
        private List<ComponentFactoryBase> _componentFactories;
        [SerializeField]
        private UnitContext _unitContext;
        [SerializeField]
        private List<ComponentBase> _components = new List<ComponentBase>();

        public bool IsAlive { get => _unitContext.IsAlive; private set => _unitContext.IsAlive = value; }
        public Team Team { get => _unitContext.Team; private set => _unitContext.Team = value; }
        public GameObject GameObject => this.gameObject;
        public int HealthMax => _unitContext.StatsDefault.Health;
        public int Health => _unitContext.StatsCurrent.Health;
        public Action<Unit, int> DamageReceived;
        public Action<Unit> Died;

        public void Init(LevelSettings levelSettings)
        {
            _unitContext.LevelSettings = levelSettings;
            _unitContext.StatsCurrent = _unitContext.StatsDefault.GetCopy();

            InitWeapons();
            InitComponents();
        }

        private void InitWeapons()
        {
            for (int i = 0; i < _unitContext.WeaponsInitDataSet.Count; i++)
            {
                _unitContext.Weapons.Add(_unitContext.WeaponsInitDataSet[i].Create(_unitContext.LevelSettings));
            }
        }

        private void InitComponents()
        {
            for (int i = 0; i < _componentFactories.Count; i++)
            {
                _components.Add(_componentFactories[i].Create(this, _unitContext));
            }
        }

        private void OnEnable()
        {
            _unitContext.UnitEvents.OnEnable?.Invoke();
        }
        private void OnDisable()
        {
            _unitContext.UnitEvents.OnDisable?.Invoke();
        }
        private void Update()
        {
            UpdateWeapons();
            _unitContext.UnitEvents.Update?.Invoke(Time.deltaTime);
        }

        private void UpdateWeapons()
        {
            for (int i = 0; i < _unitContext.Weapons.Count; i++)
            {
                _unitContext.Weapons[i].Update(Time.deltaTime, _unitContext.StatsCurrent.AttackSpeed);
            }
        }

        private void FixedUpdate()
        {
            _unitContext.UnitEvents.FixedUpdate?.Invoke(Time.fixedDeltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            _unitContext.UnitEvents.OnTriggerEnter?.Invoke(other);
        }


        private void OnTriggerExit(Collider other)
        {
            _unitContext.UnitEvents.OnTriggerExit?.Invoke(other);
        }

        private void OnDestroy()
        {
            _unitContext.UnitEvents.OnDestroy?.Invoke();
            foreach (var component in _components)
            {
                component.Destroy();
            }
        }
        public void ReceiveDamage(int damage)
        {
            if (IsAlive == false)
                return;

            _unitContext.StatsCurrent.Health--;
            DamageReceived?.Invoke(this, damage);
            if (_unitContext.StatsCurrent.Health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            if (IsAlive == false)
                return;

            IsAlive = false;
            CreateDeathEffect();
            Died?.Invoke(this);
            Destroy(this.gameObject);
        }

        private void CreateDeathEffect()
        {
            if (_unitContext.DeathEffectPrefab != null)
                Instantiate(_unitContext.DeathEffectPrefab, this.transform.position, Quaternion.identity);
        }
    }
}
