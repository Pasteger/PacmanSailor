using PacmanSailor.Scripts.Character.Characters;
using PacmanSailor.Scripts.Core;
using UnityEngine;

namespace PacmanSailor.Scripts.Character.Management
{
    public class CharactersManager : MonoBehaviour, IPaused
    {
        private CharacterSpawner _characterSpawner;

        private ICharacter[] _characters;

        public void Initialize() => _characterSpawner = GetComponent<CharacterSpawner>();

        public void SpawnCharacters() => _characters = _characterSpawner.Spawn();

        public void DestroyCharacters()
        {
            foreach (var character in _characters)
            {
                character.Destroy();
            }
        }

        public void ActivateCharacters()
        {
            foreach (var character in _characters)
            {
                character.Activate();
            }
        }

        public void Pause()
        {
            foreach (var character in _characters)
            {
                character.Pause();
            }
        }

        public void Resume()
        {
            foreach (var character in _characters)
            {
                character.Resume();
            }
        }
    }
}