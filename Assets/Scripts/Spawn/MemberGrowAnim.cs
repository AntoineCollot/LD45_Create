using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberGrowAnim : MonoBehaviour
{
    public float growTime;
    public float minDelay;
    public float maxDelay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Grow());
    }

    IEnumerator Grow()
    {
        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

        float t = 0;
        while(t<1)
        {
            t += Time.deltaTime / growTime;
            transform.localScale = Vector3.one * Curves.Berp(0, 1, t);

            yield return null;
        }
    }
}
