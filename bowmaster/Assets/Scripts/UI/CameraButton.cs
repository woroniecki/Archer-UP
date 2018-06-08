using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraButton : TextButton
{
    public const string prefCameraSaveState = "Camera";

    void Awake()
    {
        prefSaveState = prefCameraSaveState;
        activeText = "Camera: Left";
        inactiveText = "Camera: Right";
    }
}
