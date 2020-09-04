using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowerBaseController : MonoBehaviour
{	
	private float healthValue = 2000f;
	private float harmProjectile = 20f;
	private float harmEnemy = 30f;
	private float timeAfterHit = .1f;

    public Image healthBar;

	void Awake(){

    }
    // Update is called once per frame
    void Update()
    {	
        healthBar.fillAmount = healthValue / 2000.0f;
    	// control health bar
        if(healthValue <=0){
        	Destroy(gameObject, timeAfterHit);
        }
        //testText.text = "heathValue: " + healthValue.ToString();
    }

    // control health bar
    // destory the enemy and projectile
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Enemy")){
        	healthValue = healthValue - harmEnemy;
            Destroy(other.gameObject, timeAfterHit);
        }
        else if(other.gameObject.CompareTag("Projectile")){
        	healthValue = healthValue - harmProjectile;
        	Destroy(other.gameObject, timeAfterHit);
        }
    }
}


