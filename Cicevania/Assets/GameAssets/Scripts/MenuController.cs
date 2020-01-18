using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject panelLevels;
    [SerializeField]
    private Button btnStartGame;
    [SerializeField]
    private Transform content;
    [SerializeField]
    private GameObject btnLvlPref;

    // Start is called before the first frame update
    void Start()
    {
        panelLevels.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BtnShowLevels()
    {
        panelLevels.SetActive(true);
        btnStartGame.enabled = false;
        FillWithButtons();
    }

    public void BtnOptions()
    {
        GameManager._instance.optionsC.SwitchPause();
    }

    public void BtnExitGame()
    {
        Application.Quit();
    }

    private void FillWithButtons()
    {
        for (int i=0; i<GameManager._instance.GetMaxLevels(); i++)
        {
            GameObject myButton = Instantiate(btnLvlPref);
            myButton.transform.SetParent(content);
            bool auxActive;
            auxActive = i < GameManager._instance.GetCurrentLvl();
            myButton.GetComponent<LvlButton>().InitBtn(i+1, auxActive);
        }
    }
}
