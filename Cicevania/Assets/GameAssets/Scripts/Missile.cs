using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]
    int damage;
    [SerializeField]
    float moveSpeed;

    private float horizontal=1;

    private void Update()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime * horizontal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().GetDamage(damage);
            Destroy(this.gameObject);
        }
    }

    public void SetHorizontal(float dir)
    {
        horizontal = dir;
    }
}
