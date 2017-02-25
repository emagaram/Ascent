using UnityEngine;
using System.Collections;

public class IcicleFallScript : MonoBehaviour {
    private Rigidbody2D icicle;
    public float waitTime = 0f;
    // Use this for initialization
    void Start () {
        icicle = GetComponent<Rigidbody2D>();
        icicle.isKinematic = true;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            StartCoroutine(DropIcicle());
        }
    }

    IEnumerator DropIcicle()
    {
       yield return new WaitForSeconds(waitTime);
        icicle.isKinematic = false;
    }
}
