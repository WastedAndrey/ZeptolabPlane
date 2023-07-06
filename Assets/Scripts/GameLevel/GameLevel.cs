using Assets.Scripts.Camera;
using Assets.Scripts.GameSettings;
using Assets.Scripts.Services;
using Assets.Scripts.UI;
using UnityEngine;
using Unit = Assets.Scripts.Units.Unit;

namespace Assets.Scripts.GameLevel
{
    public class GameLevel : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField]
        private LevelSettings _levelSettings;
        [SerializeField]
        private GameLevelSpawner _spawner;
        [SerializeField]
        private CameraBase _camera;
        [Header("Prefabs")]
        [SerializeField]
        private GameWindowView _gameWindowPrefab;
        [SerializeField]
        private Unit _playerPrefab;

        private Unit _player;
        

        private void Start()
        {
            InitPlayer();
            InitEnemies();
            InitUI();
            InitCamera();
        }
        private void OnDestroy()
        {
            _player.Died -= OnPlayerDied;
            _spawner.LevelFinished -= OnLevelCompleted;
            _spawner.Dispose();
        }

        private void InitUI()
        {
            var uiService = ServiceLocator.Instance.GetService<UIManager>();
            uiService.CloseAllViews();

            uiService.CreateView(_gameWindowPrefab, new GameWindowViewModel(_player, _spawner));
        }

        private void InitPlayer()
        {
            _player = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
            _player.Died += OnPlayerDied;
            _player.Init(_levelSettings);
        }

        private void InitCamera()
        {
            _camera.Init(_player.transform);
        }

        private void InitEnemies()
        {
            _spawner.Init(_player, _levelSettings);
            _spawner.LevelFinished += OnLevelCompleted;
        }

 

        private void OnPlayerDied(Unit unit)
        {
            ReplayLevel();
        }

        private void OnLevelCompleted()
        {
            NextLevel();
        }
        private void ReplayLevel()
        {
            var levelManager = ServiceLocator.Instance.GetService<GameManager>();
            levelManager.ReplayLevel();
        }
        private void NextLevel()
        {
            var levelManager = ServiceLocator.Instance.GetService<GameManager>();
            levelManager.NextLevel();
        }
    }
}