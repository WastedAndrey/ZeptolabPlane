
using UnityEngine;

namespace Assets.Scripts.GameSettings
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Game/LevelSettings")]
    public class LevelSettings : ScriptableObject
    {
        [SerializeField]
        private Borders _playerBorders;
        [SerializeField]
        private Borders _enemyBorders;
        [SerializeField]
        private SpawnSettings _spawnSettings;
        [SerializeField]
        private float _waterHeight;
        [SerializeField]
        private float _underwaterSpeedMultiplier;

        public Borders PlayerBorders { get => _playerBorders; private set => _playerBorders = value; }
        public Borders EnemyBorders { get => _enemyBorders; set => _enemyBorders = value; }
        public SpawnSettings SpawnSettings { get => _spawnSettings; private set => _spawnSettings = value; }
        public float WaterHeight { get => _waterHeight; private set => _waterHeight = value; }
        public float UnderwaterSpeedMultiplier { get => _underwaterSpeedMultiplier; private set => _underwaterSpeedMultiplier = value; }
       
    }
}