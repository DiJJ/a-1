using NaughtyAttributes;
using UnityEngine;

namespace a1.Data
{
    [System.Serializable]
    public struct PlayerData
    {
        [field: SerializeField, Dropdown("Animation")]
        public float AnimationSpeed { get; private set; }
        
        [field: SerializeField, Dropdown("Movement")]
        public float MovementSpeed { get; private set; }
    }
}