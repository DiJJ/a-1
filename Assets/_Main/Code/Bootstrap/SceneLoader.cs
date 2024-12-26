using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace a1.Bootstrap
{
    public sealed class SceneLoader : MonoBehaviour
    {
        [SerializeField] 
        private AssetReference m_sceneReference;
        private AsyncOperationHandle<SceneInstance> m_sceneInstance;
        
        private void Start()
        {
            m_sceneInstance = m_sceneReference.LoadSceneAsync();
        }

        private void OnDestroy()
        {
            Addressables.Release(m_sceneInstance);
        }
    }
}