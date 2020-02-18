using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioBossEnemy : Enemy
{
    [SerializeField]
    Transform iniPos;
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
    bool isJumping = false;
    bool canJump = true;
    bool activation = false;
    bool alive = true;
    int phase = 1;
    // Start is called before the first frame update
    public virtual void Start()
    {
        base.Start();
        horizontal = -1;
        StartCoroutine(JumpCadencyTime(jumpCadency));
    }

    public void Update()
    {
        DetectTarget(target);
        if (activation && alive)
        {
            LookPlayer();
            DetectGround();
            phase = GetPhase();
            ManagePhases();
            anim.SetFloat("Speed", Mathf.Abs(horizontal));
        }
     }
     // Update is called once per frame
     public virtual void FixedUpdate()
     {
         if (activation && alive)
         {
            Movement();

            if (!isJumping && canJump)
            {
                canJump = false;
                isJumping = true;
                Jump();
                StartCoroutine(JumpCadencyTime(jumpCadency));
                  
            }
        }
    }
    

    public override void DetectTarget(Transform target)
    {
        vecToTarget = new Vector3(target.position.x, target.position.y + 0.5f, target.position.z) - transform.position;
        if (vecToTarget.magnitude < detectDist && !activation && target.GetComponent<Health>().GetCurrentHealth() > 0)
        {
            anim.SetTrigger("Detected");
            GameManager._instance.sound.PlayActivationBossShot();
            activation = true;
        }
    }

    private void LookPlayer()
    {
        if (target.position.x < this.transform.position.x ) horizontal = -1;
        else horizontal = 1;
        ChangeScale();
    }

    private void DetectGround()
    {
        Collider2D hit = Physics2D.OverlapCircle(groundDetector.position, detectGroundRadius, groundDetectorLM);
        anim.SetBool("IsGrounded", hit != null && hit.CompareTag("Ground"));
    }

    public override void Movement()
    {
        if (rb.velocity.magnitude < maxSpeed) rb.AddForce(Vector2.right * horizontal * moveSpeed * 100 * Time.deltaTime, ForceMode2D.Force);
    }

    private void ManagePhases()
    {
        switch (phase)
        {
            case 2:
                if (canShoot)
                {
                    canShoot = false;
                    CreateFireProjectil();
                }
                break;
            case 3:
                if (canShoot)
                {
                    canShoot = false;
                    CreateFireProjectil();
                }
                break;
        }
    }

    private int GetPhase()
    {
        int ph = -1;
        if (GetComponent<Health>().GetCurrentHealth() > (0.66f * GetComponent<Health>().GetMaxHealth())) ph = 1;
        else if (GetComponent<Health>().GetCurrentHealth() > (0.33f * GetComponent<Health>().GetMaxHealth())) ph = 2;
        else ph = 3;
        return ph;
    }

    private void ChangeScale()
    {
        transform.localScale = new Vector3(horizontal, this.transform.localScale.y, this.transform.localScale.z);
        GetComponentInChildren<CharacterCanvasController>().transform.localScale = new Vector3(horizontal,
                    GetComponentInChildren<CharacterCanvasController>().transform.localScale.y,
                    GetComponentInChildren<CharacterCanvasController>().transform.localScale.z);
    }

    private void CreateFireProjectil()
    {
        GameManager._instance.sound.PlayFireballShot();
        anim.SetTrigger("Fire");
        StartCoroutine(FireProjectil());
    }

    private void Jump()
    {
        //float rotation = (this.transform.position.x < target.transform.position.x) ? -1 : 1;
        //transform.localScale = new Vector3(rotation, 1, 1);
        Vector2 jumpVec = (vecToTarget.normalized * jumpForce) + (Vector2.up * jumpForce);
        rb.AddForce(jumpVec, ForceMode2D.Impulse);
    }
    
    IEnumerator FireProjectil()
    {
        yield return new WaitForSeconds(0.4f);
        GameObject go = Instantiate(fireProjectil, posDisp.transform.position, Quaternion.Euler(new Vector3(posDisp.transform.rotation.x, posDisp.transform.rotation.y, 90*horizontal)));
        go.GetComponent<Missile>().SetHorizontal(horizontal);
        Destroy(go, 10);
        yield return new WaitForSeconds(phase == 2 ? fireCadency : fireCadency * 2 / 3);
        canShoot = true;
    }

    IEnumerator JumpCadencyTime(float time)
    {
        yield return new WaitForSeconds(time);
        isJumping = false;
        yield return new WaitForSeconds(Random.Range(3, 3+time));
        canJump = true;
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, detectDist);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundDetector.position, detectGroundRadius);
    }

    public void Reset()
    {
        this.gameObject.GetComponent<Health>().GainHealth(1000);
        activation = false;
        this.transform.position = new Vector3(iniPos.position.x, iniPos.position.y, iniPos.position.z);
    }

    public void SetAlive(bool value)
    {
        alive = value;
    }
}
