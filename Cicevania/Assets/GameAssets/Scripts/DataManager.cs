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
        //Carga las vidas actuales
        if (PlayerPrefs.HasKey(keyCurrentLifes))
        GameManager._instance.SetCurrentLifes(PlayerPrefs.GetInt(keyCurrentLifes));
    }

    public void SaveData(int currentLvl, int currentLifes)
    {
        //Guarda el nivel actual
        PlayerPrefs.SetInt(keyCurrentLvl, currentLvl);
        //Guarda las vidas actuales
        PlayerPrefs.SetInt(keyCurrentLifes, currentLifes);
        PlayerPrefs.Save();
    }

    //[MenuItem("Utilidades/DeletePlayerPrefs")]
    public static void DeletePrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
