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

    }

    public void LoseLife()
    {
        if (currentLifes > 1)
        {
            currentLifes -= 1;
            FindObjectOfType<CheckPointController>().Respawn();
            CharacterCanvasController canvas = GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponent<CharacterCanvasController>();
            canvas.UpdateLifesAmmount(currentLifes);
            canvas.UpdateCoinAmmount(coinAmmount);
            canvas.UpdateHealthBar(GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().GetCurrentHealth(), GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().GetMaxHealth());
        }
        else
        {
            sceneC.LoadGameOver();
            //Resetear en GameOver los valores
        }
    }

    public void ResetValues()
    {
        currentLvl = 1;
        currentLifes = initLifes;
    }

    public void ResetScore()
    {
        coinAmmount = 0;
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
    }
}
