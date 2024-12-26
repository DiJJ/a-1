using UnityEngine;

namespace a1.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
    public sealed class PlayerDataSO : ScriptableObject
    {
        [field: SerializeField]
        public PlayerDataSO PlayerData { get; private set; }
    }
}