using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public float drag = 5;
    Vector3 screenPoint;
    Vector3 offset;
    Vector3 scanPos;

    // Start is called before the first frame update
    void Start()
    {
        scanPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    screenPoint = Camera.main.WorldToScreenPoint(scanPos);
                    offset = scanPos - Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, screenPoint.z));
                    offset.z += drag;
                    SlingshotManager.instance.aimer.eulerAngles = new Vector3(90, 0, 0);
                    break;
                case TouchPhase.Moved:
                    Vector3 curScreenPoint = new Vector3(touch.position.x, touch.position.y, screenPoint.z);
                    Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
                    transform.position = curPosition;
                    break;
                case TouchPhase.Ended:
                    var direction = scanPos - transform.position;
                    SlingshotManager.instance.throwBall(direction);
                    Invoke("ResetDirection", 1f);
                    transform.position = scanPos;
                    break;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                screenPoint = Camera.main.WorldToScreenPoint(scanPos);
                offset = scanPos - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
                offset.z += drag;
                SlingshotManager.instance.aimer.eulerAngles = new Vector3(90, 0, 0);
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
                transform.position = curPosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                var direction = scanPos - transform.position;
                SlingshotManager.instance.throwBall(direction);
                Invoke("ResetDirection", 1f);
                transform.position = scanPos;
            }
        }
    }
}

