using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickStart : MonoBehaviour
{
    public GameObject yourButton;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
		    btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick(){
		//Debug.Log ("You have clicked the button!");
        player.transform.position = new Vector3(0,0,0);
        yourButton.SetActive(false);
	}

}
