using System;
using PacmanSailor.Scripts.UI.Model;
using PacmanSailor.Scripts.UI.ViewModel;
using UniRx;
using UnityEngine;

namespace PacmanSailor.Scripts.UI.View
{
    public abstract class AbstractView<TV, TM> : MonoBehaviour, IDisposable
        where TV : AbstractViewModel<TM> where TM : AbstractModel
    {
        protected TV ViewModel;
        protected readonly CompositeDisposable Disposable = new();

        public virtual void Init(TV viewModel)
        {
            ViewModel = viewModel;

            ViewModel.OnOpen
                .Subscribe(_ => Open())
                .AddTo(Disposable);

            ViewModel.OnClose
                .Subscribe(_ => Close())
                .AddTo(Disposable);
            
            Close();
        }

        public virtual void Dispose()
        {
            Disposable.Dispose();
            ViewModel.Dispose();
        }

        protected virtual void OnDestroy() => Dispose();

        protected virtual void Open() => gameObject.SetActive(true);
        protected virtual void Close() => gameObject.SetActive(false);
    }
}