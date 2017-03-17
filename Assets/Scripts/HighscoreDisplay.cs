using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class HighscoreDisplay : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetInt("beatGame") == 1)
        {
            GetComponent<Text>().text = string.Format("COMPLETED ASCENT IN: {0} DEATHS", PlayerPrefs.GetInt("deaths"));
        }
        else
        {
            GetComponent<Text>().text = "";
        }
    }
}
