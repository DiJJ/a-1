using a1.Interfaces;
using UnityEngine;

namespace a1.PlayerCharacter
{
    [DisallowMultipleComponent]
    public sealed class PlayerReference : MonoBehaviour, IPlayer
    {
        [SerializeField] private Transform m_cameraRoot;
        
        public Transform Transform => transform;
        public Transform CameraRoot => m_cameraRoot;
    }
}