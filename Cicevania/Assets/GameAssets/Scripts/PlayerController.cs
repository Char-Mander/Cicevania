using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float crouchMoveSpeed;
    [SerializeField]
    private int jumpDamage;
    [SerializeField]
    private int superAttackDamage;
    [SerializeField]
    private float hitGodModeDuration;
    [SerializeField]
    GameObject areaSuperAttack;

    Character2DController cController;
    Animator anim;
    Rigidbody2D rb;
    float horizontal;
    bool isJumping = false;
    bool isDoubleJumping = false;
    bool canDoSuperAttack = true;
    bool isCrouching = false;
    bool canJumpOrHit = true;
    bool godMode;
    // Start is called before the first frame update
    void Start()
    {
        cController = GetComponent<Character2DController>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        areaSuperAttack.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(horizontal));
        anim.SetFloat("VelY", rb.velocity.y);
        anim.SetBool("IsGrounded", cController.GetIsGrounded());
        if (Input.GetKeyDown(KeyCode.UpArrow) && canJumpOrHit)
        {
            if (!isJumping)
                isJumping = true;
            if (!isDoubleJumping && !cController.GetIsGrounded())
            {
                isDoubleJumping = true;
                cController.Jump();
            }
        }

        //TODO Ataque
        if(Input.GetKey(KeyCode.R) && canJumpOrHit && canDoSuperAttack)
        {
                canDoSuperAttack = false;
                SuperAttack();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isCrouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isCrouching = false;
        }

        if (godMode) Blink();
        else ChangePlayerAlpha(1);
    }

    private void FixedUpdate()
    {
        float speed = isCrouching ? crouchMoveSpeed : moveSpeed;
        cController.Move(horizontal * Time.deltaTime * speed, isCrouching, isJumping);
    }

    public void OnGround()
    {
        isJumping = false;
        isDoubleJumping = false;
    }

    public void OnCrouch(bool value)
    {
        anim.SetBool("Crouch", value);
        canJumpOrHit = !value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            if (JumpAttack(collision.transform.position) || isCrouching)
            {
                cController.Jump();
                collision.gameObject.GetComponent<Health>().LoseHealth(jumpDamage);
            }
            else
            {
                GetDamage(collision.collider.GetComponent<Enemy>().GetDamage());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !canDoSuperAttack)
        {
            collision.GetComponent<Health>().LoseHealth(superAttackDamage);
        }
        
    }

    //TODO Función en la que ataca con el paraguas
    private void SuperAttack()
    {
        StartCoroutine(ActiveAreaSuperAttack());
        anim.SetTrigger("SuperAttack");
        StartCoroutine(WaitForDoSuperAttackAgain());
    }

    private bool JumpAttack(Vector2 posEnemy)
    {
        return posEnemy.y < transform.position.y && rb.velocity.y < 0;
    }

    public void GodModeOn(float time)
    {
        if(!godMode) StartCoroutine(ActiveGodMode(time));
    }

    IEnumerator ActiveGodMode(float duration)
    {
        godMode = true;
        yield return new WaitForSeconds(duration);
        godMode = false;
    }

    void Blink()
    {
        ChangePlayerAlpha(Mathf.PingPong(Time.time * 5, 1) + 0.5f);
    }

    void ChangePlayerAlpha(float value)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color auxColor = sr.color;
        auxColor.a = value;
        sr.color = auxColor;
    }

    IEnumerator ActiveAreaSuperAttack()
    {
        yield return new WaitForSeconds(0.1f);
        GameManager._instance.sound.PlaySuperAttackShot();
        areaSuperAttack.SetActive(true);
    }

    IEnumerator WaitForDoSuperAttackAgain()
    {
        yield return new WaitForSeconds(0.45f);
        canDoSuperAttack = true;
        areaSuperAttack.SetActive(false);
    }

    public void IncreaseAttackDamageOn(float time, int value)
    {
        StartCoroutine(IncreateAttakDamage(time, value));
    }

    IEnumerator IncreateAttakDamage(float time, int value)
    {
        jumpDamage *= value;
        superAttackDamage *= value;
        yield return new WaitForSeconds(time);
        jumpDamage /= value;
        superAttackDamage /= value;
    }

    public void IncreaseJumpForceOn(float time, float value)
    {
        StartCoroutine(IncreaseJumpForce(time, value));
    }

    IEnumerator IncreaseJumpForce(float time, float value)
    {
        cController.SetJumpForce(cController.GetJumpForce() + value);
        yield return new WaitForSeconds(time);
        cController.SetJumpForce(cController.GetJumpForce() - value);
    }

    public void GetDamage(int damage)
    {
        if (!godMode)
        {
            GameManager._instance.sound.PlayBumpShot();
            GetComponent<Health>().LoseHealth(damage);
            GodModeOn(hitGodModeDuration);
        }
    }
}
