using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.Physic
{
    public class ApplyGravity : MonoBehaviour
    {
        Rigidbody2D m_rigidbody;
        public float standUpTorque;
        public bool IsGrounded { get; private set; }
        public float gravityMultiplier = 1;

        // Start is called before the first frame update
        void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //gravity
            m_rigidbody.AddForce(Gravity.GetGravityAt(transform.position)* gravityMultiplier, ForceMode2D.Force);

            //Stand up only if not totally upside down
            float angle = Vector3.SignedAngle(Gravity.GetUpAt(transform.position), transform.up, Vector3.forward);
            if(Mathf.Abs(angle)<120)
                m_rigidbody.AddTorque(Mathf.Lerp(0, standUpTorque,Mathf.Abs(angle)/90)* Mathf.Sign(-angle));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.collider.tag == "Ground")
            {
                IsGrounded = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.tag == "Ground")
            {
                IsGrounded = false;
            }
        }
    }
}