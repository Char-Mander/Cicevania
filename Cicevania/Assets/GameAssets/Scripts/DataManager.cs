using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DataManager : MonoBehaviour
{
    private string keyCurrentLvl = "Current Level";

    public void LoadData()
    {
        if(PlayerPrefs.HasKey(keyCurrentLvl))
        GameManager._instance.SetCurrentLvl(PlayerPrefs.GetInt(keyCurrentLvl));
    }

    public void SaveData(int currentLvl)
    {
        PlayerPrefs.SetInt(keyCurrentLvl, currentLvl);
        PlayerPrefs.Save();
    }

    [MenuItem("Utilidades/DeletePlayerPrefs")]
    public static void DeletePrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
