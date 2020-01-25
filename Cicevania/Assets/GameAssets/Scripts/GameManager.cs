using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    [HideInInspector]
    public DataManager data;
    [HideInInspector]
    public OptionsController optionsC;
    [HideInInspector]
    public SceneController sceneC;

    [SerializeField]
    private int maxLevels = 1;
    private int currentLvl = 1;
    [SerializeField]
    private int initLifes = 3;
    private int currentLifes = 3;
    public int coinAmmount { get; set; }

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
        //Init
        data.LoadData();
        InitData();

    }

    public void InitData()
    {
        coinAmmount = 0;
        currentLifes = initLifes;
    }

    public int GetMaxLevels(){ return maxLevels; }

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
