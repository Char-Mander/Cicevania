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
    private float hitGodModeDuration;

    Character2DController cController;
    Animator anim;
    Rigidbody2D rb;
    float horizontal;
    bool isJumping;
    bool canHit = true;
    bool isCrouching = false;
    bool canJumpOrHit = true;
    bool godMode;
    // Start is called before the first frame update
    void Start()
    {
        cController = GetComponent<Character2DController>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
            isJumping = true;
        }

        //TODO Ataque
        /*if(Input.GetKey(KeyCode.R) && canJumpOrHit)
        {
            if (canJumpOrHit)
            {
                canHit = false;
                Hit();
            }
            anim.SetLayerWeight(1, 1);
        }
        else
        {
            anim.SetLayerWeight(1, 0);
        }*/

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

    //TODO Función en la que ataca con el paraguas
    private void Hit()
    {
        
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

    public void GetDamage(int damage)
    {
        if (!godMode)
        {
            GetComponent<Health>().LoseHealth(damage);
            GodModeOn(hitGodModeDuration);
        }
    }
}
