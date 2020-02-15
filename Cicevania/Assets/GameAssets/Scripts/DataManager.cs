using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DataManager : MonoBehaviour
{
    private string keyCurrentLvl = "Current Level";
    private string keyCurrentLifes = "Current Lifes";

    public void LoadData()
    {
        //Carga el nivel actual
        if(PlayerPrefs.HasKey(keyCurrentLvl))
        GameManager._instance.SetCurrentLvl(PlayerPrefs.GetInt(keyCurrentLvl));
    }
    
    public void SaveData(int currentLvl)
    {
        //Guarda el nivel actual
        PlayerPrefs.SetInt(keyCurrentLvl, currentLvl);
        PlayerPrefs.Save();
    }

    [MenuItem("Utilidades/Borrar partida")]
    public static void DeletePrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
