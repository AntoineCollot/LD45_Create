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

        public static Drawning Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void HitWater(Human human)
        {

                switch (human.sex)
                {
                    case Human.Sex.Male:
                        Drawn(human);
                        break;
                    case Human.Sex.Female:
                        MakeMermaid(human);
                        break;
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