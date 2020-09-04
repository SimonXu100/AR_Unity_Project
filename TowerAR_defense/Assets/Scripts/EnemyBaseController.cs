using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyBaseController : MonoBehaviour
{
    private float healthValue = 100f;
	private float harmProjectile = 20f;
	private float timeAfterHit = .1f;
    public Image healthBar;
	//public Text testText;
    // Start is called before the first frame update
    void Awake(){

    }
    // Update is called once per frame
    void Update()
    {	
        healthBar.fillAmount = healthValue / 100.0f;
    	// control health bar
        if(healthValue <=0){
        	Destroy(gameObject, timeAfterHit);
        }
        //testText.text = "heathValue: " + healthValue.ToString();
    }


    // control health bar
    // destory the enemy and projectile
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Projectile")){
        	healthValue = healthValue - harmProjectile;
            Destroy(other.gameObject, timeAfterHit);
        }
    
    }


}


