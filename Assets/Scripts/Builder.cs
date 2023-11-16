using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public Transform worldCursor;
    public GameObject towerPrefab;

    bool buildOn;

    private void Start()
    {
        worldCursor.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            buildOn = !buildOn;
            worldCursor.gameObject.SetActive(buildOn);
        }

        if (buildOn)
        {
            Build();
        }
    }

    void Build()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit))
        {
            worldCursor.position = hit.point;
            worldCursor.rotation = Quaternion.EulerAngles(hit.normal);

            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(towerPrefab, hit.point, Quaternion.EulerAngles(hit.normal));
            }
        }
    }
}
