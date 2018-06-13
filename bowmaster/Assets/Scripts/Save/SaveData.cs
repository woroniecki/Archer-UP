using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Save/SaveData")]
public class SaveData : ScriptableObject {

    #region Subclasses & enums

    public enum saveDictionariesTypes
    {
        options = 0, easy = 1, medium = 2, difficult = 3, player = 4
    }

    [Serializable]
    public class SaveDict<Tkey, Tvalue>
    {
        [SerializeField]
        public List<Tkey> keys;
        [SerializeField]
        public List<Tvalue> values;

        public SaveDict() { }

        public SaveDict(int targetMaxAmount) {
            keys = new List<Tkey>(targetMaxAmount);
            values = new List<Tvalue>(targetMaxAmount);
        }

        public int GetKeyIndex(Tkey key) {
            return keys.IndexOf(key);
        }

        public void SetValue(int index, Tvalue value)
        {
            values[index] = value;
        }

        public Tvalue GetValue(int index) {
            return values[index];
        }

        public void Add (Tkey key, Tvalue value)
        {
            keys.Add(key);
            values.Add(value);
        }
    }

    #endregion

    [SerializeField]
    Dictionary<saveDictionariesTypes, SaveDict<string, int>> dictionaries;

    string[] saveDictionaryNames;

    void Awake()
    {
        saveDictionaryNames = Enum.GetNames(typeof(saveDictionariesTypes));

        dictionaries = new Dictionary<saveDictionariesTypes, SaveDict<string, int>> (
            saveDictionaryNames.Length
            );

        try
        {
            for(int i = 0; i < saveDictionaryNames.Length; i++)
            {
                if (File.Exists(GetPath(saveDictionaryNames[i])))
                {
                    string data = File.ReadAllText(GetPath(saveDictionaryNames[i]));
                    dictionaries.Add ((saveDictionariesTypes)i, new SaveDict<string, int> ());
                    JsonUtility.FromJsonOverwrite(data, dictionaries[(saveDictionariesTypes)i]);
                }
            }
        }
        catch (Exception e) {
            Debug.LogError(e + " - SaveData");
        }
    }

    public void SetData(string key, int value, saveDictionariesTypes type, bool save = true) {
        if (!dictionaries.ContainsKey(type))
            dictionaries.Add(type, new SaveDict<string, int>(20));

        int keyIndex = dictionaries[type].GetKeyIndex(key);

        if (keyIndex != -1 && dictionaries[type].GetValue(keyIndex) != value) {
            dictionaries[type].SetValue(keyIndex, value);
            if(save)
                Save(type);
            return;
        }
        else if (keyIndex != -1 && dictionaries[type].GetValue(keyIndex) == value)
        {
            return;
        }

        dictionaries[type].Add(key, value);
        if(save)
            Save(type);
    }

    public int GetData(string key, int defaultValue, saveDictionariesTypes type) {
        if (!dictionaries.ContainsKey(type))
            dictionaries.Add(type, new SaveDict<string, int>(20));

        int keyIndex = dictionaries[type].GetKeyIndex(key);
        if (keyIndex != -1)
            return dictionaries[type].GetValue(keyIndex);
        SetData (key, defaultValue, type);

        return defaultValue;
    }

    public void Save(saveDictionariesTypes dictionaryType) {
        File.WriteAllText(
            GetPath(saveDictionaryNames[(int)dictionaryType]),
            JsonUtility.ToJson(dictionaries[dictionaryType])
            );
    }

    public static saveDictionariesTypes SaveTypeByLevel ()
    {
        switch (LevelMenu.GetCurrentDifficultyLevel ())
        {
            case LevelMenu.difficultyLevel.Easy:
                return saveDictionariesTypes.easy;
            case LevelMenu.difficultyLevel.Medium:
                return saveDictionariesTypes.medium;
            case LevelMenu.difficultyLevel.Difficult:
                return saveDictionariesTypes.difficult;
        }
        return saveDictionariesTypes.medium;
    }

    static string GetPath(string fileName) { return Application.persistentDataPath + "/" + fileName + ".json"; }
}
