using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableScriptAfter : MonoBehaviour {

    private FadeIn fadeOut;
    public float time;

    void Start()
    {
        fadeOut = GetComponent<FadeIn>();
        StartCoroutine(enableAfter());
    }

    private IEnumerator enableAfter()
    {
        yield return new WaitForSeconds(time);
        fadeOut.enabled = true;
    }
}
