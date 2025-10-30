using System;
using System.Collections.Generic;
using PacmanSailor.Scripts.Character;
using PacmanSailor.Scripts.Character.Management;
using PacmanSailor.Scripts.Core;
using PacmanSailor.Scripts.Level;
using PacmanSailor.Scripts.UI.Management;
using UnityEngine;

namespace PacmanSailor.Scripts.GameCycle
{
    public class GameCycleController : IDisposable
    {
        private readonly List<AbstractGameCycle> _gameCycles = new();

        public GameCycleController(UIInstaller uiInstallerPrefab, LevelConstructor levelConstructorPrefab,
            CharactersManager characterManagerPrefab, Transform canvas)
        {
            var levelConstructor = Instantiator.InstantiatePrefab(levelConstructorPrefab).GetComponent<LevelConstructor>();
            var charactersManager = Instantiator.InstantiatePrefab(characterManagerPrefab).GetComponent<CharactersManager>();
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