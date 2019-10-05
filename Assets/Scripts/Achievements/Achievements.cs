using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LD45
{
    public static class Achievements
    {
        public class AchievementEvent : UnityEvent<string> { }
        public static AchievementEvent OnAchievementValidated = new AchievementEvent();

        public static Dictionary<string, bool> achievementDic = new Dictionary<string, bool>
         {
        {"Death",false },
        {"Language",false },
        };

        public static void CharacterDied()
        {
            ValidateAchivement("Death");
        }

        static void ValidateAchivement(string achievement)
        {
            if (achievementDic.ContainsKey(achievement))
            {
                if (!achievementDic[achievement])
                {
                    achievementDic[achievement] = true;
                    OnAchievementValidated.Invoke(achievement);
                }
            }
        }
    }
}