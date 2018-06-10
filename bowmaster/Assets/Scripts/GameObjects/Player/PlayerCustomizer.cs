using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCustomizer : MonoBehaviour {
    
    [System.Serializable]
    public class CustomizedPart
    {
        public PlayerCustomizationUI.customizationPart part;
        public SpriteRenderer renderer;
    }

    [SerializeField]
    Material materialToAssign;

    public List<CustomizedPart> parts;

    private void Start()
    {
        for (int i = 0; i < parts.Count; i++)
        {
            parts[i].renderer.material = Instantiate(materialToAssign);
        }
        LoadColorsOnEnable();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < parts.Count; i++)
        {
            Destroy(parts[i].renderer.material);
            parts[i].renderer.material = null;
        }
    }

    public void LoadColorsOnEnable()
    {
        for (int i = 0; i < parts.Count; i++)
        {
            float value = (float)SaveManager.instance.data.GetData(
                    parts[i].part.ToString(), PlayerCustomizationUI.defaultColor, SaveData.saveDictionariesTypes.player);

            value /= 1000f;

            parts[i].renderer.material.SetFloat("_Hue",
                value
                );
        }
    }

}
