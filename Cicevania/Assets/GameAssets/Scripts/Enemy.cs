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
    }

    public virtual void DetectTarget(Transform target)
    {
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
        if ((collision.CompareTag("Ground") || (collision.CompareTag("Player")) && noCollision) || collision.CompareTag("Object"))
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
