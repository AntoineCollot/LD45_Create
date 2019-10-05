using LD45.Physic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.God
{
    public class SpawnCharacters : MonoBehaviour
    {
        [SerializeField] GameObject _characterPrefab;

        // Start is called before the first frame update
        void Start()
        {
            Cursor.onMouseClick.AddListener(OnClick);
        }

        void OnClick(Cursor.ClickType clickType,Rigidbody2D r)
        {
            if(clickType == Cursor.ClickType.Sky)
            {
                Vector2 spawnPos = Cursor.Instance.CursorPosition;
                Instantiate(_characterPrefab, spawnPos, Gravity.GetUpRotationAt(spawnPos), null);
            }
        }
    }
}
