using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{

    private Text textButton;
    public static string prefSoundState = "Sound";

    private string activeSoundText = "Sound: ON";
    private string inactiveSoundText = "Sound: OFF";

    void Start()
    {
        textButton = Utility.FindChildByName("Text", transform).GetComponent<Text>();
        setSoundVolumeText();
    }

    public void setSoundVolumeText()
    {
        int state = PlayerPrefs.GetInt(prefSoundState, 0);
        if (state == 0)
            textButton.text = inactiveSoundText;
        else
            textButton.text = activeSoundText;
        setSoundVolume();
    }

    public void ChangeVolume()
    {
        int state = PlayerPrefs.GetInt(prefSoundState, 0);
        if (state == 0)
        {
            PlayerPrefs.SetInt(prefSoundState, 1);
            textButton.text = activeSoundText;
        }
        else
        {
            PlayerPrefs.SetInt(prefSoundState, 0);
            textButton.text = inactiveSoundText;
        }
        setSoundVolume();
    }

    public void setSoundVolume()
    {
        SoundsManager.Instance.setVolume(PlayerPrefs.GetInt(prefSoundState, 0));
    }
}
