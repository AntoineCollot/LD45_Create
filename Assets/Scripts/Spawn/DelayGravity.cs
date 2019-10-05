using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.Physic
{
    public class DelayGravity : MonoBehaviour
    {
        [SerializeField] float delay;

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<ApplyGravity>().enabled = false;
            Invoke("ActivateGravity", delay);
        }

        void ActivateGravity()
        {
            GetComponent<ApplyGravity>().enabled = true;
        }
    }
}