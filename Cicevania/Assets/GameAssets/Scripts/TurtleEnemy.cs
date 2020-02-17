using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleEnemy : Enemy
{
    [SerializeField]
    float attackDelay;
    [SerializeField]
    List<Transform> wayPoints = new List<Transform>();

    Stack<Transform> returnPath = new Stack<Transform>();
    Transform currentWP;
    private int wpIndex = 0;
    bool flying = true;
    bool horizontalChanged = true;

    [SerializeField]
    private Transform obstacleDetector;
    [SerializeField]
    private float detectRadius;
    [SerializeField]
    LayerMask groundDetectorLM;

    public override void Start()
    {
        base.Start();
        currentWP = wayPoints[wpIndex];
        returnPath.Push(wayPoints[wpIndex]);
    }

    public override void Update()
    {
        if(GetComponent<Health>().GetCurrentHealth() > GetComponent<Health>().GetMaxHealth() / 2) Fly();
        else Walk();
    }

    void Fly()
    {
        if (rb.gravityScale > 0)
        {
            rb.gravityScale = 0;
        }
        if(rb.velocity.y != 0)
        {
            rb.velocity = new Vector3(0,0,0);
        }
        FlyingMovement();
    }

    void Walk()
    {
        if (rb.gravityScale == 0)
        {
            rb.gravityScale = 1;
            anim.SetBool("IsGrounded", true);
            horizontal = transform.localScale.x;
            transform.localScale = new Vector3(horizontal, transform.localScale.y, transform.localScale.z);
        }
        base.Movement();
        DetectObstacles();
    }

    void FlyingMovement()
    {
        Vector2 dir = (currentWP.position - this.transform.position).normalized;
        Vector2 nextPos = (Vector2)this.transform.position + dir * moveSpeed * Time.deltaTime;
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        this.transform.position = nextPos;
        if (Vector3.Distance(this.transform.position, currentWP.transform.position) < 0.1f)
            NextWayPoint();
    }

    void NextWayPoint()
    {
        if (wpIndex < wayPoints.Count - 1)
        {
            wpIndex++;
            returnPath.Push(wayPoints[wpIndex]);
            currentWP = wayPoints[wpIndex];
        }
        else
        {
            if (returnPath.Count > 0)
            {
                currentWP = returnPath.Pop();
                if (int.Parse(currentWP.gameObject.name.Substring(currentWP.gameObject.name.Length-1)) == wpIndex)
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                returnPath.Push(currentWP);
                wpIndex = 0;
                currentWP = wayPoints[wpIndex];
            }
        }
    }


    private void DetectObstacles()
    {
        Collider2D hit = Physics2D.OverlapCircle(obstacleDetector.position, detectRadius, groundDetectorLM);
        if (hit != null)
        {
            ChangeScale();
        }
    }

    private void ChangeScale()
    {
        horizontal = -horizontal;
        transform.localScale = new Vector3(horizontal, this.transform.localScale.y, this.transform.localScale.z);
        GetComponentInChildren<CharacterCanvasController>().transform.localScale = new Vector3(this.transform.localScale.x,
                    GetComponentInChildren<CharacterCanvasController>().transform.localScale.y,
                    GetComponentInChildren<CharacterCanvasController>().transform.localScale.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(obstacleDetector.transform.position, detectRadius);
    }

}
