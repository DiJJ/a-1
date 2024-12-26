using Unity.Cinemachine;
using UnityEngine;

namespace a1.PlayerCharacter
{
    public class MovementController
    {
        private readonly float m_collisionRadius;
        private readonly float m_outerBounds;
        private readonly int m_depthLimit;
        private readonly LayerMask m_collisionMask;

        public MovementController(float collisionRadius, float outerBounds, LayerMask collisionMask,
            float slopeLimit = 40f, int depthLimit = 3)
        {
            m_collisionRadius = collisionRadius;
            m_outerBounds = outerBounds;
            m_collisionMask = collisionMask;
            m_depthLimit = depthLimit;
        }

        private Vector3 m_horizontalVelocity;
        private Vector3 m_verticalVelocity;

        public Vector3 GetHorizontalVelocity() => m_horizontalVelocity;
        public Vector3 GetVerticalVelocity() => m_verticalVelocity;

        private Vector3 CollideAndSlide(Vector3 position, Vector3 velocity, int depth)
        {
            if (depth > m_depthLimit)
                return Vector3.zero;

            // CapsuleCast requires two points: centers of top and bottom sphere that form capsule
            // TODO: use dynamic values for these calculations
            Vector3 upperPoint = position + Vector3.up * 1.5f;
            Vector3 lowerPoint = position + Vector3.up * 0.3f;
            float distance = velocity.magnitude + m_outerBounds;

            if (Physics.CapsuleCast(upperPoint, lowerPoint, m_collisionRadius, velocity.normalized, out RaycastHit hit,
                    distance, m_collisionMask))
            {
                Vector3 snapToSurface = velocity.normalized * (hit.distance - m_outerBounds);
                Vector3 leftOver = velocity - snapToSurface;

                if (snapToSurface.magnitude <= m_outerBounds)
                    snapToSurface = Vector3.zero;

                leftOver = leftOver.ProjectOntoPlane(hit.normal).normalized * leftOver.magnitude;

                return snapToSurface + CollideAndSlide(position + snapToSurface, leftOver, depth + 1);
            }

            return velocity;
        }

        public Vector3 Move(Vector3 position, Vector3 direction, float speed, float acceleration, float delta)
        {
            m_horizontalVelocity = Vector3.Lerp(m_horizontalVelocity, direction * speed, acceleration * delta);
            return position + CollideAndSlide(position, (m_horizontalVelocity + m_verticalVelocity) * delta, 1);
        }

        public void ApplyGravity(Vector3 gravity, bool isGrounded, float delta)
        {
            if (!isGrounded)
                m_verticalVelocity += gravity * delta;
            else if (m_verticalVelocity.y < 0f)
                m_verticalVelocity = Vector3.zero;
        }

        public void Jump(float force)
        {
            m_verticalVelocity = Vector3.up * force;
        }
    }
}
