using LD45.Physic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.People.Interations
{
    public class CreateKing : MonoBehaviour
    {
        new Collider2D collider;
        public GameObject crownPrefab;

        // Start is called before the first frame update
        void Start()
        {
            collider = GetComponent<Collider2D>();

            GrabThings.onReleaseThing.AddListener(OnThingReleased);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnDisable()
        {
            if(GrabThings.onReleaseThing!=null)
            GrabThings.onReleaseThing.RemoveListener(OnThingReleased);
        }

        void OnThingReleased(Rigidbody2D r)
        {
            if (collider.OverlapPoint(r.position))
            {
                Transform head = r.transform.GetChild(0).Find("Head");
                GameObject crown = Instantiate(crownPrefab,head);
                crown.transform.localPosition = Vector3.zero;
                crown.transform.localRotation = Quaternion.identity;
                r.transform.GetComponent<Language>().SetExcitement(1,true);
                Destroy(transform.parent.gameObject);
            }
        }
    }
}