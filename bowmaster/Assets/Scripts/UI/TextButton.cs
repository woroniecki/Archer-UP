using UnityEngine;
using UnityEngine.UI;

public abstract class TextButton : MonoBehaviour {
    protected Text textButton;

    protected string prefSaveState = "";

    protected string activeText = "ON";
    protected string inactiveText = "OFF";

    void Start()
    {
        textButton = GetComponentInChildren<Text>();
        SetText();
    }

    public virtual void SetText()
    {
        int state = SaveManager.instance.data.GetData(prefSaveState, 0, SaveData.saveDictionariesTypes.options);
        if (state == 0)
            textButton.text = inactiveText;
        else
            textButton.text = activeText;
    }

    public virtual void ChangeValue()
    {
        int state = SaveManager.instance.data.GetData(prefSaveState, 0, SaveData.saveDictionariesTypes.options);
        if (state == 0)
        {
            SaveManager.instance.data.SetData(prefSaveState, 1, SaveData.saveDictionariesTypes.options);
            textButton.text = activeText;
        }
        else
        {
            SaveManager.instance.data.SetData(prefSaveState, 0, SaveData.saveDictionariesTypes.options);
            textButton.text = inactiveText;
        }
    }
}
