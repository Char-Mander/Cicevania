using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyFishEnemy : Enemy
{
    [SerializeField]
    private Transform groundDetector;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float detectGroundRadius;
    [SerializeField]
    private float detectObstaclesRadius;
    [SerializeField]
    PhysicsMaterial2D slide;
    [SerializeField]
    private Transform obstacleDetector;
    [SerializeField]
    LayerMask groundDetectorLM;
    [SerializeField]
    LayerMask obstacleDetectorLM;
    private bool isCrazy = false;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        horizontal = transform.localScale.x;
    }


    public override void Update()
    {
        //base.Update();
        base.DetectTarget(target);
        Movement();
        Patrol();
    }

    public override void Movement()
    {
        if(rb.velocity.magnitude < maxSpeed) rb.AddForce(Vector2.right * horizontal * moveSpeed * Time.deltaTime, ForceMode2D.Force);
    }

    void Patrol()
    {
        if (!isCrazy)
        {
            Collider2D hit = Physics2D.OverlapCircle(groundDetector.position, detectGroundRadius, groundDetectorLM);
            if (hit == null)
            {
                ChangeScale();
            }
                
        }
        else
        {
            if (this.transform.position.x < target.position.x) horizontal = 1;
            else horizontal = -1;
            ChangeScale();
        }
        DetectObstacles();
    }

    public override void Attack()
    {
        if (!isCrazy)
        {
            isCrazy = true;
            maxSpeed = maxSpeed * 2;
            anim.SetTrigger("Crazy");
            GetComponent<Collider2D>().sharedMaterial = slide;
        }
    }

    private void DetectObstacles()
    {
        Collider2D hit = Physics2D.OverlapCircle(obstacleDetector.position, detectGroundRadius, obstacleDetectorLM);
        if (hit != null)
        {
            ChangeScale();
        }
    }

    private void ChangeScale()
    {
       if(!isCrazy) horizontal = -horizontal;
        transform.localScale = new Vector3(horizontal, this.transform.localScale.y, this.transform.localScale.z);
        GetComponentInChildren<CharacterCanvasController>().transform.localScale = new Vector3(horizontal,
                    GetComponentInChildren<CharacterCanvasController>().transform.localScale.y,
                    GetComponentInChildren<CharacterCanvasController>().transform.localScale.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundDetector.transform.position, detectGroundRadius);
        Gizmos.DrawWireSphere(obstacleDetector.transform.position, detectObstaclesRadius);
    }
}
