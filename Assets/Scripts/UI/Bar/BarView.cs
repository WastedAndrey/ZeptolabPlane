
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Bar
{
    public class BarView : ViewBase<BarViewModelBase>
    {
        [SerializeField]
        private Image _fillImage;

        protected override void InitInternal()
        {
            OnValueChange(_viewModel.Progress.Value);
        }

        protected override void Subscribe()
        {
            _viewModel.Progress.Changed += OnValueChange;
        }

        protected override void Unsubscribe()
        {
            _viewModel.Progress.Changed -= OnValueChange;
        }

        private void OnValueChange(float value)
        {
            _fillImage.fillAmount = value;
        }
    }
}