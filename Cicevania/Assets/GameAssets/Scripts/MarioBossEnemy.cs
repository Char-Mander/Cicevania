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

    bool canShoot = true;
    bool jumpAttack = true;
    bool activation = false;
    int phase = 1;
    // Start is called before the first frame update
    public virtual void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        DetectTarget(target);
        if (activation)
        {
            print("Phase: " + phase);
            LookPlayer();
            if(!jumpAttack) Movement();
            ManagePhases();
        }
    }


    public override void DetectTarget(Transform target)
    {
        vecToTarget = target.position - transform.position;
        if (vecToTarget.magnitude < detectDist && !activation)
        {
            //Salta la animación del gorro
            activation = true;
        }
    }

    private void LookPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, vecToTarget.normalized, detectDist, lm);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
        {
            if (hit.collider.gameObject.transform.position.x < this.transform.position.x) transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            else transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
    }

    public override void Movement()
    {
        if (rb.velocity.magnitude < maxSpeed)
        {
            print("Debería moverse");
            rb.AddForce(Vector2.right * transform.localScale.x * moveSpeed * Time.deltaTime, ForceMode2D.Force);
        }
    }

    private void ManagePhases()
    {
        phase = GetPhase();
        switch (phase)
        {
            case 1: if (jumpAttack)
                    {
                        jumpAttack = false;
                        Jump();
                        StartCoroutine(JumpCadencyTime(jumpCadency));
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
        if (GetComponent<Health>().GetCurrentHealth() > 2 / 3 * GetComponent<Health>().GetMaxHealth()) phase = 1;
        else if (GetComponent<Health>().GetCurrentHealth() > 1 / 3 * GetComponent<Health>().GetMaxHealth()) phase = 2;
        else phase = 3;
        return phase;
    }


    private void CreateFireProjectil()
    {
        print("Crea un proyectil");
        GameObject go = Instantiate(fireProjectil, posDisp.transform.position, posDisp.transform.rotation);
        Destroy(go, 10);
    }

    private void Jump()
    {
        float rotation = (this.transform.position.x < target.transform.position.x) ? -1 : 1;
        transform.localScale = new Vector3(rotation, 1, 1);
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
        yield return new WaitForSeconds(time);
        jumpAttack = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, detectDist);
    }
}
