using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
    public Canvas quitMenu;
    public Button startText;
    public Button exitText;
    public AudioClip playSound;
	// Use this for initialization
	void Start () {
        quitMenu = quitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        if (FindObjectOfType<GlobalControl>() != null)
        {
            Destroy(FindObjectOfType<GlobalControl>().gameObject);
        }
        if (FindObjectOfType<GameMenu>() != null)
        {
            Destroy(FindObjectOfType<GameMenu>().gameObject);
        }
        quitMenu.enabled = false;
	}

    public void ExitPress()
    {
        quitMenu.enabled = true;
        startText.interactable = false;
        exitText.interactable = false;
    }

    public void NoPress()
    {
        quitMenu.enabled = false;
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
