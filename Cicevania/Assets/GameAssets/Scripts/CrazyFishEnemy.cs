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
    private float detectRadius;
    [SerializeField]
    PhysicsMaterial2D slide;

    [SerializeField]
    LayerMask groundDetectorLM;

    private bool isCrazy = false;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }


    public override void Update()
    {
        //base.Update();
        base.DetectTarget(target);
        Movement(0);
        Patrol();
    }

    public override void Movement(float height)
    {
        if(rb.velocity.magnitude < maxSpeed) rb.AddForce(Vector2.right * transform.localScale.x * moveSpeed * Time.deltaTime, ForceMode2D.Force);
    }

    void Patrol()
    {
        if (!isCrazy)
        {
            Collider2D hit = Physics2D.OverlapCircle(groundDetector.position, detectRadius, groundDetectorLM);
            if (hit == null)
            {
                Vector3 aux = transform.localScale;
                aux.x *= -1;
                transform.localScale = aux;
            }
                
        }
        else
        {
            if(this.transform.position.x < target.position.x) transform.localScale = new Vector3(1, 1, 1);
            else transform.localScale = new Vector3(-1, 1, 1);
        }
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundDetector.transform.position, detectRadius);
    }
}
