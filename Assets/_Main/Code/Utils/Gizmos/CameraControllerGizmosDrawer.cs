using a1.PlayerCharacter;
using UnityEngine;
using VContainer;

namespace a1.Utils.Gizmos
{
    public sealed class CameraControllerGizmosDrawer : MonoBehaviour
    {
#if UNITY_EDITOR
        private CameraController m_cameraController;
        
        [Inject]
        private void Inject(ICameraController cameraController)
        {
            m_cameraController = cameraController as CameraController;
        }
        
        private void OnDrawGizmosSelected()
        {
            m_cameraController?.DrawGizmos();
        }
#endif
    }
}