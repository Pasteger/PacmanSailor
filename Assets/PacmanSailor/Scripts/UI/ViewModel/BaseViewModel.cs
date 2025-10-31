using System;
using PacmanSailor.Scripts.UI.Model;
using UniRx;

namespace PacmanSailor.Scripts.UI.ViewModel
{
    public abstract class BaseViewModel<TM> : IDisposable where TM : BaseModel
    {
        protected readonly TM Model;
        protected readonly CompositeDisposable Disposable = new();

        public readonly Subject<Unit> OnOpen = new();
        public readonly Subject<Unit> OnClose = new();

        public BaseViewModel(TM model)
        {
            Model = model;

            Model.OnOpen
                .Subscribe(_ => Open())
                .AddTo(Disposable);
            Model.OnClose
                .Subscribe(_ => Close())
                .AddTo(Disposable);
        }

        public virtual void Dispose()
        {
            Disposable.Dispose();
            Model.Dispose();
        }

        protected virtual void Open() => OnOpen.OnNext(Unit.Default);
        protected virtual void Close() => OnClose.OnNext(Unit.Default);
    }
}