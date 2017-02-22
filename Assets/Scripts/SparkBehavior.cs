using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SparkBehavior : MonoBehaviour {
    private MoveOnPath pathMovement;
    public SnowballShooter cannon;
	void Start () {
        pathMovement = GetComponent<MoveOnPath>();
        cannon = GetComponentInParent<SnowballShooter>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (pathMovement.currentWaypointID == pathMovement.pathToFollow.pathObjs.Count)
        {
            pathMovement.currentWaypointID = 0;
            transform.position = pathMovement.pathToFollow.pathObjs.First().position;
            FireCannon();
        }
    }
    void FireCannon()
    {
        cannon.Fire();
    }
}
