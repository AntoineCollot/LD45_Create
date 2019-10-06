using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.People.Interations
{
    public class King : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Achievements.KingCreated();
            PointsOfInterest.points.Add(transform);
        }

        private void OnDestroy()
        {
            Achievements.KingDied();
            if(PointsOfInterest.points!=null)
                PointsOfInterest.points.Remove(transform);
        }
    }
}
