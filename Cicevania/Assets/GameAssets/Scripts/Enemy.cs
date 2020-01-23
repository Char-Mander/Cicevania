using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Transform target;
    [HideInInspector]
    public Animator anim;
    public Rigidbody2D rb;
    public float detectDist;
    public float moveSpeed;
    public LayerMask lm;
    [HideInInspector]
    public Vector2 vecToTarget;

    [SerializeField]
    private int damage;
    
    float horizontal = -1;
    bool noCollision = true;
    bool setHeight = false;

    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public virtual void Update()
    {
        DetectTarget(target);
        Movement(0);
    }

    public virtual void Movement(float height)
    {
        Vector3 pos = Vector3.zero;
        if(!setHeight && height > 0) {
            pos = new Vector3(0, height, 0);
            setHeight = true;
        }
        transform.position += pos + (Vector3.right * moveSpeed * Time.deltaTime * horizontal);
        // Llamar en el fixedupdate
        //rb.AddForce(Vector3.right * horizontal * moveSpeed * Time.deltaTime);
        //print(rb.velocity.x);
    }

    public virtual void DetectTarget(Transform target)
    {
        /* if(Vector2.Distance(this.transform.position, target.position) <= detectDist)
         {
             Vector2 dirToTarget = new Vector2(target.position.x - this.transform.position.x, target.position.y - this.transform.position.y).normalized;
             RaycastHit2D hit = Physics2D.Raycast(this.transform.position, dirToTarget, detectDist, lm);
             Debug.DrawLine(transform.position, hit.point, Color.green);
             if (hit.collider.CompareTag("Player"))
             {
                 //Debug.DrawLine(transform.position, hit.point, Color.red);
                 Attack();
             }
         }*/
        vecToTarget = target.position - transform.position;
        if (vecToTarget.magnitude < detectDist)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, vecToTarget.normalized, detectDist, lm);
            if (hit.collider.CompareTag("Player"))
            {
                Attack();
            }
        }
    }

    public virtual void Attack()
    {
        print("Enemy attacks");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.CompareTag("Ground") || collision.CompareTag("Player")) && noCollision)
        {
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

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, detectDist);
    }*/
}
