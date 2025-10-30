using System;
using UniRx;
using Unity.VisualScripting;

namespace PacmanSailor.Scripts.GameCycle
{
    public abstract class AbstractGameCycle : IInitializable, IDisposable
    {
        protected readonly CompositeDisposable Disposable = new();

        public virtual void Initialize()
        {
        }

        public void Dispose() => Disposable.Dispose();
    }
}