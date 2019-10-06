using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace LD45.People
{
    public class SpeechBubble : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        bool isHovered;
        bool isSelected;
        [HideInInspector]
        public Language language;
        public float freezeTime;
        public float lifeTime;
        float m_remainingLifeTime;
        [SerializeField] GameObject selectedGraphic;
        Animator anim;
        TextMeshProUGUI text;

        public void OnPointerEnter(PointerEventData eventData)
        {
            isHovered = true;
            anim.SetBool("Hovered", true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isHovered = false;
            anim.SetBool("Hovered", false);
        }

        // Start is called before the first frame update
        void Start()
        {
            Cursor.onMouseClick.AddListener(OnMouseClick);
            anim = GetComponent<Animator>();
            m_remainingLifeTime = lifeTime;
            text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = language.knownWord;
            if(language.knownWord.Length==0)
            {
                GetComponent<SpeechGravity>().enabled = false;
            }
        }

        void OnMouseClick(Cursor.ClickType type, Rigidbody2D r)
        {
            if(type == Cursor.ClickType.UI && isHovered)
            {
                //listen
                isSelected = true;
                StartCoroutine(ListenToKeyboard());
            }
            else if(isSelected)
            {
                StopAllCoroutines();
                selectedGraphic.SetActive(false);
                isSelected = false;
                GetComponent<SpeechGravity>().enabled = true;
                anim.speed = 1;

                if (language.knownWord.Length > 0)
                {
                    Achievements.EditLanguage();
                    if (Language.defaultWord == "")
                        Language.defaultWord = language.knownWord;
                }
            }
        }

        void Update()
        {
            if(!isSelected)
                m_remainingLifeTime -= Time.deltaTime;

            if (m_remainingLifeTime <= 0)
                Destroy(gameObject);
        }

        IEnumerator ListenToKeyboard()
        {
            string inputString = "";
            float endTime = Time.time + freezeTime;
            SpeechGravity gravity = GetComponent<SpeechGravity>();
            anim.speed = 0;
            gravity.enabled = false;
            selectedGraphic.SetActive(true);
            while (isSelected && Time.time<endTime)
            {
                inputString += Input.inputString;
                if (inputString.Length > 0)
                {
                    language.knownWord = inputString;
                    text.text = inputString;
                }
                yield return null;
            }
            selectedGraphic.SetActive(false);
            gravity.enabled = true;
            anim.speed = 1;

            if(inputString.Length>0)
                Achievements.EditLanguage();

            if (Language.defaultWord == "")
                Language.defaultWord = inputString;
        }
    }
}