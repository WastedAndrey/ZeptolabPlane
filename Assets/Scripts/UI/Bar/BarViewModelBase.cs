
using Assets.Scripts.General;

namespace Assets.Scripts.UI.Bar
{
    public class BarViewModelBase : ViewModelBase
    {
        protected ReactiveProperty<float> _progress = new ReactiveProperty<float>();
        public IReactivePropertyReadOnly<float> Progress => _progress;

    }

}