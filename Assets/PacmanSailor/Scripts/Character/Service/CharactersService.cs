using PacmanSailor.Scripts.Character.Characters;
using PacmanSailor.Scripts.Core;
using UnityEngine;

namespace PacmanSailor.Scripts.Character.Service
{
    public class CharactersService : MonoBehaviour, IPaused
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

        public void Pause(bool isPause)
        {
            foreach (var character in _characters)
            {
                character.Pause(isPause);
            }
        }
    }
}