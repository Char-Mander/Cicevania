using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    CharacterCanvasController playerCanvas;
    [SerializeField]
    private int coinValue;
    private bool used = false;

    private void Start()
    {
        playerCanvas = GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponent<CharacterCanvasController>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !used)
        {
            used = true;
            PickUp();
        }
    }

    void PickUp()
    {
        GameManager._instance.sound.PlayCoinPickUpShot();
        GameManager._instance.coinAmmount += coinValue;
        playerCanvas.UpdateCoinAmmount(GameManager._instance.coinAmmount);
        Destroy(this.gameObject);
    }
}
