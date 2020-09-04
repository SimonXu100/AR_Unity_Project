using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileController : MonoBehaviour
{
	// main logic
	// control the timing of destorying itself
    // Start is called before the first frame update
    
	public GameObject target;

	void update(){
		if(target == null){
        	Destroy(gameObject);
		}
		if(transform.position.y <= -5){
			Destroy(gameObject);
		}
	}
	void OnTriggerEnter(Collider other){
        
        if(other.gameObject.CompareTag("Plane")){
            Destroy(gameObject);
        } 
    }
}
