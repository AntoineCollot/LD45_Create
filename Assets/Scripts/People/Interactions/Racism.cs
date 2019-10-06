using LD45.Physic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.People.Interations
{
    public class Racism : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GrabThings.onReleaseThing.AddListener(CheckIfRacism);
        }

        void CheckIfRacism(Rigidbody2D r)
        {
            if (Human.All.Count < 6 || Human.All.Count>40 || Achievements.achievementDic["Racism"].isDone)
                return;

            float differentRacesMinAngle = Mathf.Infinity;
            float sameRaceMaxAngle = 0;
            foreach(Human h1 in Human.All)
            {
                foreach (Human h2 in Human.All)
                {
                    float angle = Gravity.Angle(h1.transform.position, h2.transform.position);
                    if(h1.race!=h2.race)
                    {
                        if(angle<differentRacesMinAngle)
                            differentRacesMinAngle = angle;
                    }
                    else
                    {
                        if (angle > sameRaceMaxAngle)
                            sameRaceMaxAngle = angle;
                    }
                }
            }

            if (differentRacesMinAngle <360 && differentRacesMinAngle*2 > sameRaceMaxAngle)
            {
                Achievements.Racism();
            }
         
        }
    }
}
