using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchCameraController : MonoBehaviour
{   
    public GameObject yourButton;
    public Camera MainCamera;
    public Camera followCamera;
    // Start is called before the first frame update
    
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(SwitchCameraOnClick);
    }

    void SwitchCameraOnClick(){
        if(followCamera.enabled){
            followCamera.enabled = false;
            MainCamera.enabled = true;
        }
        else{
            followCamera.enabled = true;
            MainCamera.enabled = false;
        }

	}

}
