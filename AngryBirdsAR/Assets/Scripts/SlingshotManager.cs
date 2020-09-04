using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotManager : MonoBehaviour
{
    public GameObject ammoPrefab;
    public GameObject ammoPlaceHolder;
    public Transform parent;
    public Transform aimer;
    public Transform leftHolderAnchor;
    public Transform rightHolderAnchor;
    public Transform leftAnchor;
    public Transform rightAnchor;
    public Transform ObjectHolder;
    public LineRenderer leftLine;
    public LineRenderer rightLine;
    public GameObject[] points;
    public float unitSpeed;

    public static SlingshotManager instance;

    void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        leftLine.SetPosition(0, leftAnchor.position);
        rightLine.SetPosition(0, rightAnchor.position);
        //setPath(true);
    }

    // Update is called once per frame
    void Update()
    {
        leftLine.SetPosition(1, leftHolderAnchor.position);
        rightLine.SetPosition(1, rightHolderAnchor.position);
    }

    public void setPath(bool b)
    {
        float yPos = (aimer.up.y * 50) / points.Length;
        float val = yPos;
        for (int i = 0; i < points.Length; i++)
        {
            points[i].SetActive(b);
            points[i].transform.parent = aimer;
            points[i].transform.localPosition = new Vector3(0, val, -0.8f);
            val += yPos;
        }
    }

    public void throwBall(Vector3 direction)
    {
        ObjectHolder.GetComponent<Collider>().enabled = false;
        GameObject clone = Instantiate(ammoPrefab, ammoPlaceHolder.transform.position, Quaternion.identity, parent);
        clone.GetComponent<Rigidbody>().AddForce(direction * unitSpeed, ForceMode.Impulse);
        Destroy(clone, 3f);
    }
}