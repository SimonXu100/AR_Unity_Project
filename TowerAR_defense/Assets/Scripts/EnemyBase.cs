using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{	
	//public GameObject enemyBase;
	// the frequency creating new Enemy
	public float frequency;
    private GameObject target;
    //public GameObject EnemyPopPoint;
    private GameObject EnemyPopPoint;
	// the health value
	private int healthValue;

	public GameObject enemyPrefab;         //The projectile to be shot

    Transform enemySpawnTransform;         //Location where the projectiles should spawn
    bool canSpawn = true;

    private GameObject parentObject;        // set Plane as the parent gameobject of new enemy base


    void Awake(){
        EnemyPopPoint = transform.Find("EnemyPopPoint").gameObject;
   		enemySpawnTransform = EnemyPopPoint.transform;

        target = GameObject.Find("TowerImageTarget/Plane/TowerBase");

        parentObject = GameObject.Find("TowerImageTarget/Plane");
    }
    
    
    void FixedUpdate(){
        
        if(target == null){
            Debug.Log("do not find the target");
            canSpawn = false;
        }
        else{
            if(canSpawn){
                //StartCoroutine(Loop());
                enemySpawn();
            }
        }
    }

    public void enemySpawn()
    {
        if (!canSpawn){
            return;
        }
        GameObject newEnemy = (GameObject)Instantiate(enemyPrefab, enemySpawnTransform.position, enemySpawnTransform.rotation);
        newEnemy.transform.SetParent(parentObject.transform);

        canSpawn = false;
        Invoke("CoolDown", frequency);
    }

    void CoolDown()
    {
        canSpawn = true;
    }

    /*
    IEnumerator Loop()
    {
        yield return new WaitForSeconds(frequency);
        enemySpawn();
    }
    */


}



