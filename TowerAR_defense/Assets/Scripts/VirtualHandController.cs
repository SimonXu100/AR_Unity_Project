using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualHandController : MonoBehaviour
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

    //private float ScaleStep = 2.0f;

    private float originalDistance;

    private MeshRenderer currentObjectRenderer;
 	
   
    
    
    void FixedUpdate(){
    	
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
				testTest.text = "VirtualHand:Translate";
				//currentObject.transform.parent.position = transform.position;
				currentObject.transform.position = transform.position;
			}
			// rotate
			else if(operation == 2){
				testTest.text = "VirtualHand:Rotate";
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
				float times = Vector3.Distance(transform.position, currentObject.transform.position) / originalDistance;
				testTest.text = "Virtual: Scaling  " + times.ToString()+ " times" ;
				if(times>= lowerBound && times<=upperBound){
					currentObject.transform.localScale = times * OriginalLocalScale;
				}
			}
			else if(operation == -1){
				testTest.text = "VirtualHand:Confirmed and please choose operation";

			}
    	}
        else{
            if(currentObject == null){
                testTest.text = "VirtualHand:Status  ";
            }
        }
    }

	void OnTriggerEnter(Collider other){

		if(other.gameObject.CompareTag("wall")){
			if(selected == false){
				GetComponent<MeshRenderer>().material = waitForConfirmedMaterial;
				testTest.text = "VirtualHand: Find the wall, confirmed?";
				currentObject = other.gameObject;
				currentParentObject = other.gameObject.transform.parent.gameObject;
				currentObjectRenderer = other.gameObject.GetComponent<MeshRenderer>();
				OriginalLocalScale = other.gameObject.transform.localScale;
				originalDistance = Vector3.Distance(currentObject.transform.position, transform.position);

				OriginalPosition = other.gameObject.transform.localPosition;

				OriginalRotation = other.gameObject.transform.localRotation; 

				OriginalMaterial = other.gameObject.GetComponent<MeshRenderer>().material;

			}
		}

		// only rotate in yaw and pitch
		else if(other.gameObject.CompareTag("turret_head")){
			if(selected == false){
				testTest.text = "VirtualHand:Find the turret , confirmed?";
                GetComponent<MeshRenderer>().material = waitForConfirmedMaterial;
				currentObject = other.gameObject;
				currentParentObject = currentObject.transform.parent.gameObject;
				currentObjectRenderer = other.gameObject.GetComponent<MeshRenderer>();
				OriginalLocalScale = other.gameObject.transform.localScale;
				originalDistance = Vector3.Distance(currentObject.transform.position, transform.position);
				OriginalPosition = other.gameObject.transform.localPosition;
				
				OriginalRotation = other.gameObject.transform.localRotation; 

				OriginalMaterial = other.gameObject.GetComponent<MeshRenderer>().material;
				operation = 2; 

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



    // not selected

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

























