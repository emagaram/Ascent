using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {
    public GameObject blackSquare;
    public float speed;
    private bool finishFade = false;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (finishFade == false)
        {
            if (blackSquare != null && blackSquare.GetComponent<Image>().color.a > 0.05)
            {
                Color newColor = blackSquare.GetComponent<Image>().color;
                newColor.a = Mathf.Lerp(newColor.a, 0, Time.deltaTime * speed);
                blackSquare.GetComponent<Image>().color = newColor;
            }
            else if (blackSquare != null)
            {
                finishFade = true;
                Color newColor = blackSquare.GetComponent<Image>().color;
                newColor.a = 0;
                blackSquare.GetComponent<Image>().color = newColor;
            }
        }
    }
}
