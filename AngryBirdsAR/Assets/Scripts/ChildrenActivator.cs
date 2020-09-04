using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ChildrenActivator : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour trackableBehaviour;
    private GravityPivot gp;

    // Start is called before the first frame update
    void Start()
    {
        trackableBehaviour = GetComponent<TrackableBehaviour>();
        gp = GetComponent<GravityPivot>();

        if (trackableBehaviour)
            trackableBehaviour.RegisterTrackableEventHandler(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            OnTrackingFound();
        else
            onTrackingLost();
    }

    private void OnTrackingFound()
    {
        if (transform.childCount > 0)
            SetChildrenActive(true);
        gp.enabled = true;
    }

    private void onTrackingLost()
    {
        if (transform.childCount > 0)
            SetChildrenActive(false);
    }

    private void SetChildrenActive(bool activeState)
    {
        for (int i = 0; i <= transform.childCount; i++)
            transform.GetChild(i++).gameObject.SetActive(activeState);
    }
}
