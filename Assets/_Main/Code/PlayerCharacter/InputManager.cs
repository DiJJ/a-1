using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private InputSystem_Actions m_inputActions;

    private Vector2 m_moveInput;
    private Vector2 m_lookInput;
    private bool m_jumpInput;
    
    public Vector2 MoveInput => m_moveInput;
    public Vector2 LookInput => m_lookInput;

    public event Action<Vector2> OnMove = delegate { };
    public event Action<Vector2> OnLook = delegate { };
    public event Action OnJump = delegate { };
    
    private void Awake()
    {
        m_inputActions = new InputSystem_Actions();
    }

    private void OnDestroy()
    {
        m_inputActions = null;
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

    private void OnEnable()
    {
        m_inputActions.Enable();
        m_inputActions.Player.Move.performed += Move;
        m_inputActions.Player.Look.performed += Look;
        m_inputActions.Player.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        m_inputActions.Disable();
        m_inputActions.Player.Move.performed -= Move;
        m_inputActions.Player.Look.performed -= Look;
        m_inputActions.Player.Jump.performed -= Jump;
    }
}
