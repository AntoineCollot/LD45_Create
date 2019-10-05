using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechSelectedAnim : MonoBehaviour
{
    public float interval;
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {
        int id = 0;
        Image image = GetComponent<Image>();
        while(true)
        {
            image.sprite = sprites[id];
            id++;
            id %= sprites.Length;

            yield return new WaitForSeconds(interval);
        }
    }
}
