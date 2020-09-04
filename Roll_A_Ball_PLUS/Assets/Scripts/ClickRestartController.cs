using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClickRestartController : MonoBehaviour
{   
    public GameObject yourButton;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {   
        yourButton.SetActive(false);
        Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void TaskOnClick(){
		//Debug.Log ("You have clicked the button!");
        player.transform.position = new Vector3(0,0,0);
        yourButton.SetActive(false);
	}

}
