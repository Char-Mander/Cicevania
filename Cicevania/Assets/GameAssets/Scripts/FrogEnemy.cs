using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogEnemy : Enemy
{
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float attackDelay;

    bool canAttack;

    public override void Start()
    {
        base.Start();
        canAttack = true;
    }

    public override void Update()
    {
        DetectTarget(target);
    }

    public override void Attack()
    {
        if (canAttack) {
            canAttack = false;
            Vector2 jumpVec = (vecToTarget.normalized * jumpForce) + (Vector2.up * jumpForce);
            rb.AddForce(jumpVec, ForceMode2D.Impulse);
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload() {
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }
}

