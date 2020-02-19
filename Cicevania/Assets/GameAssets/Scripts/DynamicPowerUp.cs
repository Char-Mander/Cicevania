using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPowerUp : MonoBehaviour
{
    public float moveSpeed;
    public Transform obstacleDetectorR;
    public Transform obstacleDetectorL;
    public float detectRadius;
    public LayerMask groundDetectorLM;

    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public float horizontal;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float value = (Random.Range(0, 2) == 0) ? 1 : -1;
        horizontal = value;
        rb.AddForce(new Vector2(value / 2, 1) * 2, ForceMode2D.Impulse);
    }

    private void Update()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime * horizontal;
        DetectObstacles();
    }

    private void DetectObstacles()
    {
        Collider2D hitR = Physics2D.OverlapCircle(obstacleDetectorR.position, detectRadius, groundDetectorLM);
        Collider2D hitL = Physics2D.OverlapCircle(obstacleDetectorL.position, detectRadius, groundDetectorLM);
        if (hitR != null || hitL != null)
            horizontal = -horizontal;
    }
}
