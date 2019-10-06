using LD45.Physic;
using LD45.People;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.God
{
    public class SpawnCharacters : MonoBehaviour
    {
        [SerializeField] GameObject _characterPrefab;
        [SerializeField] GameObject _starEffect;
        public int firstWomanAt = 10;
        public int firstWhiteAt = 20;
        int characterSpawnedCount = 0;

        // Start is called before the first frame update
        void Start()
        {
            Cursor.onMouseClick.AddListener(OnClick);
        }

        void OnClick(Cursor.ClickType clickType,Rigidbody2D r)
        {
            if (!Achievements.achievementDic["World"].isDone)
                return;

            if(clickType == Cursor.ClickType.Sky)
            {
                Vector2 spawnPos = Cursor.Instance.CursorPosition;
                GameObject newCharacter = Instantiate(_characterPrefab, spawnPos, Gravity.GetUpRotationAt(spawnPos), null);
                Instantiate(_starEffect, spawnPos,Quaternion.identity, null);
                characterSpawnedCount++;
                Human h = newCharacter.GetComponent<Human>();

                if (characterSpawnedCount >= firstWomanAt && !Achievements.achievementDic["Women"].isDone)
                   h.ForceWoman();

                else if(characterSpawnedCount >= firstWhiteAt && !Achievements.achievementDic["Race"].isDone)
                    h.ForceWhite();

                else if(Gravity.DistanceFromCenter(spawnPos)>9)
                {
                    Achievements.AlienCreated();
                    h.SetRace(Human.Race.Alien);
                }

                Achievements.CharacterSpawned(h.sex,h.race);
            }
        }
    }
}
