
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{       
        //public GameObject largeTarget;
        //public Transform target;
        //public GameObject target;
        private GameObject target;
        private GameObject TowerBase;
        private float speed = 0.02f;
        private float timeAfterHit = 0.1f;
        //private float rotateSpeed = 1.0f;
        private Vector3  position1;
        private Vector3  position2;
        private Vector3  position3;

        private Quaternion rotation1;
        private Quaternion rotation2;
        private Quaternion rotation3;



        void Awake(){
                target = GameObject.Find("TowerImageTarget/Plane/TowerBase/TargetPoint");
                TowerBase = GameObject.Find("TowerImageTarget/Plane/TowerBase");
        }

        /*
        void FixedUpdate(){
                if(target == null){
                        Destroy(target);
                        return;
                }

                transform.LookAt(target.transform, Vector3.up);
                //Vector3 force = transform.forward * 1;
                //GetComponent<Rigidbody>().AddForce(force);
                //Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
                //rb.AddForce (movement * speed);
                transform.Rotate(0.0f, -90.0f, 0.0f, Space.Self);
                transform.Translate(-transform.forward * Time.deltaTime * speed);
        }*/

        // method 2: by moveTowards to 
        void FixedUpdate(){
                if(TowerBase == null){
                        Destroy(gameObject);
                        return;
                }
                transform.LookAt(target.transform, Vector3.up);
                transform.Rotate(0.0f, -180.0f, 0.0f, Space.Self);

                float step =  speed * Time.deltaTime; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);

                // Check if the position of the cube and sphere are approximately equal.

                if (Vector3.Distance(transform.position, target.transform.position) < 0.0001f)
                {
                    return;
                }    
        }

        // when the enemy meets with projectiles, they destory itself
        void OnTriggerEnter(Collider other){
            if(other.gameObject.CompareTag("Projectile")){
                Destroy(gameObject, timeAfterHit);
            }
        }
}






















