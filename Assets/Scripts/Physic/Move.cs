using LD45.People.Interations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.Physic
{
    public class Move : MonoBehaviour
    {
        Rigidbody2D m_rigidbody;
        ApplyGravity gravityComponent;

        [Range(0,90)]
        [SerializeField] float _jumpAngle;
        [SerializeField] float _jumpForce;
        [SerializeField] float _interval;

        AvoidWater avoidWater;

        // Start is called before the first frame update
        void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
            gravityComponent = GetComponent<ApplyGravity>();
            avoidWater = GetComponent<AvoidWater>();
        }

        private void Start()
        {
            InvokeRepeating("Jump", _interval, _interval);
        }

        bool targetIsOnItsRight(Vector2 target)
        {
            Vector2 diff = target - m_rigidbody.position;
            return Vector2.Angle(transform.right, diff) < 90;
        }

        void Jump()
        {
            if (!gravityComponent.IsGrounded)
                return;
            Vector2 jumpDir;
            bool moveToTarget;
            Vector2 target = PointsOfInterest.ClosestPointOfInterest(m_rigidbody.position, out moveToTarget);
            if(moveToTarget)
            {
                bool targetIsOnRight = targetIsOnItsRight(target);

                if (avoidWater != null && avoidWater.enabled)
                {
                    bool waterNear;
                    if (targetIsOnRight)
                        waterNear = avoidWater.IsWaterNear(1);
                    else
                        waterNear = avoidWater.IsWaterNear(-1);

                    if (waterNear)
                        return;
                }

                if (targetIsOnRight)
                    jumpDir = Gravity.GetUpAt(transform.position).Rotate(-_jumpAngle);
                else
                    jumpDir = Gravity.GetUpAt(transform.position).Rotate(_jumpAngle);

            }
            else
            {
                jumpDir = Gravity.GetUpAt(transform.position);
            }

            m_rigidbody.AddForce(jumpDir * _jumpForce, ForceMode2D.Impulse);
        }
    }
}