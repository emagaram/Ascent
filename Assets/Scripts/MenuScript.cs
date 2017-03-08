using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
    public Button startText;
    public Button exitText;
	// Use this for initialization
	void Start () {
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        if (FindObjectOfType<GlobalControl>() != null)
        {
            Destroy(FindObjectOfType<GlobalControl>().gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
        }
        if (FindObjectOfType<GameMenu>() != null)
        {
            Destroy(FindObjectOfType<GameMenu>().gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
        }
	}

    public void ExitPress()
    {
        startText.interactable = false;
        exitText.interactable = false;
    }

    public void NoPress()
    {
        startText.interactable = true;
        exitText.interactable = true;
    }
    public void StartLevel()
    {
        StartCoroutine(startLev());
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator startLev()
    {
        yield return new WaitForSeconds(0.8f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Everest");
    }
}
