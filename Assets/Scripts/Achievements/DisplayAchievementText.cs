using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LD45
{
    public class DisplayAchievementText : MonoBehaviour
    {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI descriptionText;
        public float characterTimeInterval;
        public float alertLifeTime;
        Queue<Achievements.Achievement> achievementToDisplay = new Queue<Achievements.Achievement>();

        // Start is called before the first frame update
        void Start()
        {
            Achievements.OnAchievementValidated.AddListener(OnNewAchievement);

            StartCoroutine(ManageAchivementDisplay());
        }

        void OnNewAchievement(Achievements.Achievement achievement)
        {
            achievementToDisplay.Enqueue(achievement);
        }

        IEnumerator ManageAchivementDisplay()
        {
            while(true)
            {
                if(achievementToDisplay.Count>0)
                {
                    yield return new WaitForSeconds(0.5f);

                    nameText.enabled = true;
                    Achievements.Achievement achievement = achievementToDisplay.Dequeue();

                    nameText.text = "Day "+Achievements.achievementValidatedCount+" : You created " + achievement.name + " !";

                    yield return StartCoroutine(LetterByLetter(nameText));

                    yield return new WaitForSeconds(1);
                    descriptionText.enabled = true;
                    descriptionText.text = achievement.comment;

                    yield return StartCoroutine(LetterByLetter(descriptionText));

                    yield return new WaitForSeconds(alertLifeTime);

                    StartCoroutine(FadeOut(nameText));
                    yield return StartCoroutine(FadeOut(descriptionText));

                    descriptionText.enabled = false;
                    nameText.enabled = false;
                }

                yield return null;
            }
        }

        IEnumerator LetterByLetter(TextMeshProUGUI text)
        {
            text.maxVisibleCharacters = 0;
            while (text.maxVisibleCharacters < text.text.Length)
            {
                text.maxVisibleCharacters++;
                yield return new WaitForSeconds(characterTimeInterval);
            }
        }

        IEnumerator FadeOut(TextMeshProUGUI text)
        {
            float t = 0;
            Color c = text.color;
            while(t<1)
            {
                t += Time.deltaTime * 2;
                c.a = Curves.QuadEaseInOut(1, 0, Mathf.Clamp01(t));
                text.color = c;
                yield return null;
            }
            c.a = 1;
            text.color = c;
        }
    }
}