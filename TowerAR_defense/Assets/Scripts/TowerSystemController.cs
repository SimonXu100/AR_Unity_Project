using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
// control the main game logistics
public class TowerSystemController : MonoBehaviour
{	

    public GameObject enemyBase1;
    public GameObject enemyBase2;
    public GameObject enemyBase3;

    public GameObject wallPrefab;
    public GameObject referenceWall;
    public GameObject wallParent;

    public GameObject target;
    public GameObject parentObject;
    private Vector3  position1;
    private Vector3  position2;
    private Vector3  position3;
    private Vector3  wallPosition;

    private Quaternion rotation1;
    private Quaternion rotation2;
    private Quaternion rotation3;
    private Quaternion wallRotation;


    private Vector3 localScale1;
    private Vector3 localScale2;
    private Vector3 localScale3;
    private Vector3 wallLocalScale;

    private Vector3 localPosition1;
    private Vector3 localPosition2;
    private Vector3 localPosition3;
    private Vector3 wallLocalPosition;



    private Quaternion localRotation1;
    private Quaternion localRotation2;
    private Quaternion localRotation3;
    private Quaternion wallLocalRotation;

    private float timeAfterDestory = 3f;

    public GameObject enemyBasePrefab;

    private bool canReSpawn1;
    private bool canReSpawn2;
    private bool canReSpawn3;

    private int SwitchModeFlag;

    public Text infoText;

    private GameObject VirtualHandMode;
    private GameObject RayCastingMode;
    
    public VirtualHandController virtualScript;
    public RayCastingController raycastingScript;
    void Awake()
    {
        //Get a reference to the projectile spawn point. By providing the path to the object like this, we are making an 
        //inefficient method call more efficient
        //projectileSpawnTransform = GameObject.Find("Head/Cannon_1").transform;
        canReSpawn1 = true;
        canReSpawn2 = true;
        canReSpawn3 = true;

        localPosition1 = enemyBase1.transform.localPosition;
        localRotation1 = enemyBase1.transform.localRotation;
        localScale1 = enemyBase1.transform.localScale;

        localPosition2 = enemyBase2.transform.localPosition;
        localRotation2 = enemyBase2.transform.localRotation;
        localScale2 = enemyBase2.transform.localScale;

        localPosition3 = enemyBase3.transform.localPosition;
        localRotation3 = enemyBase3.transform.localRotation;
        localScale3 = enemyBase3.transform.localScale;



        position1 = enemyBase1.transform.position;
        rotation1 = enemyBase1.transform.rotation;

        position2 = enemyBase2.transform.position;
        rotation2 = enemyBase2.transform.rotation;

        position3 = enemyBase3.transform.position;
        rotation3 = enemyBase3.transform.rotation;


        // for wall
        wallLocalPosition = referenceWall.transform.localPosition;
        wallLocalRotation = referenceWall.transform.localRotation;
        wallLocalScale = referenceWall.transform.localScale;

        wallPosition = referenceWall.transform.position;
        wallRotation = referenceWall.transform.rotation;

        // default mode: virtual hand when flag == 1
        SwitchModeFlag = 1;


        //VirtualHandMode = GameObject.Find("TowerImageTarget/Wand1ImageTarget/LeftHand");
        //RayCastingMode = GameObject.Find("TowerImageTarget/Wand2ImageTarget/RightHand");

        //virtualScript.enabled = true;
        //raycastingScript.enabled = false;

    }
    
    
    void FixedUpdate(){
        if(target == null){
            Debug.Log("do not find the target");
            canReSpawn1 = false;
            canReSpawn2 = false;
            canReSpawn3 = false;
        }
        if(enemyBase1 == null && canReSpawn1){
            canReSpawn1 = false;
            StartCoroutine(ExampleCoroutine(1));
        }
        if(enemyBase2 == null && canReSpawn2){
            canReSpawn2 = false;
            StartCoroutine(ExampleCoroutine(2));
        }
        if(enemyBase3 == null && canReSpawn3){
            canReSpawn3 = false;
            StartCoroutine(ExampleCoroutine(3));
        }

    }
    
    
    public void reSpawn(int flag, GameObject enemyBasePrefab, Vector3 position, Quaternion rotation){
        if(flag == 1){
            enemyBase1 = (GameObject)Instantiate(enemyBasePrefab, position, rotation);
            enemyBase1.transform.SetParent(parentObject.transform);
            enemyBase1.transform.localScale = localScale1;
            enemyBase1.transform.localPosition = localPosition1;
            enemyBase1.transform.localRotation = localRotation1;
 
        }
        else if(flag == 2){
            enemyBase2 = (GameObject)Instantiate(enemyBasePrefab, position, rotation);
            enemyBase2.transform.SetParent(parentObject.transform);
            enemyBase2.transform.localScale = localScale2;
            enemyBase2.transform.localPosition = localPosition2;
            enemyBase2.transform.localRotation = localRotation2;
        }
        else if(flag == 3){
            enemyBase3 = (GameObject)Instantiate(enemyBasePrefab, position, rotation);
            enemyBase3.transform.SetParent(parentObject.transform);
            enemyBase3.transform.localScale = localScale3;
            enemyBase3.transform.localPosition = localPosition3;
            enemyBase3.transform.localRotation = localRotation3;
        }
    }
    


    // delay mechanism
    
    IEnumerator ExampleCoroutine(int flag)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(timeAfterDestory);
        if(flag == 1){
            reSpawn(1, enemyBasePrefab, position1, rotation1);
            canReSpawn1 = true;

        }
        else if(flag == 2){
            reSpawn(2, enemyBasePrefab, position2, rotation2);
            canReSpawn2 = true;
        }
        else if(flag == 3){
            reSpawn(3, enemyBasePrefab, position3, rotation3);
            canReSpawn3 = true;
        }      
    }
    

    void CoolDown(int flag)
    {   
        if(canReSpawn1 == false){
            canReSpawn1 = true;
        }
        
        if(canReSpawn2 == false){
            canReSpawn2 = true;
        }
        
        if(canReSpawn3 == false){
            canReSpawn3 = true;
        }
    }

    public void clickCreateWall(){
        referenceWall = (GameObject)Instantiate(wallPrefab, wallPosition, wallRotation);
        referenceWall.transform.SetParent(wallParent.transform);
        referenceWall.transform.localScale = wallLocalScale;
        referenceWall.transform.localPosition = wallLocalPosition;
        referenceWall.transform.localRotation = wallLocalRotation;
    }


    /*
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
    */

    // switch which way to manipulate: virtual hand or raycasting
    public void clickSwitchMode(){
        if(SwitchModeFlag == 1){
            infoText.text = "Switch to Raycasting";
            //VirtualHandMode.GetComponent<VirtualHandController>.enabled = false;
            //RayCastingMode.GetComponent<RayCastingController>.enabled = true;
            //VirtualHandMode.GetComponent<>();
            virtualScript.enabled = false;
            raycastingScript.enabled = true;
            SwitchModeFlag = 2;
        }
        else if(SwitchModeFlag == 2){
            infoText.text = "Switch to Virtual hand";
            //enabled = true;
            //RayCastingMode.GetComponent<RayCastingController>.enabled = false;
            virtualScript.enabled = true;
            raycastingScript.enabled = false;
            SwitchModeFlag = 1;
        }
        

    }

}









