﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelDoor : MonoBehaviour
{
    private bool triggered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !triggered)
        {
            triggered = true;
            GameManager._instance.SetCurrentLvl(GameManager._instance.GetCurrentLvl() + 1);
            GameManager._instance.data.SaveData(GameManager._instance.GetCurrentLvl());
            GameManager._instance.sceneC.LoadGameOver();
        }
    }
}
