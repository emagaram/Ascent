using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBar : MonoBehaviour
{
    public bool isUp;
    Vector2 currentVec;
    // Use this for initialization
    void Start()
    {
        currentVec = GetComponent<RectTransform>().anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        currentVec = GetComponent<RectTransform>().anchoredPosition;
        if (FindObjectOfType<StartCutScene>().cutSceneHasStarted)
        {

            if (isUp)
            {
                Vector2 desiredPos = new Vector2(currentVec.x, -3f);
                GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(currentVec, desiredPos, Time.deltaTime);
            }
            else
            {
                Vector2 desiredPos = new Vector2(currentVec.x, 3f);
                GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(currentVec, desiredPos, Time.deltaTime);
            }
        }
    }
}
