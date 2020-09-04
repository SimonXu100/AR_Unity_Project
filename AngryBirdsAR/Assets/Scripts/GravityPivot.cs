using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPivot : MonoBehaviour
{
    public float gravityMagnitude;

    // Start is called before the first frame update
    void Start()
    {
        gravityMagnitude = Physics.gravity.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Physics.gravity = -transform.up * gravityMagnitude;
        Debug.Log((Physics.gravity, Physics.gravity.magnitude));
    }
}
