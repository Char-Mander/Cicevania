using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPowerUp : DynamicPowerUp
{
    [SerializeField]
    float duration;
    [SerializeField]
    int increaseValue;
 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Health>().GetCurrentHealth() > 0)
        {
            GameManager._instance.sound.PlayPowerUpPickUpShot();
            collision.gameObject.GetComponent<PlayerController>().IncreaseAttackDamageOn(duration, increaseValue);
            Destroy(this.gameObject);
        }
    }

}
