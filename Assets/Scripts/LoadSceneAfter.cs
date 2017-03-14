using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneAfter : MonoBehaviour {
    public string sceneName;
    public float seconds;
	// Use this for initialization
	void Start () {
        StartCoroutine(loadAfter());
	}

    private IEnumerator loadAfter()
    {
        yield return new WaitForSeconds(seconds);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
