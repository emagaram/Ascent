using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public GameObject blackSquare;
    public float speed;
    private bool fadeOut;
    public float time;
    IEnumerator FadeOutAfterSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        fadeOut = true;

    }
    // Use this for initialization
    void Start()
    {
        StartCoroutine(FadeOutAfterSeconds(time));
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOut)
        {
            Color newColor = blackSquare.GetComponent<Image>().color;
            newColor.a = Mathf.Lerp(newColor.a, 1, Time.deltaTime * speed);
            blackSquare.GetComponent<Image>().color = newColor;

            if (blackSquare.GetComponent<Image>().color.a > 0.9)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
            }
        }
    }
}
