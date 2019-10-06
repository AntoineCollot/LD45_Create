using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.People
{
    public class Human : MonoBehaviour
    {
        public static List<Human> All;
        public SpriteRenderer head;

        [Header("Sex")]
        public Sprite maleHead;
        public Sprite femaleHead;
        public enum Sex { Male,Female}
        public Sex sex { get; private set; }

        [Header("Races")]
        public Sprite alienHead;
        public Color AlienColor;
        public enum Race { Default, White, Alien}
        public Race race { get; private set; }

        int seed;

        // Start is called before the first frame update
        void Start()
        {
            if (All == null)
                All = new List<Human>();

            All.Add(this);
            if (All.Count > 30)
                Achievements.OverPopulation();

            seed = Random.Range(0, int.MaxValue);

            if (IsDeepWoman() && Achievements.achievementDic["Women"].isDone)
            {
                SetSex(Sex.Female);
            }
            else
            {
                SetSex(Sex.Male);
            }

            if(IsDeepWhite() && Achievements.achievementDic["Race"].isDone && race!=Race.Alien)
            {
                SetRace(Race.White);
            }
            Achievements.onFirstWomanCreated.AddListener(OnFirstWomanCreated);
            Achievements.onRacesCreated.AddListener(OnFirstWhiteCreated);
        }

        private void OnDestroy()
        {
            if (All != null)
                All.Remove(this);
        }

        void OnFirstWomanCreated()
        {
            if(IsDeepWoman())
            {
                SetSex(Sex.Female);
            }
        }
        void OnFirstWhiteCreated()
        {
            if (IsDeepWhite()&&race !=Race.Alien)
            {
                SetRace(Race.White);
            }
        }

        bool IsDeepWoman()
        {
            return seed.ReadBit(1);
        }

        bool IsDeepWhite()
        {
            return !seed.ReadBit(2);
        }

        [ContextMenu("ForceWoman")]
        public void ForceWoman()
        {
            seed.SetBit(1, true);
            SetSex(Sex.Female);
        }


        [ContextMenu("ForceWoman")]
        public void ForceWhite()
        {
            seed.SetBit(2, false);
            SetRace(Race.White);
        }

        void SetSex(Sex sex)
        {
            this.sex = sex;
            switch (sex)
            {
                case Sex.Male:
                    //head.sprite = maleHead;
                    break;
                case Sex.Female:
                    head.sprite = femaleHead;
                    break;
            }
        }

        public void SetRace(Race race)
        {
            this.race = race;
            switch (race)
            {
                case Race.White:
                    SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>(true);
                    foreach (SpriteRenderer r in renderers)
                    {
                        r.color = Color.white;
                    }
                    break;
                case Race.Alien:
                    head.sprite = alienHead;
                    SpriteRenderer[] renderersAlien = GetComponentsInChildren<SpriteRenderer>();
                    foreach(SpriteRenderer r in renderersAlien)
                    {
                        r.color = AlienColor;
                    }
                    GetComponent<Physic.ApplyGravity>().gravityMultiplier = 0.1f;
                    break;
                default:
                    break;
            }
        }
    }
}