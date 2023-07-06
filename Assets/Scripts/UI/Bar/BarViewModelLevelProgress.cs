
using Assets.Scripts.GameLevel;

namespace Assets.Scripts.UI.Bar
{
    public class BarViewModelLevelProgress : BarViewModelBase
    {
        private GameLevelSpawner _spawner;

        public BarViewModelLevelProgress(GameLevelSpawner spawner)
        {
            _spawner = spawner;
        }

        public override void OnEnable()
        {
            _spawner.Progress.Changed += OnChanged;
        }

        public override void OnDisable()
        {
            _spawner.Progress.Changed -= OnChanged;
        }

        private void OnChanged(float value)
        {
            _progress.Value = value;
        }
    }
}