using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeIn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fade());
    }

   IEnumerator Fade()
    {

        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        float t = 0;
        Color c = text.color;
        float target = c.a;
        c.a = 0;
        text.color = c;
        yield return new WaitForSeconds(1);
        while(t<1)
        {
            t += Time.deltaTime;

            c.a = Mathf.Lerp(0, target,t);
            text.color = c;
            yield return null;
        }
    }
}
