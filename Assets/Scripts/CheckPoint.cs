using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour
{
    public LevelManager levelManager;
    public Transform startMarker;
    public Transform endMarker;
    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;
    private Transform banner;
    bool isUp = false;

    // Use this for initialization
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        banner = GetComponentInChildren<Banner>().transform;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        levelManager = FindObjectOfType<LevelManager>();
    }

    bool didHitCheckpointInTime(float timeInSeconds)
    {
        if(GlobalControl.Instance.timeAtRestart!=0 && Mathf.Abs(Time.time - GlobalControl.Instance.timeAtRestart) < timeInSeconds){
            return true;
        }
        else
        {
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        if (GlobalControl.Instance.checkPoint != null && GlobalControl.Instance.checkPoint.transform.position == transform.position && didHitCheckpointInTime(1))
        {
            banner.position = Vector3.Lerp(startMarker.position, endMarker.position, 1);
            isUp = true;
        }
        else if (levelManager.currentCheckpoint == gameObject)
        {
            if (isUp != true)
            {
                startTime = Time.time;
                distCovered = (Time.time - startTime) * speed;
                fracJourney = distCovered / journeyLength;
                isUp = true;
            }
            banner.position = new Vector3(banner.position.x, banner.position.y, banner.position.z);
            banner.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
        }
        else
        {
            if (isUp == true)
            {
                startTime = Time.time;
                distCovered = (Time.time - startTime) * speed;
                fracJourney = distCovered / journeyLength;
                isUp = false;
            }
            banner.position = new Vector3(banner.position.x, banner.position.y, banner.position.z);
            banner.position = Vector3.Lerp(endMarker.position, startMarker.position, fracJourney);

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            levelManager.currentCheckpoint = gameObject;
            GlobalControl.Instance.checkPoint = gameObject;
        }
    }
}
