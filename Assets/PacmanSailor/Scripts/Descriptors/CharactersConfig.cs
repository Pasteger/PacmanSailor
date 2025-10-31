using System.Linq;
using PacmanSailor.Scripts.Data;
using PacmanSailor.Scripts.Enum;
using UnityEngine;

namespace PacmanSailor.Scripts.Descriptors
{
    [CreateAssetMenu(fileName = "Characters Descriptor", menuName = "Descriptors/Characters Descriptor")]
    public class CharactersDescriptor : ScriptableObject
    {
        [SerializeField] private CharacterData[] _charactersData;

        public CharacterData GetCharacterData(CharacterType characterType) =>
            _charactersData.FirstOrDefault(character => character.CharacterType == characterType);
    }
}