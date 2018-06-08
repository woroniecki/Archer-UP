using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComponent : MonoBehaviour {

    public Transform camera;

	void Start () {
        int state = SaveManager.instance.data.GetData(CameraButton.prefCameraSaveState, 0, SaveData.saveDictionariesTypes.options);
        if(state == 0)
        {
            camera.localEulerAngles = new Vector3(0, 180, 0);
            camera.localPosition = new Vector3(camera.localPosition.x, camera.localPosition.y, 10);
        }
    }
}
