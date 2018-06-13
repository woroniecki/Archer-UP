using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComponent : MonoBehaviour {

    int state;

    public Transform camera;

    void Start () {
        state = SaveManager.instance.data.GetData(CameraButton.prefCameraSaveState, 0, SaveData.saveDictionariesTypes.options);
        Accelerometer.accelerometerInputMultiplier = state == 0 ? -1f : 1f;
        if (state == 0)
        {
            camera.localEulerAngles = new Vector3(0, 180, 0);
            camera.localPosition = new Vector3(camera.localPosition.x, camera.localPosition.y, 10);
        }
    }
}
