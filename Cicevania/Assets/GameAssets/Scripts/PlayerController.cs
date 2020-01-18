using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private int jumpDamage;
    [SerializeField]
    private float hitGodModeDuration;

    Character2DController cController;
    Animator anim;
    Rigidbody2D rb;
    float horizontal;
    bool isJumping;
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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isJumping = true;
        }

        if (godMode) Blink();
        else ChangePlayerAlpha(1);
    }

    private void FixedUpdate()
    {
        cController.Move(horizontal * Time.deltaTime * moveSpeed, false, isJumping);
    }

    public void OnGround()
    {
        isJumping = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            if (JumpAttack(collision.transform.position))
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
