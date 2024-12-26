using System;
using UnityEngine;

namespace a1.Data
{
    [Serializable]
    public struct CameraData
    {
        [field: SerializeField, Range(0f, 100f)] 
        public float HorizontalSensitivity { get; set; }
        
        [field: SerializeField, Range(0f, 100f)] 
        public float VerticalSensitivity { get; set; }
        
        [field: SerializeField] 
        public Vector2 ClampVerticalMinMax { get; set; }
    }
}