using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCanvasController : MonoBehaviour
{
    [SerializeField]
    private GameObject winTitle;
    [SerializeField]
    private GameObject loseTitle;
    [SerializeField]
    private GameObject winPanel;
    [SerializeField]
    private GameObject nextLevelBtn;
    [SerializeField]
    private GameObject losePanel;
    [SerializeField]
    private GameManager optionsPanel;
    //Score
    [SerializeField]
    private TextMeshProUGUI scoreText;


    private void Start()
    {
        LoadWinLoseSetUp(GameManager._instance.GetCurrentLifes() != 0);
    }

    public void LoadWinLoseSetUp(bool hasWin)
    {
        //SetUpTitleAndPanels
        winTitle.SetActive(hasWin);
        loseTitle.SetActive(!hasWin);
        winPanel.SetActive(hasWin);
        print("Current lvl: " + GameManager._instance.GetCurrentLvl() + "    maxLvl: " + GameManager._instance.GetMaxLevels() + "     hasWin: " + hasWin);
        nextLevelBtn.SetActive((GameManager._instance.GetCurrentLvl() < GameManager._instance.GetMaxLevels()) && hasWin);
        losePanel.SetActive(!hasWin);
        //SetUpData
        scoreText.text = CalculateScorePoints().ToString();
    }

    int CalculateScorePoints()
    {
        return GameManager._instance.coinAmmount + (GameManager._instance.GetCurrentLifes() * 50);
    }

    //TODORevisar
    public void btnLoadLevel()
    {
        if((GameManager._instance.GetCurrentLvl() <= GameManager._instance.GetMaxLevels()))
        GameManager._instance.sceneC.LoadSceneLvl(GameManager._instance.GetCurrentLvl()+1);
    }


}
