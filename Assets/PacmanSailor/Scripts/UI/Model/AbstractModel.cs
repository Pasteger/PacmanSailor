using System;
using UniRx;

namespace PacmanSailor.Scripts.UI.Model
{
    public abstract class AbstractModel : IDisposable
    {
        public readonly Subject<Unit> OnOpen = new();
        public readonly Subject<Unit> OnClose = new();

        protected readonly CompositeDisposable Disposable = new();

        public virtual void Dispose() => Disposable.Dispose();

        public virtual void Open() => OnOpen.OnNext(Unit.Default);
        public virtual void Close() => OnClose.OnNext(Unit.Default);
    }
}