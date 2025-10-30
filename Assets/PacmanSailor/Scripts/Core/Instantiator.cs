using UnityEngine;

namespace PacmanSailor.Scripts.Core
{
    public class Instantiator : MonoBehaviour
    {
        public static T InstantiatePrefab<T>(T prefab, Vector3 position = new(), Quaternion rotation = new())
            where T : Object => Instantiate(prefab, position, rotation);

        public static T InstantiatePrefab<T>(T prefab, Transform parent) where T : Object => Instantiate(prefab, parent);
    }
}