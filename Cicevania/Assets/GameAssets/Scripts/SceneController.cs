using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadSceneLvl(int lvl)
    {
        GameManager._instance.sound.StopMusic();
        GameManager._instance.sound.PlayMainTheme();
        SceneManager.LoadScene("Lvl" + lvl);
        GameManager._instance.SetCurrentLvl(lvl);
        GameManager._instance.InitData();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitForLoadGameOver());
    }

    IEnumerator WaitForLoadGameOver()
    {
        yield return new WaitForSeconds(0.75f);
        SceneManager.LoadScene("GameOver");
    }
}
