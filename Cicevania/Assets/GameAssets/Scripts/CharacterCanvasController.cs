using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CharacterCanvasController : MonoBehaviour
{
    [SerializeField]
    private Image imgHealthBar;
    [SerializeField]
    private Text txtCoinAmmount;
    [SerializeField]
    private GameObject panelTutorial;
    [SerializeField]
    private GameObject lifeImage;
    [SerializeField]
    private Transform contentLifes;
    public Text timeText;

    private void Start()
    {
        if (GameManager._instance.GetCurrentLvl() == 1 && panelTutorial != null) panelTutorial.SetActive(true);
        else if(panelTutorial !=null) panelTutorial.SetActive(false);
        StartCoroutine(UpdateTime());
    }

    public void UpdateHealthBar(int current, int max)
	{
        imgHealthBar.fillAmount = (float)current / (float)max;
	}

    public void UpdateLifesAmmount(int currentLifes)
    {
        // if (txtLifesAmmount != null) txtLifesAmmount.text = "x " + ammount.ToString();
        if (contentLifes.childCount > currentLifes)
        {
            Destroy(contentLifes.GetChild(0).gameObject);
        }
        else if (contentLifes.childCount < currentLifes)
        {
            int dif = currentLifes - contentLifes.childCount;
            for(int i=0; i<dif; i++)
            {
                Instantiate(lifeImage, contentLifes);
            }
        }
    }

    IEnumerator UpdateTime()
    {
        yield return new WaitForSeconds(1);
        GameManager._instance.lvlTime = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
        timeText.text = string.Format("{0:D2}:{1:D2}", GameManager._instance.lvlTime.Minutes, GameManager._instance.lvlTime.Seconds);
        StartCoroutine(UpdateTime());
    }

    public void UpdateCoinAmmount(int ammount)
    {
        //print("Entra al updateCoinAmmount");
        if(txtCoinAmmount != null) txtCoinAmmount.text = "x "+ ammount.ToString();
    }

}
