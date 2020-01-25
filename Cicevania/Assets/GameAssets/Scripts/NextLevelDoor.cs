using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
             GameManager._instance.SetCurrentLvl(GameManager._instance.GetCurrentLvl() + 1);
             GameManager._instance.sceneC.LoadGameOver();
        }
    }
}
