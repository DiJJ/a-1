using UnityEngine;
using VInspector;

namespace a1.PlayerCharacter
{
    public class CharacterAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator m_animator;

        [SerializeField] private float m_movementSpeed = 1f;

        private int m_movementSpeedHash = Animator.StringToHash("MovementSpeed");

        [Button]
        public void SetSpeed()
        {
            m_animator.SetFloat(m_movementSpeedHash, m_movementSpeed);
        }
    }
}