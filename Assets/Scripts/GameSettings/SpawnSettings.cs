
using Assets.Scripts.Units;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameSettings
{
    [System.Serializable]
    public class SpawnData
    {
        [SerializeField]
        private Unit _unitPrefab;
        [SerializeField]
        private int _unitsCount;
        [SerializeField]
        private float _timeDelay;

        public Unit UnitPrefab { get => _unitPrefab; private set => _unitPrefab = value; }
        public int UnitsCount { get => _unitsCount; private set => _unitsCount = value; }
        public float TimeDelay { get => _timeDelay; private set => _timeDelay = value; }
    }

    [System.Serializable]
    [CreateAssetMenu(fileName = "SpawnSettings", menuName = "Game/SpawnSettings")]
    public class SpawnSettings : ScriptableObject
    {
        [SerializeField]
        private Borders _enemiesSpawnBorders;
        [SerializeField]
        private Vector3 _enemiesSpawnOffset;
        [SerializeField]
        private List<SpawnData> _spawnData = new List<SpawnData>();

        public SpawnData GetSpawnData(int index)
        {
            return _spawnData[index];
        }

        public int SpawnDataCount => _spawnData.Count;

        public Borders EnemiesSpawnBorders  => _enemiesSpawnBorders; 
        public Vector3 EnemiesSpawnOffset => _enemiesSpawnOffset;
    }
}