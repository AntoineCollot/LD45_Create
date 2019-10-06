using LD45.People;
using LD45.People.Interations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.Physic
{
    public class Death : MonoBehaviour
    {
        Rigidbody2D m_rigidbody;
        public float maxSurviveVelocity;
        public GameObject deathEffectPrefab;

        // Start is called before the first frame update
        void Start()
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.relativeVelocity.magnitude > maxSurviveVelocity)
            {
                Die();
            }
        }

        public void Die()
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity, null);
            Destroy(gameObject);

            Achievements.CharacterDied();
        }
    }
}
