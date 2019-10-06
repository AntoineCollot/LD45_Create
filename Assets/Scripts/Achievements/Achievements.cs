using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace LD45
{
    public static class Achievements
    {
        public class AchievementEvent : UnityEvent<Achievement> { }
        public static AchievementEvent OnAchievementValidated = new AchievementEvent();
        public static UnityEvent onFirstWomanCreated = new UnityEvent();
        public static UnityEvent onRacesCreated = new UnityEvent();
        public static float lastAchievementTime;
        public static Queue<Action> waitForNoAchievementsQueue = new Queue<Action>();

        public static Dictionary<string, Achievement> achievementDic = new Dictionary<string, Achievement>
         {
        {"World",new Achievement("the World","Wow, just like that ?!", true) },
        {"Death",new Achievement("Death","Was this inevitable ?", false) },
        {"Life",new Achievement("Life","Life always finds a way.", true) },
        {"Women",new Achievement("Women","I feel like things are going to be way more complicated now.", true) },
        {"Language",new Achievement("Language","Is that really a good thing ?",true) },
        {"Drowning",new Achievement("Drowning","You are a terrible person.",false) },
        {"Mermaid",new Achievement("Mermaids","That was essential...",true) },
        {"King",new Achievement("Kings","Long live the king !",true) },
        {"France",new Achievement("France","Baguette du fromage ?",true) },
        {"Sex",new Achievement("Sex","Now that's pretty good !",true) },
        {"Aliens",new Achievement("Aliens","Some people are going to freak out about this...",true) },
        {"Race",new Achievement("Races","Uhh oh...",true) },
        {"Racism",new Achievement("Racims","For the record I do not support this.",false) },
        {"overpopulation ",new Achievement("overpopulation ","Stop clicking everywhere like mad man !",false) }
        };

        public static int achievementValidatedCount = 0;

        static Achievements()
        {
#pragma warning disable CS4014 // Dans la mesure où cet appel n'est pas attendu, l'exécution de la méthode actuelle continue avant la fin de l'appel
            LookForNoAchievementLoop();
#pragma warning restore CS4014 // Dans la mesure où cet appel n'est pas attendu, l'exécution de la méthode actuelle continue avant la fin de l'appel
        }

        public static void CharacterDied()
        {
            ValidateAchivement("Death");
        }

        public static void CharacterDrawned()
        {
            ValidateAchivement("Drowning");
        }

        public static void EditLanguage()
        {
            ValidateAchivement("Language");
        }

        public static void MakeMermaid()
        {
            ValidateAchivement("Mermaid");
        }

        public static void KingDied()
        {
            ValidateAchivement("France");
        }

        public static void KingCreated()
        {
            ValidateAchivement("King");
        }

        public static void WorldCreated()
        {
            ValidateAchivement("World");
        }

        public static void AlienCreated()
        {
            ValidateAchivement("Aliens");
        }

        public static void Sex()
        {
            ValidateAchivement("Sex");
        }

        public static void RaceCreated()
        {
            ValidateAchivement("Race");
        }

        public static void Racism()
        {
            ValidateAchivement("Racism");
        }

        public static void OverPopulation()
        {
            ValidateAchivement("Overpopulation");
        }

        public static void CharacterSpawned(People.Human.Sex sex,People.Human.Race race)
        {
            ValidateAchivement("Life");

            if (sex == People.Human.Sex.Female)
            {
                if (ValidateAchivement("Women"))
                    onFirstWomanCreated.Invoke();
            }


            else if (race == People.Human.Race.White)
            {
                if (ValidateAchivement("Race"))
                    onRacesCreated.Invoke();
            }
        }

        static bool ValidateAchivement(string achievement)
        {
            if (achievementDic.ContainsKey(achievement))
            {
                Achievement currentAchievement = achievementDic[achievement];
                if (!currentAchievement.isDone)
                {
                    currentAchievement.isDone = true;
                    currentAchievement.createdDay = achievementValidatedCount + 1;
                    achievementValidatedCount++;
                    OnAchievementValidated.Invoke(currentAchievement);
                    lastAchievementTime = Time.time;
                    return true;
                }
            }

            return false;
        }

        public static async Task LookForNoAchievementLoop()
        {
            while(true)
            {
                if(Time.time > lastAchievementTime + 10 && waitForNoAchievementsQueue.Count>0)
                {
                    waitForNoAchievementsQueue.Dequeue()();
                }

                await Task.Delay(5000);
            }
        }

        public class Achievement
        {
            public Achievement(string name, string comment, bool isGood)
            {
                this.name = name;
                this.comment = comment;
                this.isGood = isGood;
                isDone = false;
                createdDay = 0;
            }

            public string name;
            public string comment;
            public bool isGood;
            public bool isDone;
            public int createdDay;
        }
    }
}