using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.Physic
{
    public class GrabThings : MonoBehaviour
    {
        Rigidbody2D m_heldRigidbody;
        Camera m_cam;
        [Range(0,1)]
        public float throwMultiplier;

        // Start is called before the first frame update
        void Start()
        {
            m_cam = Camera.main;

            Cursor.onMouseClick.AddListener(OnClick);
        }

        void OnClick(Cursor.ClickType clickType,Rigidbody2D hitR)
        {
            if(clickType == Cursor.ClickType.Character)
            {
                m_heldRigidbody = hitR;
                StartCoroutine(DragRigidbody());
            }
        }

        IEnumerator DragRigidbody()
        {
            Vector2 diff = m_heldRigidbody.transform.position - m_cam.ScreenToWorldPoint(Input.mousePosition);
            
            while(Input.GetMouseButton(0))
            {
                m_heldRigidbody.position = Cursor.Instance.CursorPosition + diff;
                yield return null;
            }

            m_heldRigidbody.velocity = Cursor.Instance.CursorForce * throwMultiplier;
        }
    }
}
