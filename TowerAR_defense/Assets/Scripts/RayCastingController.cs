using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RayCastingController : MonoBehaviour
{
   
    // Start is called before the first frame update
    bool selected = false;
    // when operation == 1: translate
    // when operation == 2: rotate
    // when operation == 3: scaling
    int operation = -1;
    public GameObject target;
    public Text testTest;

    // change the status of materials when sth got fetched
    public Material selectedMaterial;
    public Material normalMaterial;
    public Material waitForConfirmedMaterial;


    private GameObject currentObject;

    private GameObject currentParentObject;


    private float upperBound = 3.0f;
    private float lowerBound = 0.1f;

    private Vector3 OriginalLocalScale;
    
    private Vector3 OriginalPosition;

    private Quaternion OriginalRotation;

    private Material OriginalMaterial; 

    private Vector3 deltaDistance;

    //private float ScaleStep = 2.0f;

    private float originalDistance;

    private MeshRenderer currentObjectRenderer;
    

    // for testing
    public GameObject target2;

    public GameObject pointer;

    // For manipulation
    
    void Update()
    {
        
        if(selected == true){
            // selected oject will become semi-transparent
            //GetComponent<MeshRenderer>().material = selectedMaterial;

            
            if(currentObject == null){
                return;
            }

            //currentObjectRenderer = currentObject.GetComponent<Renderer>();
            //currentObjectRenderer.material.color.a = 0.5f;
            // translate
            if(operation == 1){
                // method 1: directly change the position
                //currentObject.transform.position = transform.position;

                // method 2: setParent
                testTest.text = "RayCasting: Translate";
                currentObject.transform.position = transform.position + deltaDistance;
            }
            // rotate
            else if(operation == 2){
                testTest.text = "RayCasting: Rotate";
                // sperate the operation with other operation
                //currentObject.transform.parent = currentParentObject.transform;
                currentObject.transform.LookAt(transform);
            }
            // scaling
            else if(operation == 3){
                // localScale: relative to its parent
                // uniformly scale 
                // move faraway: scaling up
                // move closer: scaling down
                // sperate the operation with other operation
                //currentObject.transform.parent = currentParentObject.transform;
                float times = Vector3.Distance(transform.position, currentObject.transform.position) / originalDistance * 2;
                testTest.text = "Raycasting: Scaling  " + times.ToString()+ " times" ;
                if(times>= lowerBound && times<=upperBound){
                    currentObject.transform.localScale = times * OriginalLocalScale;
                }
            }
            else if(operation == -1){
                testTest.text = "RayCasting: Confirmed and please choose operation";

            }
        }
        else{
            if(currentObject == null){
                testTest.text = "RayCasting:Status  ";
            }
        }
          
    }

    /*
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }*/

    // For selection
    void FixedUpdate(){
    	RaycastHit hit;
    	// new direction
        //Vector3 direction = (pointer.transform.position - transform.position);
        Vector3 direction = pointer.transform.TransformDirection(Vector3.forward);
        Ray ray = new Ray(pointer.transform.position, direction);
    	//Vector3 direction = (pointerPointer.transform.position - transform.position) / (float) Vector3.Distance(pointerPointer.transform.position, transform.position);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(pointer.transform.position, direction * hit.distance, Color.red);
            Debug.Log("Did Hit");
            
	        if(hit.transform.gameObject.CompareTag("wall")){
				if(selected == false){
					testTest.text = "RayCasting: Find the wall, confirmed?";
                    GetComponent<MeshRenderer>().material = waitForConfirmedMaterial;
					currentObject = hit.transform.gameObject;
					currentParentObject = currentObject.transform.parent.gameObject;
                    currentObjectRenderer = currentObject.GetComponent<MeshRenderer>();
					OriginalLocalScale = currentObject.gameObject.transform.localScale;
					originalDistance = Vector3.Distance(currentObject.transform.position, transform.position);

                    OriginalPosition = currentObject.transform.localPosition;
                    OriginalRotation = currentObject.transform.localRotation; 
                    OriginalMaterial = currentObject.GetComponent<MeshRenderer>().material;
				}
			}
			// only rotate in yaw and pitch
			else if(hit.transform.gameObject.CompareTag("turret")){
				if(selected == false){
                    GetComponent<MeshRenderer>().material = waitForConfirmedMaterial;
					testTest.text = "RayCasting: Find the turret, confirmed?";
                    // control the turret head 
					currentObject = hit.transform.Find("Head").gameObject; 
					currentParentObject = currentObject.transform.parent.gameObject;
                    currentObjectRenderer = GetComponent<MeshRenderer>();
                    OriginalLocalScale = currentObject.transform.localScale;

                    OriginalPosition = currentObject.transform.localPosition;
                
                    OriginalRotation = currentObject.transform.localRotation; 

                    OriginalMaterial = currentObject.GetComponent<MeshRenderer>().material;
					operation = 2; 
				}
			}
        }
        
    }


    public void changeSelectedStatus(){
        if(selected){
            if(currentObject != null){
                GetComponent<MeshRenderer>().material = normalMaterial;
                currentObject.GetComponent<MeshRenderer>().material = OriginalMaterial;
                currentObject.transform.parent = currentParentObject.transform;
                currentObject.GetComponent<Collider>().isTrigger = true;
                operation = -1;
                selected = false;

                // eliminate the deselected object
                currentObject = null;
            }
        } 
        else{
            if(currentObject != null){
                deltaDistance = currentObject.transform.position - transform.position;
                GetComponent<MeshRenderer>().material = selectedMaterial;

                currentObject.GetComponent<Collider>().isTrigger = false;
                
                if(currentObject.tag == "turret_head"){
                    currentObject.GetComponent<MeshRenderer>().material = waitForConfirmedMaterial;
                }
                else if(currentObject.tag == "wall"){
                    currentObject.GetComponent<MeshRenderer>().material = waitForConfirmedMaterial;
                }
                selected = true;
            }
        }
    }

    public void changeTranslate(){
        if(selected){
            operation = 1;
        }
    }

    public void changeRotate(){
        if(selected){
            operation = 2;
        }
    }

    public void changeScale(){
        if(selected){
            operation = 3;
        }
    }


    public void undo(){
        // before declutch
        if(selected && currentObject != null){
            operation = -1;
            currentObject.transform.localPosition = OriginalPosition;
            currentObject.transform.localRotation = OriginalRotation;
            currentObject.transform.localScale = OriginalLocalScale;
        }
        if(!selected){
            GetComponent<MeshRenderer>().material = normalMaterial;   
        }

    }      


}
