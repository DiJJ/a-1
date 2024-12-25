using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private InputManager m_inputManager;
    [SerializeField, Range(0f, 100f)]
    private float m_horizontalSensitivity = 60f;
    [SerializeField, Range(0f, 100f)]
    private float m_verticalSensitivity = 40f;
    [SerializeField]
    private Vector2 m_clampVerticalMinMax;

    private Vector2 m_currentRotation;
    private Vector2 m_input;
    
    public Vector3 CameraForward => Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
    public Vector3 CameraRight => Vector3.ProjectOnPlane(transform.right, Vector3.up).normalized;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        UpdateCurrentRotation();
        RotateCamera();
    }
    
    private void UpdateCurrentRotation()
    {
        m_input = m_inputManager.LookInput;
        // X and Y swapped to align movement input with corresponding rotation axes.
        m_currentRotation += new Vector2(m_input.y * m_verticalSensitivity, m_input.x * m_horizontalSensitivity) * Time.deltaTime;
        m_currentRotation = new Vector2(Mathf.Clamp(m_currentRotation.x, m_clampVerticalMinMax.x, m_clampVerticalMinMax.y), m_currentRotation.y);
    }
    
    private void RotateCamera()
    {
        transform.rotation = Quaternion.Euler(m_currentRotation.x, m_currentRotation.y, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + CameraForward);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + CameraRight);
    }
}
