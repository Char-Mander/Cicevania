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
    }

    public void LoadMenu()
    {
        StartCoroutine(WaitForLoad());
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitForLoad());
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(1);
    }
}
