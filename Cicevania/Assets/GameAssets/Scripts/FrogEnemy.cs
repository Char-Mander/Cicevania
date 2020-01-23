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
            float rotation = (this.transform.position.x < target.transform.position.x) ? -1 : 1;
            transform.localScale = new Vector3(rotation, 1, 1);
            canAttack = false;
            Vector2 jumpVec = (vecToTarget.normalized * jumpForce) + (Vector2.up * jumpForce);
            rb.AddForce(jumpVec, ForceMode2D.Impulse);
            StartCoroutine(Reload());
            Animations();
        }
    }

    void Animations()
    {
        anim.SetFloat("VelY", rb.velocity.y);
    }

    IEnumerator Reload() {
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }
}

