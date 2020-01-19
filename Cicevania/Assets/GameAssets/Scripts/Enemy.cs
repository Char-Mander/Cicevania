using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int damage;
    [SerializeField]
    private float moveSpeed;
    
    float horizontal = -1;
    bool noCollision = true;
    
    private void Update()
    {
        EnemyMovement();
    }

    void EnemyMovement()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime * horizontal;
        // Llamar en el fixedupdate
        //rb.AddForce(Vector3.right * horizontal * moveSpeed * Time.deltaTime);
        //print(rb.velocity.x);
    }
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") && noCollision)
        {
            print("Horizontal: " + horizontal);
            print("sin colisión: " + noCollision);
            noCollision = false;
            horizontal = -horizontal;
            StartCoroutine(WaitForNextCollision());
        }
    }

    IEnumerator WaitForNextCollision()
    {
        yield return new WaitForSeconds(0.5f);
        noCollision = true;
    }

    public int GetDamage() { return damage; }
}
