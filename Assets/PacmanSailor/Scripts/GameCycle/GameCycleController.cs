using System;
using System.Collections.Generic;
using PacmanSailor.Scripts.Character;
using PacmanSailor.Scripts.Character.Service;
using PacmanSailor.Scripts.Core;
using PacmanSailor.Scripts.Level;
using PacmanSailor.Scripts.UI.Service;
using UnityEngine;

namespace PacmanSailor.Scripts.GameCycle
{
    public class GameCycleController : IDisposable
    {
        private readonly List<BaseGameCycle> _gameCycles = new();

        public GameCycleController(UIInstaller uiInstallerPrefab, LevelConstructor levelConstructorPrefab,
            CharactersService characterServicePrefab, Transform canvas)
        {
            var levelConstructor = Instantiator.InstantiatePrefab(levelConstructorPrefab).GetComponent<LevelConstructor>();
            var charactersManager = Instantiator.InstantiatePrefab(characterServicePrefab).GetComponent<CharactersService>();
            var uiInstaller = Instantiator.InstantiatePrefab(uiInstallerPrefab).GetComponent<UIInstaller>();

            charactersManager.Initialize();
            uiInstaller.Install(canvas);

            _gameCycles.Add(new GameStarter(levelConstructor, charactersManager, uiInstaller));
            _gameCycles.Add(new GamePauser(uiInstaller, charactersManager));
            _gameCycles.Add(new GameEnder(uiInstaller, charactersManager));

            foreach (var gameCycle in _gameCycles)
            {
                gameCycle.Initialize();
            }

            uiInstaller.MainMenuModel.Open();
        }

        public void Dispose()
        {
            foreach (var gameCycle in _gameCycles)
            {
                gameCycle.Dispose();
            }
        }
    }
}