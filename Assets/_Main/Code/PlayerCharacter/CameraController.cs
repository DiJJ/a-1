using a1.Data;
using a1.Interfaces;
using a1.ScriptableObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace a1.PlayerCharacter
{
    public interface ICameraController
    {
        Vector3 CameraForward { get; }
        Vector3 CameraRight { get; }
    }
    
    public sealed class CameraController : ICameraController, IInitializable, ILateTickable
    {
        private Transform m_cameraRoot;
        private Vector2 m_currentRotation;
        private Vector2 m_input;

        private IInputService m_InputService;

        private CameraDataSO m_cameraDataSO;
        private CameraData CameraData => m_cameraDataSO.CameraData;
        
        public Vector3 CameraForward => Vector3.ProjectOnPlane(m_cameraRoot.forward, Vector3.up).normalized;
        public Vector3 CameraRight => Vector3.ProjectOnPlane(m_cameraRoot.right, Vector3.up).normalized;
        
        [Inject]
        private void Inject(IInputService inputService, IPlayer player, CameraDataSO cameraDataSO)
        {
            m_InputService = inputService;
            m_cameraRoot = player.CameraRoot;
            m_cameraDataSO = cameraDataSO;
            
            this.LogInjectSuccess();
        }

        void IInitializable.Initialize()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void ILateTickable.LateTick()
        {
            UpdateCurrentRotation();
            RotateCamera();
        }

        private void UpdateCurrentRotation()
        {
            m_input = m_InputService.LookInput;
            // X and Y swapped to align movement input with corresponding rotation axes.
            m_currentRotation += new Vector2(m_input.y * CameraData.VerticalSensitivity, m_input.x * CameraData.HorizontalSensitivity) *
                                 Time.deltaTime;
            m_currentRotation =
                new Vector2(Mathf.Clamp(m_currentRotation.x, CameraData.ClampVerticalMinMax.x, CameraData.ClampVerticalMinMax.y),
                    m_currentRotation.y);
        }

        private void RotateCamera()
        {
            m_cameraRoot.rotation = Quaternion.Euler(m_currentRotation.x, m_currentRotation.y, 0f);
        }
        
        public void DrawGizmos()
        {
            Gizmos.color = Color.blue;
            var position = m_cameraRoot.position;
            
            Gizmos.DrawLine(position, position + CameraForward);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(position, position + CameraRight);
        }
    }
}
