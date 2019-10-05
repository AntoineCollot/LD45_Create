using LD45.Physic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.People
{
    public class WordsDisplay : MonoBehaviour
    {
        [SerializeField] Transform wordPrefab;

        public static WordsDisplay Instance;

        // Start is called before the first frame update
        void Awake()
        {
            Instance = this;
        }

        public void Spawn(string word, Vector3 position, Language language)
        {
            Transform newWord = Instantiate(wordPrefab,position, Gravity.GetUpRotationAt(position), transform);
            newWord.GetComponentInChildren<SpeechBubble>().language = language;
        }
    }
}