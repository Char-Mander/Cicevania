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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float value = (Random.Range(0, 1) == 0) ? 1 : -1;
        horizontal = value;
        rb.AddForce(new Vector2(value/2, 1) * 2, ForceMode2D.Impulse);
    }

    private void Update()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime * horizontal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().GainHealth(recoverHP);
            Destroy(this.gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {
            horizontal = -horizontal;
        }
    }

}
