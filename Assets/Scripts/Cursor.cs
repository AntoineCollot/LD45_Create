using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace LD45
{
    public class Cursor : MonoBehaviour
    {
        Camera m_cam;
        EventSystem eventSystem;
        Queue<Vector2> m_cursorPositions = new Queue<Vector2>();

        public int averageForceOnFrameCount;

        //Events
        public enum ClickType { UI,Sky, World, Character}
        public class ClickEvent : UnityEvent<ClickType,Rigidbody2D> { }
        public static ClickEvent onMouseClick = new ClickEvent();

        public static Cursor Instance;

        public Vector2 CursorPosition
        {
            get
            {
                return m_cam.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        public Vector2 CursorForce
        {
            get
            {
                return (CursorPosition - m_cursorPositions.Peek()) / (Time.smoothDeltaTime * averageForceOnFrameCount);
            }
        }

        // Start is called before the first frame update
        void Awake()
        {
            m_cam = Camera.main;
            eventSystem = FindObjectOfType<EventSystem>();
            Instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            AddCursorPositionToQueue(CursorPosition);

            //On click
            if (Input.GetMouseButtonDown(0))
            {
                ProcessClick();
            }
        }

        void ProcessClick()
        {
            //Get the 2D colliders hit by the cam ray
            Ray camRay = m_cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(camRay);

            //If any ui element hit
            if(eventSystem.IsPointerOverGameObject())
            {
                onMouseClick.Invoke(ClickType.UI, hit.rigidbody);
            }

            //if something with a rigibody is hit
            else if (hit.rigidbody != null)
            {
                onMouseClick.Invoke(ClickType.Character, hit.rigidbody);
            }

            else if(hit.collider != null && hit.collider.tag == "Ground")
            {
                onMouseClick.Invoke(ClickType.World,null);
            }

            else
            {
                onMouseClick.Invoke(ClickType.Sky,null);
            }
        }

        void AddCursorPositionToQueue(Vector2 newCursorPos)
        {
            m_cursorPositions.Enqueue(newCursorPos);
            while(m_cursorPositions.Count> averageForceOnFrameCount)
            {
                m_cursorPositions.Dequeue();
            }
        }
    }
}