using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {

    static public SaveManager instance;

    public SaveData data;

    void Awake () {
        if(instance == null)
            instance = this;
        data = (SaveData)ScriptableObject.CreateInstance(typeof(SaveData));
        DontDestroyOnLoad(this.gameObject);
    }

    public static void Create()
    {
        GameObject gobj = new GameObject("SaveManager");
        gobj.AddComponent<SaveManager>();
    }
}
