using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class PlayerCustomizationUI : MonoBehaviour
{
    public enum customizationPart
    {
        hair = 0,
        band = 1,
        eyes = 2,
        quiver = 3,
        bow = 4,
        legs = 5
    }

    const string unlockedSaveString = "unlockedPlayerCustomzitaion";
    public const int defaultColor = 65;

    [SerializeField]
    Text unlockBTNText;
    [SerializeField]
    Text mainBTNText;

    [SerializeField]
    Material materialToAssign;

    [SerializeField]
    List<CustomizationSlider> parts;

    #region Unity Methods

    void Start()
    {
#if UNITY_EDITOR
        SaveManager.instance.data.SetData(
            unlockedSaveString, 0, SaveData.saveDictionariesTypes.player);
#endif

        if (SaveManager.instance.data.GetData(
            unlockedSaveString, 0, SaveData.saveDictionariesTypes.player) == 0)
        {
            SetMaingTextColorAlpha(0.5f);
        }
        else
        {
            unlockBTNText.enabled = false;
        }

        for (int i = 0; i < parts.Count; i++)
        {
            parts[i].img.material = Instantiate(materialToAssign);
        }

        SetColorsOnEnable();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < parts.Count; i++)
        {
            Destroy(parts[i].img.material);
            parts[i].img.material = null;
        }
    }

    #endregion

    #region Menu Button

    public void Unlock()
    {
        SaveManager.instance.data.SetData(
            unlockedSaveString, 1, SaveData.saveDictionariesTypes.player);
        unlockBTNText.enabled = false;
        SetMaingTextColorAlpha(1f);
    }

    private void SetMaingTextColorAlpha(float alpha)
    {
        Color mainTexColorTransparent = mainBTNText.color;
        mainTexColorTransparent.a = alpha;
        mainBTNText.color = mainTexColorTransparent;
    }

    #endregion

    #region Canvas

    public void SetColorsOnEnable()
    {
        for (int i = 0; i < parts.Count; i++)
        {
            float value = (float)SaveManager.instance.data.GetData(
                    parts[i].part.ToString(), defaultColor, SaveData.saveDictionariesTypes.player);

            value /= 1000f;

            parts[i].img.material.SetFloat("_Hue",
                value
                );

            parts[i].slider.value = value;
        }
    }

    public void SetColor(int part)
    {
        customizationPart _part = (customizationPart)part;
        for (int i = 0; i < parts.Count; i++)
        {
            if (_part == parts[i].part)
                parts[i].img.material.SetFloat("_Hue", parts[i].slider.value);
        }
    }

    public void SavePlayerCustomization()
    {
        for (int i = 0; i < parts.Count; i++)
        {
            SaveManager.instance.data.SetData(
                    parts[i].part.ToString(),
                    (int)(parts[i].slider.value * 1000),
                    SaveData.saveDictionariesTypes.player);
        }
    }

    #endregion
}
