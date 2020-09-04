using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotatingFloorController : MonoBehaviour
{   
    private float speed;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        speed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float current_speed = speed * slider.value / 20;
        transform.Rotate(new Vector3(0,100,0) * Time.deltaTime * current_speed);   
    }
}
