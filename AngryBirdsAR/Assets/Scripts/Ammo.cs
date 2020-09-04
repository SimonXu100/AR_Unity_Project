using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float damage = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        var ec = collision.gameObject.GetComponent<EnemyController>();
        if (ec)
        {
            ec.GetDamage(damage);
        }
        Destroy(gameObject);
    }
}
