using UnityEngine;
using UnityEngine.UI;

public class SoundButton : TextButton
{
    void Awake()
    {
        prefSaveState = "Sound";
        activeText = "Sound: ON";
        inactiveText = "Sound: OFF";
    }

    public override void SetText()
    {
        base.SetText();
        SetSoundVolume();
    }

    public override void ChangeValue()
    {
        base.ChangeValue();
        SetSoundVolume();
    }

    public void SetSoundVolume()
    {
        SoundsManager.Instance.setVolume(SaveManager.instance.data.GetData(prefSaveState, 0, SaveData.saveDictionariesTypes.options));
    }
}
