using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.People
{
    public class People : MonoBehaviour
    {
        public static List<People> All;

        // Start is called before the first frame update
        void Start()
        {
            if (All == null)
                All = new List<People>();

            All.Add(this);
        }

        private void OnDestroy()
        {
            if (All != null)
                All.Remove(this);
        }
    }
}