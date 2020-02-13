using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalMechanism : MonoBehaviour
{
    [SerializeField]
    private Transform destiny;
    [SerializeField]
    private float fallSpeed;

    private GameObject target;
    private bool triggered = false;
    private bool finished = false;
    private Rigidbody2D targetRB;

    private void Update()
    {
        if(triggered && Vector3.Distance(target.transform.position, destiny.position) > 0.8f && !finished)
        {
            target.transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        }
        else if (triggered && Vector3.Distance(target.transform.position, destiny.position) < 0.8f && !finished)
        {
            finished = true;
            EnableOrDisableTarget(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") & !triggered)
        {
            triggered = true;
            target = collision.gameObject;
            target.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            EnableOrDisableTarget(false);
            UpdateScore();
        }
    }

    private void EnableOrDisableTarget(bool enable)
    {
        target.GetComponent<PlayerController>().enabled = enable;
    }

    private void UpdateScore()
    {
        GameManager._instance.goalScore += Vector3.Distance(target.transform.position, destiny.position) * 7;
    }
}
