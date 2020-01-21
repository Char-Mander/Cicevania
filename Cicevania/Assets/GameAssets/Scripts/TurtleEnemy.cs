using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleEnemy : Enemy
{
    [SerializeField]
    float distanceOverGround;
    [SerializeField]
    float attackDelay;
    [SerializeField]
    Transform leftLimit;
    [SerializeField]
    Transform rightLimit;

    bool canAttack = true;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        if(GetComponent<Health>().GetCurrentHealth() > GetComponent<Health>().GetMaxHealth() / 2) Fly();
        else Walk();
        DetectTarget(target);
    }

    void Fly()
    {
        rb.gravityScale = 0;
        Movement(distanceOverGround);
    }

    void Walk()
    {
        rb.gravityScale = 1;
        base.Movement(0);
    }

    public override void Movement(float height)
    {

    }



    public override void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            rb.AddForce(Vector2.down, ForceMode2D.Impulse);
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }
}
