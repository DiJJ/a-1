using UnityEngine;

namespace a1.PlayerCharacter
{
    [CreateAssetMenu(fileName = "Character", menuName = "Characters/Stat block")]
    public class CharacterStats : ScriptableObject
    {
        [SerializeField] private float m_movementSpeed;
        [SerializeField] private float m_accelerationRate;
        [SerializeField] private float m_jumpHeight;
        [SerializeField] private float m_jumpDistance;

        public float MovementSpeed => m_movementSpeed;
        public float AccelerationRate => m_accelerationRate;
        public float JumpHeight => m_jumpHeight;
        public float JumpDistance => m_jumpDistance;

        public float Gravity => -2.0f * m_jumpHeight * Mathf.Pow(m_movementSpeed, 2) / Mathf.Pow(m_jumpDistance, 2);
        public float JumpForce => 2.0f * m_jumpHeight * m_movementSpeed / m_jumpDistance;
    }
}