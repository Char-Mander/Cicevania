using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : DynamicPowerUp
{
    [SerializeField]
    int recoverHP;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Health>().GetCurrentHealth() > 0)
        {
            GameManager._instance.sound.PlayPowerUpPickUpShot();
            collision.gameObject.GetComponent<Health>().GainHealth(recoverHP);
            Destroy(this.gameObject);
        }
    }

}
