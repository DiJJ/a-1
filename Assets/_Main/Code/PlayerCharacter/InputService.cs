using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace a1.PlayerCharacter
{
    public interface IInputService
    {
        Vector2 MoveInput { get; }
        Vector2 LookInput { get; }
        
        event Action<Vector2> OnMove;
        event Action<Vector2> OnLook;
        event Action OnJump;
    }
    
    public sealed class InputService : IInputService, IInitializable, IDisposable
    {
        private InputSystem_Actions _mInputActions;

        private Vector2 m_moveInput;
        private Vector2 m_lookInput;
        private bool m_jumpInput;

        public Vector2 MoveInput => m_moveInput;
        public Vector2 LookInput => m_lookInput;

        public event Action<Vector2> OnMove = delegate { };
        public event Action<Vector2> OnLook = delegate { };
        public event Action OnJump = delegate { };
        
        void IInitializable.Initialize()
        {
            _mInputActions = new InputSystem_Actions();
            _mInputActions.Enable();
            
            SubscribeEvents();
        }
        
        void IDisposable.Dispose()
        {
            _mInputActions.Disable();
            UnsubscribeEvents();
            
            _mInputActions = null;
        }

        private void Move(InputAction.CallbackContext context)
        {
            m_moveInput = context.ReadValue<Vector2>();
            OnMove.Invoke(m_moveInput);
        }

        private void Look(InputAction.CallbackContext context)
        {
            m_lookInput = context.ReadValue<Vector2>();
            OnLook.Invoke(m_lookInput);
        }

        private void Jump(InputAction.CallbackContext context)
        {
            m_jumpInput = context.ReadValueAsButton();
            OnJump.Invoke();
        }
        
        private void SubscribeEvents()
        {
            _mInputActions.Player.Move.performed += Move;
            _mInputActions.Player.Look.performed += Look;
            _mInputActions.Player.Jump.performed += Jump;
        }

        private void UnsubscribeEvents()
        {
            _mInputActions.Player.Move.performed -= Move;
            _mInputActions.Player.Look.performed -= Look;
            _mInputActions.Player.Jump.performed -= Jump;
        }
    }
}
