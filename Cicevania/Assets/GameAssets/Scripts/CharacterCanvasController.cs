using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCanvasController : MonoBehaviour
{
    [SerializeField]
    private Image imgHealthBar;
    /*[SerializeField]
    private Text txtLifesAmmount;*/
    [SerializeField]
    private Text txtCoinAmmount;
    [SerializeField]
    private GameObject panelTutorial;
    [SerializeField]
    private GameObject lifeImage;
    [SerializeField]
    private Transform contentLifes;

    private void Start()
    {
        if (GameManager._instance.GetCurrentLvl() == 1 && panelTutorial != null) panelTutorial.SetActive(true);
        else if(panelTutorial !=null) panelTutorial.SetActive(false);
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

    public void UpdateCoinAmmount(int ammount)
    {
        //print("Entra al updateCoinAmmount");
        if(txtCoinAmmount != null) txtCoinAmmount.text = "x "+ ammount.ToString();
    }

}
