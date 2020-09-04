using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SmallRotateController : MonoBehaviour
{
    private Transform center;
    private Vector3  center_v3;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.002f;
        center = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        //center point

        float temp_x = transform.position.x - center.position.x;
        float temp_z = transform.position.z - center.position.z;

        // compute position
        float angle_step = (float)(speed * Math.PI);
        float next_x = (float)(temp_x* Math.Cos(angle_step) - temp_z * Math.Sin(angle_step) + center.position.x);
        float next_z = (float)(temp_x* Math.Sin(angle_step) + temp_z * Math.Cos(angle_step) + center.position.z);

        Vector3 nextPosition = new Vector3(next_x,transform.position.y, next_z);
        transform.position = nextPosition;
    }
}
