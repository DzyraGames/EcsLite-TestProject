using UnityEngine;

namespace EcsLiteTestProject.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public float MoveSpeed;
    }
}