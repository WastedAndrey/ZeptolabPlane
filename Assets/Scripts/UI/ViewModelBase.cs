
namespace Assets.Scripts.UI
{
    public abstract class ViewModelBase: IViewModel
    {
        public virtual void OnEnable() { }

        public virtual void OnDisable() { }

        public virtual void OnDestroy() { }

    }
}