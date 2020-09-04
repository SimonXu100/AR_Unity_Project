using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vuforia;
using System.Linq;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    private CameraSettings cameraSettings;
    private TrackableSettings trackableSettings;
    void Start()
    {
        this.cameraSettings = FindObjectOfType<CameraSettings>();
        this.trackableSettings = FindObjectOfType<TrackableSettings>();
    }

    
    public void ClickAutofocus(bool enable)
    {
        if (this.cameraSettings)
        {
            this.cameraSettings.SwitchAutofocus(enable);
        }
    }

    public void ClickExtendedTracking(bool enable)
    {
        if (this.trackableSettings)
        {
            this.trackableSettings.ToggleDeviceTracking(enable);
        }
    }

}
