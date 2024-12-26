using UnityEngine;

namespace a1.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CameraData", menuName = "Data/CameraData")]
    public sealed class CameraDataSO : ScriptableObject
    {
        [field: SerializeField]
        public Data.CameraData CameraData { get; private set; }
    }
}