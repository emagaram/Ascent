using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {
    public GameObject controlImage;
    public bool disabledSound;
    public void changedSound()
    {
        disabledSound = !disabledSound;
    }

    public void DisplayControls()
    {
        controlImage.SetActive(true);
    }

    public void HideControls()
    {
        controlImage.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
        Time.timeScale = 1;
    }
}
