using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LD45
{
    public class DisplayAchievementText : MonoBehaviour
    {
        public TextMeshProUGUI text;
        public float characterTimeInterval;
        public float alertLifeTime;
        Queue<string> achievementToDisplay = new Queue<string>();

        // Start is called before the first frame update
        void Start()
        {
            Achievements.OnAchievementValidated.AddListener(OnNewAchievement);

            StartCoroutine(ManageAchivementDisplay());
        }

        void OnNewAchievement(string achievement)
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

                    text.enabled = true;
                    string achievement = achievementToDisplay.Dequeue();

                    text.text = "You created " + achievement + " !";

                    yield return StartCoroutine(LetterByLetter());

                    yield return new WaitForSeconds(alertLifeTime);
                    
                    yield return StartCoroutine(FadeOut());

                    text.enabled = false;
                }

                yield return null;
            }
        }

        IEnumerator LetterByLetter()
        {
            text.maxVisibleCharacters = 0;
            while (text.maxVisibleCharacters < text.text.Length)
            {
                text.maxVisibleCharacters++;
                yield return new WaitForSeconds(characterTimeInterval);
            }
        }

        IEnumerator FadeOut()
        {
            float t = 0;
            Color c = text.color;
            while(t<1)
            {
                t += Time.deltaTime;
                c.a = Curves.QuadEaseInOut(1, 0, Mathf.Clamp01(t));
                text.color = c;
                yield return null;
            }
            c.a = 1;
            text.color = c;
        }
    }
}