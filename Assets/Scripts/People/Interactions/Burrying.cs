using LD45.Physic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.People.Interations
{
    public class Burrying : MonoBehaviour
    {
        public Collider2D[] groundColliders;
        public GameObject burryEffect;

        // Start is called before the first frame update
        void Start()
        {
            GrabThings.onReleaseThing.AddListener(OnThingReleased);
        }

        void OnThingReleased(Rigidbody2D r)
        {
            foreach (Collider2D c in groundColliders)
            {
                if(c.OverlapPoint(r.position))
                {
                    Instantiate(burryEffect, r.position, Quaternion.identity, null);
                    Destroy(r.gameObject);
                    Achievements.CharacterBuried();
                }
            }
        }
    }
}