using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadSceneLvl(int lvl)
    {
        StartCoroutine(WaitForLoad());
        SceneManager.LoadScene("Lvl" + lvl);
        GameManager._instance.SetCurrentLvl(lvl);
        print("currentlvl al cambiar de escena: " + GameManager._instance.GetCurrentLvl());
        GameManager._instance.InitData();
    }

    public void LoadMenu()
    {
        StartCoroutine(WaitForLoad());
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitForLoad());
        SceneManager.LoadScene("GameOver");
    }

    IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(1);
    }
}
