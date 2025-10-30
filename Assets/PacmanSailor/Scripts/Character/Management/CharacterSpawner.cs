using PacmanSailor.Scripts.Character.Characters;
using PacmanSailor.Scripts.Data;
using UnityEngine;

namespace PacmanSailor.Scripts.Character.Management
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] private CharactersConfig _characterConfig;

        public ICharacter[] Spawn()
        {
            var spawnPoints = FindObjectsByType<CharacterSpawnPoint>(FindObjectsSortMode.None);
            var characters = new ICharacter[spawnPoints.Length];

            for (var index = 0; index < spawnPoints.Length; index++)
            {
                var spawnPoint = spawnPoints[index];
                var characterData = _characterConfig.GetCharacterData(spawnPoint.CharacterType);
                var character = Spawn(characterData, spawnPoint);
                characters[index] = character;
            }

            return characters;
        }

        private ICharacter Spawn(CharacterData characterData, CharacterSpawnPoint spawnPoint)
        {
            var character = Instantiate(characterData.Prefab, spawnPoint.transform.position, spawnPoint.transform.rotation)
                .GetComponent<ICharacter>();

            character.Initialize(characterData);
            Destroy(spawnPoint.gameObject);
            return character;
        }
    }
}