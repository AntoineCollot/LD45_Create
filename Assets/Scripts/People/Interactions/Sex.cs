using LD45.Physic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.People.Interations
{
    public class Sex : MonoBehaviour
    {
        public float intimateDistance = 0.5f;
        public GameObject sexEffect;

        // Start is called before the first frame update
        void Start()
        {
            GrabThings.onReleaseThing.AddListener(OnCharacterReleased);
        }

        void OnCharacterReleased(Rigidbody2D r)
        {
            foreach(Human h in Human.All)
            {
                if(h.gameObject!=r.gameObject && Vector2.Distance(r.position,h.transform.position)< intimateDistance)
                {
                    if(h.sex != r.GetComponent<Human>().sex)
                    {
                        Achievements.Sex();
                        Instantiate(sexEffect, (r.transform.position + h.transform.position) * 0.5f, Quaternion.identity, r.transform);
                    }
                }
            }
        }
    }
}