
using Assets.Scripts.GameSettings;
using Assets.Scripts.Weapons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Units
{
    [System.Serializable]
    public class UnitContext
    {
        [Header("Dependencies")]
        [SerializeField]
        public Rigidbody Rigidbody;
        [SerializeField]
        public Transform Model;
        [HideInInspector]
        public LevelSettings LevelSettings;

        [Header("Prefabs")]
        [SerializeField]
        public GameObject DeathEffectPrefab;

        [Header("Values")]
        [SerializeField]
        public Team Team;
        [SerializeField]
        public UnitStats StatsDefault;
        [SerializeField]
        public UnitStats StatsCurrent;
        [SerializeField]
        public List<WeaponInitData> WeaponsInitDataSet = new List<WeaponInitData>();
        [HideInInspector]
        public List<Weapon> Weapons = new List<Weapon>();

        public UnitEvents UnitEvents = new UnitEvents();
        public bool IsAlive = true;
    }
}