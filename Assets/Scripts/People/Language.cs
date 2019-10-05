using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.People
{
    public class Language : MonoBehaviour
    {
        [HideInInspector]
        public string knownWord = "";

        [SerializeField] Transform _headT;

        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("Speak", 2, 2);
        }

        public void Speak()
        {
            WordsDisplay.Instance.Spawn(knownWord,_headT.position, this);
        }
    }
}