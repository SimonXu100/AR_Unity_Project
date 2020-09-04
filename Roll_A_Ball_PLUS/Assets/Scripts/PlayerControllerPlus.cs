using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerControllerPlus : MonoBehaviour {
    public GameObject player;

    public float speed;

    private Rigidbody rb;

    private int count;

    public Text ScoreText;
    
    public Text TimerText;

    public Text winText;

    public Text testText;

    public GameObject yourButton;

    public GameObject slider;
    
    public float original_speed;



    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        count  = 0;
        SetCountText();
        winText.text = "";
        slider.SetActive(false);
        original_speed = speed;
    }

    void FixedUpdate ()
    {   
        // moved based on keyboard
        /*
        TimerText.text = "Time: " + Time.time.ToString();
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        rb.AddForce (movement * speed);
        */

        // moved based on touch
        // more touches, higher speed
        
        if(Input.touchCount>0)
        {
            Touch t = Input.GetTouch(0);
            Camera b = Camera.current;
            Ray r = b.ScreenPointToRay(t.position);
            RaycastHit hit;
            if(Physics.Raycast(r,out hit))
            {
                Vector3 p=hit.point;
                p = p - transform.position;
                p.y = 0;
                p.Normalize();
                rb.AddForce(p * speed);
            }
        }
       
        

        // out of bound area checking
        if(rb.transform.position.y < -20){
            // back to the origin
            rb.transform.position = new Vector3(0,0,0);
            speed = original_speed;
        }

    }
    
    void OnTriggerEnter(Collider other){
        
        if(other.gameObject.CompareTag("Pick Up")){
            other.gameObject.SetActive(false);
            count = count  + 1;
            SetCountText();
        }
        else if(other.gameObject.CompareTag("Platform2"))
        {
            slider.SetActive(true);
        }
        else if(other.gameObject.CompareTag("RotateObject"))
        {
            //float FixeScale = 1.0f;
            //player.transform.SetParent(other.gameObject.transform);
            //player.transform.localScale = new Vector3 (FixeScale/other.gameObject.transform.localScale.x,FixeScale/other.gameObject.transform.localScale.y,
            //                                            FixeScale/other.gameObject.transform.localScale.z);
            
            player.transform.position = other.gameObject.transform.position;
            player.transform.position =  new Vector3(player.transform.position.x, player.transform.position.y + 0.3f, player.transform.position.z);
            player.transform.rotation = other.gameObject.transform.rotation;
        }
        else if(other.gameObject.CompareTag("Platform3"))
        {
            // remove parental relationship between player and rotate objet
            testText.text = "Info: ";
            player.transform.SetParent(null);
            player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            slider.SetActive(true);
            // change the speed, change the speed by checking the value of sliders

        }
        else if(other.gameObject.CompareTag("Platform4")){
            testText.text = "Info: ";
            player.transform.SetParent(null);
            player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            slider.SetActive(true);
            // back to the original speed
            speed = original_speed;
            other.isTrigger = false;

        }
        else if(other.gameObject.CompareTag("RotatingFloor"))
        {
            //currentSpeed = (float)(speed * 0.2);
            //player.transform.position =  new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 0.3f, other.gameObject.transform.position.z);
            player.transform.rotation = other.gameObject.transform.rotation;


        }
        else if(other.gameObject.CompareTag("FallBlock"))
        {
            other.attachedRigidbody.isKinematic = true;
            other.attachedRigidbody.useGravity = false;
            StartCoroutine(ExampleCoroutine(other));
            other.isTrigger = false;
        }
        else if(other.gameObject.CompareTag("GameEnd"))
        {
            slider.SetActive(false);
            if(count == 12)
            {
                testText.text = "At " + Time.time.ToString() + "seconds,   You win!!! Game Over";
            }
            else{
                testText.text = "still coins left!!!";
            }
            TimerText.enabled = false;
            //if(count >=1 )  winText.text = "You Win!!!";
            yourButton.SetActive(true);
        }
        else if(other.gameObject.CompareTag("velocityBoost"))
        {
            testText.text = "boosting 2 X ";
            //add speed
            speed = speed * 2.0f;
        }
        
    }

    void SetCountText(){
        ScoreText.text = "Score: " + count.ToString();
    }
    
    IEnumerator ExampleCoroutine(Collider other)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1.0f);
        other.attachedRigidbody.useGravity = true;
        other.attachedRigidbody.isKinematic = false;
    }

    
}
