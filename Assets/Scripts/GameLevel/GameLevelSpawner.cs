
using Unit = Assets.Scripts.Units.Unit;
using UnityEngine;
using Assets.Scripts.GameSettings;
using System.Collections.Generic;
using System;
using Assets.Scripts.General;

namespace Assets.Scripts.GameLevel
{
    public class GameLevelSpawner : MonoBehaviour, IDisposable
    {
        private Unit _player;
        private LevelSettings _levelSettings;
        private SpawnSettings _spawnSettings;

        private bool _isActive = false;
        private SpawnData _spawnData;
        private int _spawnDataIndex = 0;
        private float _spawnDelay = 0;

        private List<Unit> _units = new List<Unit>();

        ReactiveProperty<float> _progress = new ReactiveProperty<float>();
        public IReactivePropertyReadOnly<float> Progress => _progress;

        public event Action LevelFinished;

        public void Init(Unit player, LevelSettings levelSettings)
        {
            _player = player;
            _levelSettings = levelSettings;
            _spawnSettings = levelSettings.SpawnSettings;
            _isActive = true;
            SetSpawnData(_spawnDataIndex);
        }

        private void Update()
        {
            if (_isActive == false)
                return;

            UpdateSpawn();
            UpdateProgress();
        }

        private void UpdateSpawn()
        {
            _spawnDelay -= Time.deltaTime;
            if (_spawnDelay <= 0)
            {
                SpawnAll();
                SetSpawnData(_spawnDataIndex);
            }
        }

        private void UpdateProgress()
        {
            _progress.Value = 
                ((float)_spawnDataIndex - 1) / (float)_spawnSettings.SpawnDataCount 
                + (1 - _spawnDelay / _spawnData.TimeDelay) / (float)_spawnSettings.SpawnDataCount;
        }

        private void SpawnAll()
        {
            if (_spawnDataIndex < _spawnSettings.SpawnDataCount - 1)
                _spawnDataIndex++;
            else _isActive = false;

            for (int i = 0; i < _spawnData.UnitsCount; i++)
            {
                SpawnUnit(_spawnData.UnitPrefab);
            }
        }

        private void SpawnUnit(Unit prefab)
        {
            Unit newUnit = Instantiate(prefab, GetSpawnPosition(), GetSpawnRotation());
            newUnit.Died += OnUnitDied;
            newUnit.Init(_levelSettings);
            _units.Add(newUnit);
        }

        private void OnUnitDied(Unit unit)
        {
            unit.Died -= OnUnitDied;
            if (_units.Contains(unit))
                _units.Remove(unit);

            if (_units.Count == 0 && _isActive == false)
            {
                LevelFinished?.Invoke();
            }
        }

        private void SetSpawnData(int index)
        {
            _spawnData = _spawnSettings.GetSpawnData(index);
            _spawnDelay = _spawnData.TimeDelay;
            _spawnDataIndex = index;
        }

      

        private Vector3 GetSpawnPosition()
        {
            Vector3 newPosition = new Vector3();

            newPosition.x = UnityEngine.Random.Range(
                _spawnSettings.EnemiesSpawnBorders.BorderHorizontal.x,
                _spawnSettings.EnemiesSpawnBorders.BorderHorizontal.y);
            newPosition.y = UnityEngine.Random.Range(
               _spawnSettings.EnemiesSpawnBorders.BorderVertical.x,
               _spawnSettings.EnemiesSpawnBorders.BorderVertical.y);

            newPosition.z = _player.transform.position.z;

            newPosition += _spawnSettings.EnemiesSpawnOffset;

            return newPosition;
        }

        private Quaternion GetSpawnRotation()
        {
            return Quaternion.AngleAxis(180, Vector3.up);
        }

        public void Dispose()
        {
            for (int i = 0; i < _units.Count; i++)
            {
                _units[i].Died -= OnUnitDied;
            }
        }
    }
}