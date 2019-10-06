using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.People
{
    public class Language : MonoBehaviour
    {
        [HideInInspector]
        public string knownWord = "";
        public static string defaultWord = "";

        [SerializeField] Transform _headT;

        [HideInInspector]
        public float excitement;

        public float minInterval;
        float lastSpeakTime;

        private void Start()
        {
            InvokeRepeating("MaySpeakBecauseExcitement", 1, 1);
        }

        public void SetExcitement(float value, bool shout)
        {
            if(shout)
                SpeakIfKnowHow();

            excitement = value;
        }

        void MaySpeakBecauseExcitement()
        {
            if (Random.value < Mathf.Lerp(0.05f, 0.3f, excitement))
                SpeakIfKnowHow();
        }

        public void SpeakIfKnowHow()
        {
            if ((knownWord.Length > 0 || defaultWord.Length>0) && Time.time > lastSpeakTime+minInterval)
                Speak();
        }

        public void Speak()
        {
            lastSpeakTime = Time.time;
            WordsDisplay.Instance.Spawn(knownWord,_headT.position, this);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Language language = collision.collider.GetComponent<Language>();
            if(language!=null && knownWord.Length>0)
            {
                if (language.knownWord.Length == 0)
                    language.knownWord = knownWord;
                else if(Random.value>0.5f)
                {
                    language.knownWord = knownWord;
                }

            }
        }
    }
}