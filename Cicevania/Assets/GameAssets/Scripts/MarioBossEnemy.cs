using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioBossEnemy : Enemy
{
    [SerializeField]
    private GameObject fireProjectil;
    [SerializeField]
    private GameObject posDisp;
    [SerializeField]
    private float jumpCadency;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float fireCadency;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    Transform groundDetector;
    [SerializeField]
    private float detectGroundRadius;
    [SerializeField]
    LayerMask groundDetectorLM;

    bool canShoot = true;
    bool jumpAttack = true;
    bool isJumping = false;
    bool activation = false;
    int phase = 1;
    // Start is called before the first frame update
    public virtual void Start()
    {
        base.Start();
    }

    public void Update()
    {
        DetectTarget(target);
        if (activation)
        {
            LookPlayer();
            DetectGround();
            /* phase = GetPhase();
             print("Phase: " + phase);
             LookPlayer();
             ManagePhases();*/
        }
     }
     // Update is called once per frame
     public virtual void FixedUpdate()
     {
         if (activation)
         {
             Movement();
            /*if (!isJumping)
            {
              isJumping = true;
                Jump();
                StartCoroutine(JumpCadencyTime(jumpCadency));
                  
            }*/
        }
    }
    

    public override void DetectTarget(Transform target)
    {
        vecToTarget = new Vector3(target.position.x, target.position.y + 0.5f, target.position.z) - transform.position;
        if (vecToTarget.magnitude < detectDist && !activation)
        {
            anim.SetTrigger("Detected");
            activation = true;
        }
    }

    private void LookPlayer()
    {
        if (this.transform.position.x < target.position.x) horizontal = 1;
        else horizontal = -1;
        ChangeScale();
    }

    private void DetectGround()
    {
        Collider2D hit = Physics2D.OverlapCircle(groundDetector.position, detectGroundRadius, groundDetectorLM);
        print("Hit: " + (hit != null && hit.CompareTag("Ground")));
        anim.SetBool("IsGrounded", hit != null && hit.CompareTag("Ground"));
    }

    public override void Movement()
    {
        if (rb.velocity.magnitude < maxSpeed) rb.AddForce(Vector2.right * horizontal * moveSpeed * Time.deltaTime, ForceMode2D.Force);
    }

    private void ManagePhases()
    {
        switch (phase)
        {
            case 1: if (jumpAttack)
                    {
                        jumpAttack = false;
                    }
                break;
            case 2:
                if (canShoot)
                {
                    print("Dispara 2");
                    canShoot = false;
                    CreateFireProjectil();
                    StartCoroutine(FireCadencyTime(fireCadency));
                }
                else if (jumpAttack)
                {
                    jumpAttack = false;
                    Jump();
                    StartCoroutine(JumpCadencyTime(jumpCadency));
                }
                break;
            case 3:
                if (canShoot)
                {
                    print("Dispara 3");
                    canShoot = false;
                    CreateFireProjectil();
                    StartCoroutine(FireCadencyTime(fireCadency * 2 / 3));
                }
               else if (jumpAttack)
                {
                    jumpAttack = false;
                    Jump();
                    StartCoroutine(JumpCadencyTime(jumpCadency * 2 / 3));
                }
                break;
        }
    }

    private int GetPhase()
    {
        int phase = -1;
        print("Currentbosshealth: " + GetComponent<Health>().GetCurrentHealth() + " maxhealth: " + GetComponent<Health>().GetMaxHealth());
        print((2 / 3) * GetComponent<Health>().GetMaxHealth());
        if (GetComponent<Health>().GetCurrentHealth() > ((2 / 3) * GetComponent<Health>().GetMaxHealth())) phase = 1;
        else if (GetComponent<Health>().GetCurrentHealth() > ((1 / 3) * GetComponent<Health>().GetMaxHealth())) phase = 2;
        else phase = 3;
        return phase;
    }

    private void ChangeScale()
    {
        horizontal = -horizontal;
        transform.localScale = new Vector3(horizontal, this.transform.localScale.y, this.transform.localScale.z);
        GetComponentInChildren<CharacterCanvasController>().transform.localScale = new Vector3(horizontal,
                    GetComponentInChildren<CharacterCanvasController>().transform.localScale.y,
                    GetComponentInChildren<CharacterCanvasController>().transform.localScale.z);
    }

    private void CreateFireProjectil()
    {
        print("Crea un proyectil");
        GameObject go = Instantiate(fireProjectil, posDisp.transform.position, posDisp.transform.rotation);
        Destroy(go, 10);
    }

    private void Jump()
    {
        //float rotation = (this.transform.position.x < target.transform.position.x) ? -1 : 1;
        //transform.localScale = new Vector3(rotation, 1, 1);
        Vector2 jumpVec = (vecToTarget.normalized * jumpForce) + (Vector2.up * jumpForce);
        rb.AddForce(jumpVec, ForceMode2D.Impulse);
    }

    IEnumerator FireCadencyTime(float time)
    {
        yield return new WaitForSeconds(time);
        canShoot = true;
    }

    IEnumerator JumpCadencyTime(float time)
    {
        yield return new WaitForSeconds(0.2f);
        isJumping = false;
        yield return new WaitForSeconds(time-0.2f);
        jumpAttack = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, detectDist);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundDetector.position, detectGroundRadius);
    }
}
