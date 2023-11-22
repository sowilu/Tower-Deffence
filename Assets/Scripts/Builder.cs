using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public LayerMask groundLayer;
    public Transform worldCursor;
    public GameObject towerPrefab;

    public int towerPrice = 10;

    bool buildOn;
    MeshRenderer cursorRenderer;

    private void Start()
    {
        //player can build 3 towers at the start
        MoneyCounter.instance.Money = towerPrice * 3;

        cursorRenderer = worldCursor.GetComponent<MeshRenderer>();

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

        if (MoneyCounter.instance.Money >= towerPrice)
        {
            cursorRenderer.material.color = Color.green;
        }
        else
        {
            cursorRenderer.material.color = Color.red;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit, 10000, groundLayer))
        {
            worldCursor.position = hit.point;
            worldCursor.rotation = Quaternion.EulerAngles(hit.normal);

            if (Input.GetMouseButtonDown(0) && MoneyCounter.instance.Money >= towerPrice)
            {
                Instantiate(towerPrefab, hit.point, Quaternion.EulerAngles(hit.normal));

                MoneyCounter.instance.Money -= towerPrice;
            }
        }
    }
}
