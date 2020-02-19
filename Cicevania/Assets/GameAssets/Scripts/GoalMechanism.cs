using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalMechanism : MonoBehaviour
{
    [SerializeField]
    private Transform destiny;
    [SerializeField]
    private GameObject flag;
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
            flag.transform.position = new Vector3(flag.transform.position.x, target.transform.position.y, flag.transform.position.z);
        }
        else if (triggered && Mathf.Abs(Vector3.Distance(target.transform.position, destiny.position)) < 0.7f && !finished)
        {
            flag.transform.position = new Vector3(flag.transform.position.x, destiny.transform.position.y-1, flag.transform.position.z);
            finished = true;
            EnableOrDisableTarget(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") & !triggered)
        {
            triggered = true;
            GameManager._instance.sound.PlayGoalFlagDownShot();
            target = collision.gameObject;
            target.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            EnableOrDisableTarget(false);
            UpdateScore();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == target.tag)
        {
            target.gameObject.GetComponent<Rigidbody2D>().AddForce((Vector2.up * Time.deltaTime * 50) + (Vector2.right  * Time.deltaTime * 50));
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
