using UnityEngine;
using VContainer;

namespace a1.PlayerCharacter
{
    public class CharacterAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator m_animator;

        [SerializeField] private float m_movementSpeed = 1f;

        private int m_MotionSpeedHash = Animator.StringToHash("MotionSpeed");
        
        [Inject]
        private void Inject(IInputService inputService)
        {
            inputService.OnMove += UpdateMoveAnimation;
        }
        
        private void UpdateMoveAnimation(Vector2 moveInput)
        {
            m_animator.SetFloat(m_MotionSpeedHash, moveInput.magnitude * 5);
        }
    }
}