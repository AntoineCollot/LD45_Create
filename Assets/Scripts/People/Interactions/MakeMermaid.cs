using LD45.Physic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.People.Interations
{
    public class MakeMermaid : MonoBehaviour
    {
        public GameObject[] legs;
        public GameObject mermaidQueue;

        public void MakeItAMermaid()
        {
            foreach (GameObject go in legs)
            {
                go.SetActive(false);
            }

            mermaidQueue.SetActive(true);

            //GetComponent<ApplyGravity>().enabled = false;

            Achievements.MakeMermaid();
        }

        //private void OnCollisionExit2D(Collision2D collision)
        //{
        //    if(collision.collider.tag=="Water")
        //    {
        //        GetComponent<Death>().Die();
        //    }
        //}
    }
}