using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LD45
{
    public class DisplayAchievementStack : MonoBehaviour
    {
        public Color badColor;
        public Color goodColor;
        public TextMeshProUGUI textPrefab;
        public Transform badStackParent;
        public Transform goodStackParent;

        // Start is called before the first frame update
        void Start()
        {
            Achievements.OnAchievementValidated.AddListener(OnNewAchievement);
        }

        IEnumerator DelayedAdd(Achievements.Achievement achievement)
        {
            yield return new WaitForSeconds(4);
            TextMeshProUGUI newText;
            if (achievement.isGood)
            {
                if (goodStackParent.childCount == 1)
                {
                    goodStackParent.gameObject.SetActive(true);
                    StartCoroutine(LetterByLetter(goodStackParent.GetChild(0).GetComponent<TextMeshProUGUI>()));
                }
                newText = Instantiate(textPrefab, goodStackParent);
                newText.color = goodColor;
            }
            else
            {
                if (badStackParent.childCount == 1)
                {
                    badStackParent.gameObject.SetActive(true);
                    StartCoroutine(LetterByLetter(badStackParent.GetChild(0).GetComponent<TextMeshProUGUI>()));
                }
                newText = Instantiate(textPrefab, badStackParent);
                newText.color = badColor;
            }

            newText.text = achievement.name;
        }

        void OnNewAchievement(Achievements.Achievement achievement)
        {
            StartCoroutine(DelayedAdd(achievement));
        }

        IEnumerator LetterByLetter(TextMeshProUGUI text)
        {
            text.maxVisibleCharacters = 0;
            while (text.maxVisibleCharacters < text.text.Length)
            {
                text.maxVisibleCharacters++;
                yield return null;
            }
        }

    }
}