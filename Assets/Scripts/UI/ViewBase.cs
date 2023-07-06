
using System;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public abstract class ViewBase<T> : MonoBehaviour, IView where T : IViewModel
    {
        protected T _viewModel;

        public event Action<IView> Closed;
        protected bool _isInited = false;

        public void Init(T viewModel)
        {
            _viewModel = viewModel;

            InitInternal();
            _isInited = true;
            OnEnable();
        }

        protected virtual void InitInternal()
        { 
        }

        private void OnEnable()
        {
            if (_isInited)
            {
                Subscribe();
                _viewModel.OnEnable();
                OnEnableInternal();
            }
        }

        protected virtual void OnEnableInternal() { }

        private void OnDisable()
        {
            if (_isInited)
            {
                Unsubscribe();
                _viewModel.OnDisable();
                OnDisableInternal();
            }
        }

        protected virtual void OnDisableInternal() { }

        private void OnDestroy()
        {
            if (_isInited)
            {
                _viewModel.OnDestroy();
                OnDestroyInternal();
            } 
        }
        protected virtual void OnDestroyInternal() { }
        protected virtual void Subscribe() { }
        protected virtual void Unsubscribe() { }

        public virtual void Close()
        {
            Closed?.Invoke(this);
            Destroy(this.gameObject);
        }

        public void SetParent(Transform parent)
        {
            this.transform.SetParent(parent);
        }
    }
}