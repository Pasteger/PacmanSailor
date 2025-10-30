using System.Linq;
using PacmanSailor.Scripts.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "Characters Config", menuName = "Configs/Characters Config")]
public class CharactersConfig : ScriptableObject
{
    [SerializeField] private CharacterData[] _charactersData;

    public CharacterData GetCharacterData(CharacterType characterType) =>
        _charactersData.FirstOrDefault(character => character.CharacterType == characterType);
}