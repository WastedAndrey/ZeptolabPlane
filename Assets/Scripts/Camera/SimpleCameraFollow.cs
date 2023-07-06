using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class SimpleCameraFollow : CameraBase
    {
        [SerializeField]
        private Vector3 _offset;

        private Transform _target;

        public override void Init(Transform target)
        {
            _target = target;
        }

        private void LateUpdate()
        {
            if (_target == null)
                return;

            Vector3 position = this.transform.position;
            position.z = _target.transform.position.z;
            position += _offset;
            this.transform.position = position;
        }
    }
}
