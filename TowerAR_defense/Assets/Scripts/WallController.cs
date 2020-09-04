using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WallController : MonoBehaviour
{
	private float healthValue = 200.0f;
	private float harmProjectile = 20f;
	private float harmEnemy = 30f;
	private float timeAfterHit = .1f;

    public Image healthBar;

    //public Text testText;


	//public Text testText;
    // Start is called before the first frame update
    void Awake(){

    }

    // Update is called once per frame
    void Update()
    {	
        healthBar.fillAmount = healthValue / 200.0f;
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
    /*
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.CompareTag("Projectile") )
        {
            healthValue = healthValue - harmProjectile;
            Destroy(collision.gameObject, timeAfterHit);
        }
    }*/



}
