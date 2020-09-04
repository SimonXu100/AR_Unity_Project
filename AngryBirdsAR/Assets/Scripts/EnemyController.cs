using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	private float setup_time = 0.2f;
	private float start_time;
    private float death_time = 0.0f;

    //public ParticleSystem effect;
    public float HP = 100;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        //effect.Stop();
        start_time = Time.time;
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (death_time != 0.0f && Time.time > death_time + 0.5f){
            Debug.Log("death:" + death_time);
            if(CompareTag("Enemy"))
                gm.kills += 1;
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
    	if (Time.time > start_time + setup_time){
            if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player")){
                GetDamage(100);
            }
    	}
    }

    public void GetDamage(float damage){
        HP -= damage;
        if (HP <= 0){
            //effect.Play();
            //effect.enableEmission = true;
            death_time = Time.time;
        }
    }
}
