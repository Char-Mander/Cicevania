using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluffyEnemy : Enemy
{
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float attackDelay;

    bool canAttack;
    bool detected = false;

    public override void Start()
    {
        base.Start();
        canAttack = true;
    }

    public override void Update()
    {
        DetectTarget(target);
        Animations();
    }

    public override void DetectTarget(Transform target)
    {
        vecToTarget = target.position - transform.position;
        if (vecToTarget.magnitude < detectDist)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, vecToTarget.normalized, detectDist, lm);
            if (hit.collider.CompareTag("Player"))
            {
                if (hit.collider.gameObject.transform.position.x < this.transform.position.x) transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                else transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                detected = true;
                Attack();
            }
            else
            {
                detected = false;
            }
        }
    }

    public override void Attack()
    {
        if (canAttack) {
            float rotation = (this.transform.position.x < target.transform.position.x) ? -1 : 1;
            transform.localScale = new Vector3(rotation, 1, 1);
            canAttack = false;
            Vector2 jumpVec = (vecToTarget.normalized * jumpForce) + (Vector2.up * jumpForce);
            rb.AddForce(jumpVec, ForceMode2D.Impulse);
            StartCoroutine(Reload());
        }
    }

    void Animations()
    {
        anim.SetBool("Detected", detected);
    }

    IEnumerator Reload() {
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }
    
}

