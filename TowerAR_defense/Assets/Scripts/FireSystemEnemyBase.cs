using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class FireSystemEnemyBase : MonoBehaviour
{
    public float maxProjectileForce = 30f;   //Maximum force of a projectile
    //public float cooldown = 1f;
    public float frequency = 5.0f;


    public GameObject projectilePrefab;         //The projectile to be shot

    public GameObject firePoint;

    Transform projectileSpawnTransform;         //Location where the projectiles should spawn
    bool canShoot = true;

    private GameObject target; 

    void Awake(){
        // firePoint = GameObject.Find("turret_4/Head");
    	projectileSpawnTransform = firePoint.transform;
        //projectileSpawnTransform  = firePoint.transform;;
        target = GameObject.Find("TowerImageTarget/Plane/TowerBase");

    	StartCoroutine(Loop());
    }
    public void enemyFireProjectile()
    {
        if (!canShoot)
            return;

        GameObject go = (GameObject)Instantiate(projectilePrefab, projectileSpawnTransform.position, projectileSpawnTransform.rotation);
        Vector3 force = projectileSpawnTransform.transform.forward * maxProjectileForce;
        go.GetComponent<Rigidbody>().AddForce(force);

        //canShoot = false;
        //Invoke("CoolDown", cooldown);
    }

    IEnumerator Loop()
    {
        while (true)
        {   
            yield return new WaitForSeconds(frequency);
            if(target != null){
                transform.LookAt(target.transform);  
                enemyFireProjectile();
            }
        }
    }

    void CoolDown()
    {
        canShoot = true;
    }
}


