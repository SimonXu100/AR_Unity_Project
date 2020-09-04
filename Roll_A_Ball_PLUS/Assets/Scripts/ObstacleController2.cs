using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObstacleController2 : MonoBehaviour
{   
    public Slider slider;
    private Vector3 scaleChange, positionChange;
    // Start is called before the first frame update
    void Start()
    {
        scaleChange = new Vector3(0.0f, 0.0f, 0.05f);  
        positionChange = new Vector3(0.0f, 0.0f, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += scaleChange * slider.value / 50;
        transform.position += positionChange * slider.value / 50;


        if (transform.localScale.z < 1.0f || transform.localScale.z > 9.0f)
        {
            scaleChange = -scaleChange;
            positionChange = -positionChange;
            //positionChange = -positionChange;
        }
        
    }
}
