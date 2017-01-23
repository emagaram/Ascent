using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

    public Transform canvas;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(canvas.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
	}

    void TogglePause()
    {
        if (canvas.gameObject.activeInHierarchy == false)
        {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            canvas.gameObject.SetActive(false);
        }
    }
}
