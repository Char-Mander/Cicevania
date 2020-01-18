using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCanvasController : MonoBehaviour
{
    [SerializeField]
    private Image imgHealthBar;
    [SerializeField]
    private Text txtCoinAmmount;

    public void UpdateHealthBar(int current, int max)
	{
        imgHealthBar.fillAmount = (float)current / (float)max;
	}

    public void UpdateCoinAmmount(int ammount)
    {
        txtCoinAmmount.text = ammount.ToString();
    }
}
