using System;
using PacmanSailor.Scripts.Enum;
using UnityEngine;

namespace PacmanSailor.Scripts.Data
{
    [Serializable]
    public class CharacterData
    {
        public CharacterType CharacterType;
        public GameObject Prefab;
        public float Speed;
    }
}
