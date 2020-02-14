using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    [SerializeField]
    private float value;
    [SerializeField]
    private float time;
    private void Start()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager._instance.sound.PlayPowerUpPickUpShot();
            collision.gameObject.GetComponent<PlayerController>().IncreaseJumpForceOn(time, value);
            Destroy(this.gameObject);
        }
    }
}
