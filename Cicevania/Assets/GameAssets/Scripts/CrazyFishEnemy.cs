using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyFishEnemy : Enemy
{
    [SerializeField]
    private Transform groundDetector;

    private bool isCrazy = false;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public override void Movement(float height)
    {

    }

    void Patrol()
    {

    }

    public override void Attack()
    {

    }


    void DetectPlayer()
    {

    }
}
