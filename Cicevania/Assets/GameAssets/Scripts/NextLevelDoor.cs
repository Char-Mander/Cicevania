using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(GameManager._instance.GetCurrentLvl() < GameManager._instance.GetMaxLevels())
            {
                GameManager._instance.SetCurrentLvl(GameManager._instance.GetCurrentLvl() + 1);
                GameManager._instance.sceneC.LoadSceneLvl(GameManager._instance.GetCurrentLvl());
            }
            else
            {
                //Cargamos la escena de win
            }
        }
    }
}
