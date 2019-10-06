using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.God
{
    public class SpawnWorld : MonoBehaviour
    {
        public GameObject world;

        // Start is called before the first frame update
        void Start()
        {
            Cursor.onMouseClick.AddListener(OnClick);

        }

        void OnClick(Cursor.ClickType clickType, Rigidbody2D r)
        {
            world.SetActive(true);
            Invoke("WorldAchievement", .5f);
            Cursor.onMouseClick.RemoveListener(OnClick);
        }

        void WorldAchievement()
        {
            Achievements.WorldCreated();
        }
    }
}