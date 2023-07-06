
namespace Assets.Scripts.Units.States
{
    public class UnitStateBase
    {
        public virtual void OnEnable() { }
        public virtual void OnDisable() { }
        public virtual void OnDestroy() { }
        public virtual void Update(float deltaTime) { }
        public virtual void FixedUpdate(float deltaTime) { }
    }
}