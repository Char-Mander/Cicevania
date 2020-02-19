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
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI coinsText;
    [SerializeField]
    private TextMeshProUGUI enemiesText;
    [SerializeField]
    private TextMeshProUGUI totalScoreText;


    private void Start()
    {
        LoadWinLoseSetUp(GameManager._instance.GetCurrentLifes() > 0);
        ScoreDataSetUp();
    }

    public void LoadWinLoseSetUp(bool hasWin)
    {
        //SetUpTitleAndPanels
        winTitle.SetActive(hasWin);
        loseTitle.SetActive(!hasWin);
        winPanel.SetActive(hasWin);
        nextLevelBtn.SetActive((GameManager._instance.GetCurrentLvl() <= GameManager._instance.GetMaxLevels()) && hasWin);
        losePanel.SetActive(!hasWin);
        //SetUpSound
        if (hasWin)
        {
            if (GameManager._instance.GetCurrentLvl() <= GameManager._instance.GetMaxLevels())
                GameManager._instance.sound.PlayLevelCompleted();
            else GameManager._instance.sound.PlayWorldClear();
        }
        else  GameManager._instance.sound.PlayGameOver();
    }

    public void btnLoadMenu()
    {
        GameManager._instance.sceneC.LoadMenu();
    }

    //TODORevisar
    public void btnLoadLevel()
    {
        GameManager._instance.sceneC.LoadSceneLvl(GameManager._instance.GetCurrentLvl());
    }

    void ScoreDataSetUp()
    {
        timeText.text = string.Format("{0:D2}:{1:D2}", GameManager._instance.lvlTime.Minutes, GameManager._instance.lvlTime.Seconds);
        coinsText.text = GameManager._instance.coinAmmount.ToString();
        enemiesText.text = GameManager._instance.enemyAmmount.ToString();
        totalScoreText.text = CalculateScorePoints().ToString();
    }

    int CalculateScorePoints()
    {
        int total = 0;
        total += GameManager._instance.coinAmmount;
        total += GameManager._instance.enemyAmmount * 5;
        total += total / GameManager._instance.GetInitLifes() * GameManager._instance.GetCurrentLifes();
        total += (int) GameManager._instance.goalScore * 6;
        total += 45 - (int)GameManager._instance.lvlTime.TotalSeconds / 2;

        return total;
    }
}
