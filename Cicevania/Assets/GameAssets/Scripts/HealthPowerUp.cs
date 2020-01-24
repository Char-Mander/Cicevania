using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    int recoverHP;

    Rigidbody2D rb;
    float horizontal;
    bool canJump = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float value = (Random.Range(0, 1) == 0) ? 1 : -1;
        horizontal = value;
        rb.AddForce(new Vector2(value/2, 1) * 2, ForceMode2D.Impulse);
        StartCoroutine(WaitForJump());
    }

    private void Update()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime * horizontal;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.CompareTag("Ground") || collision.CompareTag("Enemy")) && canJump)
        {
             rb.AddForce(new Vector2(horizontal, 1) * 1.5f, ForceMode2D.Impulse);
             canJump = false;
             StartCoroutine(WaitForJump());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().GainHealth(recoverHP);
            Destroy(this.gameObject);
        }
        else if (collision.CompareTag("Ground") || collision.CompareTag("Enemy"))
        {
           horizontal = -horizontal;
        }
    }

    IEnumerator WaitForJump()
    {
        yield return new WaitForSeconds(1f);
        canJump = true;
    }

}
