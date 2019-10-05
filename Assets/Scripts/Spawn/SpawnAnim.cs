using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnim : MonoBehaviour
{
    public float growTime;
    public float maxGrow;
    public float rotateTime;
    public float maxRotate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Rotate());
        StartCoroutine(Grow());
    }

    IEnumerator Rotate()
    {
        Vector3 originalRot = transform.localEulerAngles;
        float rot = Random.Range(-maxRotate, maxRotate);
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / rotateTime;
            transform.localEulerAngles = originalRot + Vector3.forward * Curves.SinEaseOut(rot, 0, t);

            yield return null;
        }
    }

    IEnumerator Grow()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / growTime*2;
            transform.localScale = Vector3.one * Curves.QuadEaseOut(0, maxGrow, t);

            yield return null;
        }
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / growTime*2;
            transform.localScale = Vector3.one * Curves.Berp(maxGrow, 1, t);

            yield return null;
        }
    }
}
