using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Database : MonoBehaviour
{
    [SerializeField] List<Item> unlockedFromStart = new List<Item>();
    public List<Item> UnlockedFromStart => unlockedFromStart;
    [SerializeField] List<WeaponBase> unlockedFromStartWeapons = new List<WeaponBase>();
    public List<WeaponBase> UnlockedFromStartWeapons => unlockedFromStartWeapons;
    [SerializeField] List<Item> _allItems = new List<Item>();
    public List<Item> AllItems => _allItems;
    [SerializeField] List<WeaponBase> _allWeapons = new List<WeaponBase>();
    public List<WeaponBase> AllWeapons => _allWeapons;

    [SerializeField] SaveFile blankSave;
    [SerializeField] SaveFile save;
    private void Awake()
    {       
        if (File.Exists(GetSavePath()))
        {
            LoadFile();
        }
        else
        {
            save = blankSave;
            SaveFile();
        }
    }
    public List<Item> GetAllItems()
    {
        return _allItems;
    }
    public List<Item> GetUnlockedItems()
    {
        var output = new List<Item>();
        output.AddRange(UnlockedFromStart);
        output.AddRange(save.UnlockedItems);
        return output;
    }
    public List<WeaponBase> GetAllWeapons()
    {
        return _allWeapons;
    }
    public List<WeaponBase> GetUnlockedWeapons()
    {
        var output = new List<WeaponBase>();
        output.AddRange(unlockedFromStartWeapons);
        output.AddRange(save.UnlockedWeapons);
        return output;
    }
    public void UnlockItem(Item item, bool makeNotification)
    {
        foreach (var unlockedItem in save.UnlockedItems)
        {
            if(unlockedItem == item)
            {
                return;
            }
        }
        save.UnlockedItems.Add(item);
        if(makeNotification)
        {
            //notification
        }
    }
    public void ResetSave()
    {
        Debug.Log("Save reset...");
        save = blankSave;
        SaveFile();
    }
    public void LoadSave(SaveFile save)
    {
        Debug.Log("Loading save...");
        this.save = save;
        Debug.Log("Save loaded.");
    }
    public void SaveFile()
    {
        Debug.Log("Saving...");
        using StreamWriter writer = new StreamWriter(GetSavePath());
        writer.Write(JsonUtility.ToJson(save));
        Debug.Log("Saved.");
    }
    public void LoadFile()
    {
        Debug.Log("Loading file( "+ GetSavePath()+ " )");
        if (File.Exists(GetSavePath()))
        {
            using StreamReader reader = new StreamReader(GetSavePath());
            save = JsonUtility.FromJson<SaveFile>(reader.ReadToEnd());
        }
        else
            throw new System.Exception("Failed to load save! Maybe save file does not exists?");
        Debug.Log("File loaded.");
    }
    public string GetSavePath()
    {
        return Application.persistentDataPath + Path.AltDirectorySeparatorChar + "save.json";
    }
}
[System.Serializable]
public class SaveFile
{
    public List<Item> UnlockedItems = new List<Item>();
    public List<WeaponBase> UnlockedWeapons = new List<WeaponBase>();
    public int voidTokens = 0;
}
    

