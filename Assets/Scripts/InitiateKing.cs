using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.People.Interations
{
    public class InitiateKing : MonoBehaviour
    {
        public float registerTime = 30;
        public GameObject kingRock;

        // Start is called before the first frame update
        void Start()
        {
            Invoke("RegisterToNoAchievements", registerTime);
        }

        void RegisterToNoAchievements()
        {
            Achievements.waitForNoAchievementsQueue.Enqueue(SpawnKingRock);
        }

        void SpawnKingRock()
        {
            kingRock.SetActive(true);
        }
    }
}