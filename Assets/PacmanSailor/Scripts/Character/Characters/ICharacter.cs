using PacmanSailor.Scripts.Core;
using PacmanSailor.Scripts.Data;

namespace PacmanSailor.Scripts.Character.Characters
{
    public interface ICharacter : IPaused
    {
        public void Initialize(CharacterData characterData);
        public void Activate();
        public void Destroy();
    }
}