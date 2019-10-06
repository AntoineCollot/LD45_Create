using LD45.Physic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.People.Interations
{
    public class Drawning : MonoBehaviour
    {
        public Collider2D waterCollider;
        public GameObject drawningEffect;

        // Start is called before the first frame update
        void Start()
        {
            GrabThings.onReleaseThing.AddListener(OnThingReleased);
        }

        void OnThingReleased(Rigidbody2D r)
        {
            Human p = r.GetComponent<Human>();
            if(p!=null)
            {
                if(!waterCollider.OverlapPoint(r.position))
                    return;

                switch (p.sex)
                {
                    case Human.Sex.Male:
                        Drawn(p);
                        break;
                    case Human.Sex.Female:
                        MakeMermaid(p);
                        break;
                }
            }
        }

        void Drawn(Human p)
        {
            Instantiate(drawningEffect, p.transform.position, Quaternion.identity, null);
            Destroy(p.gameObject);
            Achievements.CharacterDrawned();
        }

        void MakeMermaid(Human p)
        {
            p.GetComponent<MakeMermaid>().MakeItAMermaid();
        }
    }
}