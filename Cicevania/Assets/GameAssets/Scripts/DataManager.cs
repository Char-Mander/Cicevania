using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DataManager : MonoBehaviour
{
    private string keyLevelsCompleted = "Levels Completed";

    public void LoadData()
    {
        //Carga los niveles completados hasta el momento
        if (PlayerPrefs.HasKey(keyLevelsCompleted))
            GameManager._instance.SetLevelsCompleted(PlayerPrefs.GetInt(keyLevelsCompleted));
    }
    
    public void SaveData(int levelsCompleted)
    {
        //Guarda los niveles completados hasta el momento
        PlayerPrefs.SetInt(keyLevelsCompleted, levelsCompleted);
        PlayerPrefs.Save();
    }

    [MenuItem("Utilidades/Borrar partida")]
    public static void DeletePrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
