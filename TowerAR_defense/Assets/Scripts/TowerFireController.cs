using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFireController : MonoBehaviour
{
    public float maxProjectileForce = 300f;   //Maximum force of a projectile
    public float frequency = 2.0f;

    public GameObject projectilePrefab;         //The projectile to be shot

    private GameObject firePoint;

    Transform projectileSpawnTransform;         //Location where the projectiles should spawn
    bool canShoot = true;

    
    void Awake()
    {
        //Get a reference to the projectile spawn point. By providing the path to the object like this, we are making an 
        //inefficient method call more efficient
        //projectileSpawnTransform = GameObject.Find("Head/Cannon_1").transform;
        firePoint = GameObject.Find("Head/Cannon_1/TowerFirePoint");
        projectileSpawnTransform = firePoint.transform;
        StartCoroutine(Loop());

    }

    public void FireProjectile()
    {
        if (!canShoot)
            return;

        GameObject go = (GameObject)Instantiate(projectilePrefab, projectileSpawnTransform.position, projectileSpawnTransform.rotation);
        Vector3 force = projectileSpawnTransform.transform.forward * maxProjectileForce;
        go.GetComponent<Rigidbody>().AddForce(force) ;

        //canShoot = false;
        //Invoke("CoolDown", cooldown);
    }

    void CoolDown()
    {
        canShoot = true;
    }


    // auto fire setting
    IEnumerator Loop()
    {
        while (true)
        {   
            yield return new WaitForSeconds(frequency);
            FireProjectile();
            
        }
    }

    
}
