using UnityEngine;
using VContainer;

namespace a1.PlayerCharacter
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public sealed class KinematicCharacterController : MonoBehaviour
    {
        [SerializeField] private CharacterStats m_characterStats;
        [SerializeField] private float m_outerBounds = 0.05f;
        [SerializeField] private LayerMask m_collisionMask;
        
        private IInputService m_inputService;
        private ICameraController m_cameraController;

        private Rigidbody m_rigidbody;
        private CapsuleCollider m_capsuleCollider;
        private MovementController m_movementController;

        private Vector3 m_inputDirection;
        private bool m_isGrounded;
        
        [Inject]
        private void Inject(IInputService inputService, ICameraController cameraController)
        {
            m_inputService = inputService;
            m_cameraController = cameraController;
            
            SubscribeEvents();
            
            this.LogInjectSuccess();
        }

        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_capsuleCollider = GetComponent<CapsuleCollider>();
            m_movementController = new MovementController(m_capsuleCollider.radius, m_outerBounds, m_collisionMask);
        }

        private void FixedUpdate()
        {
            CheckGround();
            ApplyGravity();
            MoveCharacter();
            RotateCharacter();
        }

        private void LateUpdate()
        {
            UpdateInputDirection();
        }

        private void UpdateInputDirection()
        {
            m_inputDirection = m_cameraController.CameraForward * m_inputService.MoveInput.y +
                               m_cameraController.CameraRight * m_inputService.MoveInput.x;
        }

        private void CheckGround()
        {
            m_isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.01f, Vector3.down, 0.05f,
                m_collisionMask);
        }

        private void ApplyGravity()
        {
            m_movementController.ApplyGravity(Vector3.up * m_characterStats.Gravity, m_isGrounded, Time.fixedDeltaTime);
        }

        private void MoveCharacter()
        {
            var newPos = m_movementController.Move(m_rigidbody.position, m_inputDirection.normalized,
                m_characterStats.MovementSpeed, m_characterStats.AccelerationRate, Time.fixedDeltaTime);
            m_rigidbody.MovePosition(newPos);
        }

        private void RotateCharacter()
        {
            var horizontalVelocity = m_movementController.GetHorizontalVelocity();
            if (horizontalVelocity != Vector3.zero)
                m_rigidbody.MoveRotation(Quaternion.LookRotation(horizontalVelocity));
        }

        private void Jump()
        {
            if (m_isGrounded)
                m_movementController.Jump(m_characterStats.JumpForce);
        }
        
        private void SubscribeEvents()
        {
            m_inputService.OnJump += Jump;
        }
        
        private void UnsubscribeEvents()
        {
            m_inputService.OnJump -= Jump;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}