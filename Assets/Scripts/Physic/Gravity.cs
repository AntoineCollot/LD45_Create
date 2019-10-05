using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.Physic
{
    public class Gravity : MonoBehaviour
    {
        public static Vector3 centerOfGravity;
        [SerializeField] float _gravityForce;
        public static float GravityForce
        {
            get
            {
                return Instance._gravityForce;
            }
        }
        public static Gravity Instance;

        // Start is called before the first frame update
        void Awake()
        {
            Instance = this;
            centerOfGravity = transform.position;
        }

        public static Vector2 GetGravityAt(Vector3 position)
        {
            return (centerOfGravity - position).normalized * GravityForce;
        }

        public static Vector2 GetUpAt(Vector3 position)
        {
            return (position - centerOfGravity).normalized;
        }

        public static Quaternion GetUpRotationAt(Vector3 position)
        {
            return Quaternion.LookRotation(Vector3.forward, GetUpAt(position));
        }
    }
}