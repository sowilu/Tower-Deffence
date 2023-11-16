using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed = 5;
    public float sensitivity = 2;

    bool rotationOn;

    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.R))
        {
            rotationOn = !rotationOn;
        }

        if(rotationOn)
        {
            Rotate();
        }
    }

    void Move()
    {
        var direction = new Vector3();
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;
    }

    void Rotate()
    {
        var rotation = new Vector2();
        rotation.x = Input.GetAxis("Mouse X");
        rotation.y = Input.GetAxis("Mouse Y");

        var finalRotation = new Vector3(-rotation.y, rotation.x, 0) * sensitivity;

        transform.Rotate(finalRotation);
    }
}
