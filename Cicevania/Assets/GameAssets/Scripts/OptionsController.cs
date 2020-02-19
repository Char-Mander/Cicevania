using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsController : MonoBehaviour
{
    [SerializeField]
    private GameObject optionPanel;

    private bool isPaused = false;
    private bool soundOn = false;

    // Start is called before the first frame update
    void Start()
    {
        optionPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchPause();
        }
    }


    public void BackMenuBtn()
    {
        GameManager._instance.sound.StopMusic();
        GameManager._instance.sound.PlayButtonSound();
        GameManager._instance.sceneC.LoadMenu();
    }

    public void ResumeBtn()
    {
        GameManager._instance.sound.PlayButtonSound();
        SwitchPause();
    }

    public void SwitchPause()
    {
        isPaused = !isPaused;
        GameManager._instance.sound.PlayPauseMusic();
        optionPanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0.000001f : 1;
    }

}
