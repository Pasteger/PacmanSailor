using Unity.Mathematics;
using UnityEngine;

namespace PacmanSailor.Scripts.Character.Behaviour.Modules
{
    public static class DirectionAdjuster
    {
        public static bool AdjustDirection(Vector3 navMeshAgentVelocity, ref Vector2 previousDirection)
        {
            var desiredVelocity = Vector3.Normalize(navMeshAgentVelocity);

            var currentDirection = CalculateDirection(desiredVelocity);

            if (previousDirection == currentDirection) return false;

            previousDirection = currentDirection;
            return true;
        }

        private static Vector2 CalculateDirection(Vector3 desiredVelocity)
        {
            if (math.abs(desiredVelocity.x) > math.abs(desiredVelocity.z))
            {
                if (desiredVelocity.x > 0) return Vector2.right;
                if (desiredVelocity.x < 0) return Vector2.left;
            }
            else if (math.abs(desiredVelocity.z) > math.abs(desiredVelocity.x))
            {
                if (desiredVelocity.z > 0) return Vector2.up;
                if (desiredVelocity.z < 0) return Vector2.down;
            }

            return Vector2.zero;
        }
    }
}