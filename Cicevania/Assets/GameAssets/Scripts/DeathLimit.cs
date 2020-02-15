using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLimit : MonoBehaviour
{
    [SerializeField]
    int damage;
    bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !triggered)
        {
            triggered = true;
            collision.GetComponent<Health>().LoseHealth(damage);
            StartCoroutine(WaitForActivateAgain());
        }
    }

    IEnumerator WaitForActivateAgain()
    {
        yield return new WaitForSeconds(2);
        triggered = false;
    }
}
