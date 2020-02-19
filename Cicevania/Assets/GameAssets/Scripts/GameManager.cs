using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    [HideInInspector]
    public DataManager data;
    [HideInInspector]
    public OptionsController optionsC;
    [HideInInspector]
    public SceneController sceneC;
    [HideInInspector]
    public SoundManager sound;

    [SerializeField]
    private int maxLevels = 2;
    private int currentLvl = 1;
    private int levelsCompleted = 1;
    [SerializeField]
    private int initLifes = 3;
    private int currentLifes = 3;

    //Puntuaciones
    public int coinAmmount { get; set; }
    public TimeSpan lvlTime { get; set; }
    public int enemyAmmount { get; set; }
    public float goalScore { get; set; }

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        //References
        data = GetComponent<DataManager>();
        optionsC = GetComponentInChildren<OptionsController>();
        sceneC = GetComponent<SceneController>();
        sound = GetComponent<SoundManager>();
        //Init
        data.LoadData();
        InitData();

    }

    public void InitData()
    {
        currentLifes = initLifes;
        coinAmmount = 0;
        enemyAmmount = 0;
        goalScore = 0;
    }

    public int GetMaxLevels(){ return maxLevels; }

    public int GetLevelsCompleted() { return levelsCompleted; }

    public void SetLevelsCompleted(int lvl)
    {
        levelsCompleted = lvl;
    }

    public int GetCurrentLvl() { return currentLvl; }

    public void SetCurrentLvl(int lvl)
    {
        currentLvl = lvl;
    }

    public int GetInitLifes() { return initLifes; }

    public int GetCurrentLifes() { return currentLifes; }

    public void SetCurrentLifes(int currentLifes)
    {
        this.currentLifes = currentLifes;
        if (this.currentLifes > initLifes) this.currentLifes = initLifes;
    }
}
