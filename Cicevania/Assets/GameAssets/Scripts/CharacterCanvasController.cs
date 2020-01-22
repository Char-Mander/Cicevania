﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCanvasController : MonoBehaviour
{
    [SerializeField]
    private Image imgHealthBar;
    [SerializeField]
    private Text txtLifesAmmount;
    [SerializeField]
    private Text txtCoinAmmount;

    private void Start()
    {
        UpdateLifesAmmount(GameManager._instance.GetCurrentLifes());
        UpdateCoinAmmount(GameManager._instance.coinAmmount);
    }

    public void UpdateHealthBar(int current, int max)
	{
        imgHealthBar.fillAmount = (float)current / (float)max;
	}

    public void UpdateLifesAmmount(int ammount)
    {
        if (txtLifesAmmount != null) txtLifesAmmount.text = "x " + ammount.ToString();
    }

    public void UpdateCoinAmmount(int ammount)
    {
        print("Entra al updateCoinAmmount");
        if(txtCoinAmmount != null) txtCoinAmmount.text = "x "+ ammount.ToString();
    }

}
