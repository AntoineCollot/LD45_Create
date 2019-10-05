using LD45.Physic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.People
{
    public class SpeechGravity : MonoBehaviour
    {
        public float maxGravityMult;
        public float accelerationSpeed;
        float m_lifeTime = 0;

        // Update is called once per frame
        void Update()
        {
            m_lifeTime += Time.deltaTime * accelerationSpeed;
            transform.Translate(Gravity.GetGravityAt(transform.position) * Curves.QuadEaseIn(0,maxGravityMult,Mathf.Clamp01(m_lifeTime)) * Time.deltaTime, Space.World);
        }
    }
}