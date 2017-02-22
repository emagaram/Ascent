using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorPath : MonoBehaviour
{
    public float pathLength = 0;
    public Color rayColor = Color.white;
    public List<Transform> pathObjs = new List<Transform>();
    Transform[] theArray;

    void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        theArray = GetComponentsInChildren<Transform>();
        pathObjs.Clear();
        pathLength = 0;

        foreach (Transform pathObj in theArray)
        {
            if (pathObj != this.transform)
            {
                pathObjs.Add(pathObj);
            }
        }

        for (int i = 0; i < pathObjs.Count; i++)
        {
            Vector3 position = pathObjs[i].position;
            if (i > 0)
            {
                Vector3 previous = pathObjs[i - 1].position;
                Gizmos.DrawLine(previous, position);
                pathLength += Vector3.Distance(previous, position);
                Gizmos.DrawWireSphere(position, 0.2f);
            }
        }
    }
}
