using System.Linq;
using UniRx;
using UnityEngine;

namespace PacmanSailor.Scripts.Items.Management
{
    public class PelletsManager : MonoBehaviour
    {
        [SerializeField] private Pellet[] _pellets;

        private readonly CompositeDisposable _disposable = new();

        public static readonly Subject<Unit> OnAllPelletsCollect = new();
        public static readonly Subject<Unit> OnPelletCollect = new();

        private void Awake()
        {
            foreach (var pellet in _pellets)
            {
                pellet.OnPelletCollect
                    .Subscribe(_ => PelletCollect())
                    .AddTo(_disposable);
            }
        }

        private void PelletCollect()
        {
            OnPelletCollect.OnNext(Unit.Default);
            CheckPelletsCount();
        }

        private void CheckPelletsCount()
        {
            if (_pellets.All(pellet => !pellet.gameObject.activeSelf)) OnAllPelletsCollect.OnNext(Unit.Default);
        }
    }
}