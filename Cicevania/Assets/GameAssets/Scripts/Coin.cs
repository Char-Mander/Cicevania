using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    CharacterCanvasController playerCanvas;
    [SerializeField]
    private int coinValue;

    private void Start()
    {
        playerCanvas = FindObjectOfType<CharacterCanvasController>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        GameManager._instance.coinAmmount += coinValue;
        print("Current coins: " + GameManager._instance.coinAmmount);
        playerCanvas.UpdateCoinAmmount(GameManager._instance.coinAmmount);
        Destroy(this.gameObject);
    }
}
