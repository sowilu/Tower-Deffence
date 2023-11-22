using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Transform> waypoints;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }
    }
}
