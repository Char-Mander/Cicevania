using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLifePowerUp : MonoBehaviour
{

    private void Start()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager._instance.sound.Play1UPShot();
            GameManager._instance.SetCurrentLifes(GameManager._instance.GetCurrentLifes() + 1);
            GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponent<CharacterCanvasController>().UpdateLifesAmmount(GameManager._instance.GetCurrentLifes());
            Destroy(this.gameObject);
        }
    }

}
