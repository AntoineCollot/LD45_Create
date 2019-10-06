using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.People
{
    public class InitiateLanguage : MonoBehaviour
    {
        public float initiateLanguageMinTime = 20;
        public float baseInterval = 3;

        // Start is called before the first frame update
        void Start()
        {
            Invoke("RegisterToNoAchievementQueue", initiateLanguageMinTime);
        }

        void RegisterToNoAchievementQueue()
        {
            Achievements.waitForNoAchievementsQueue.Enqueue(InitiateLanguageLoop);
        }

        void InitiateLanguageLoop()
        {
            StartCoroutine(InitiateLanguageLoopC());
        }

        IEnumerator InitiateLanguageLoopC()
        {
            float interval = baseInterval;

            while(!Achievements.achievementDic["Language"].isDone)
            {
                Language[] speakers = FindObjectsOfType<Language>();
                if(speakers.Length>0)
                    speakers[Random.Range(0,speakers.Length)].Speak();

                yield return new WaitForSeconds(interval);
                interval *= 2;
            }
        }
    }
}