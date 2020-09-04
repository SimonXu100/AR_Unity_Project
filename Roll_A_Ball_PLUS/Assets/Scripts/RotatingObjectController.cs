using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class RotatingObjectController : MonoBehaviour
{
    //private Vector3 target = new Vector3(5.0f, 0.0f, 0.0f);
    // Start is called before the first frame update
    public GameObject center;
    private Vector3  center_v3;
    private float speed;
    public Slider slider;

    void Start()
    {
        speed = 0.001f;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(new Vector3(1,0,0), 100*Time.deltaTime);
        // transform.LookAt( targetPostition ) ;
        
        // center point

        float temp_y = transform.position.y - center.transform.position.y;
        float temp_z = transform.position.z - center.transform.position.z;
        


        // compute position
        float current_speed = speed * slider.value / 20;
        float angle_step = (float)(current_speed * Math.PI);
        float next_y = (float)(temp_y* Math.Cos(angle_step) - temp_z * Math.Sin(angle_step) + center.transform.position.y);
        float next_z = (float)(temp_y* Math.Sin(angle_step) + temp_z * Math.Cos(angle_step) + center.transform.position.z);

        Vector3 nextPosition = new Vector3(transform.position.x, next_y,next_z);
        transform.position = nextPosition;
    }
}
