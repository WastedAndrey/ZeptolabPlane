
using Assets.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class UIManager : ServiceBase
    {
        [SerializeField]
        private Transform _viewsParent;

        private List<IView> _views = new List<IView>();

        public ViewBase<T> CreateView<T>(ViewBase<T> prefabView, T viewModel) where T : ViewModelBase 
        {
            var newView = Instantiate(prefabView, _viewsParent);
            //newView.SetParent(_viewsParent);
            newView.Init(viewModel);
            newView.Closed += OnViewClosed;
            _views.Add(newView);
            return newView;
        }

        public void CloseAllViews()
        {
            for (int i = 0; i < _views.Count; i++)
            {
                _views[i].Close();
            }
        }

        public void CloseLastView()
        {
            if (_views.Count > 0)
                _views[_views.Count - 1].Close();
        }

        private void OnViewClosed(IView view)
        {
            view.Closed -= OnViewClosed;
            
            if(_views.Contains(view)) 
                _views.Remove(view);
        }
    }
}