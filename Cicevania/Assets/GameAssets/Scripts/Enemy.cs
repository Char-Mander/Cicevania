using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Transform target;
    [HideInInspector]
    public Animator anim;
    public Rigidbody2D rb;
    public float detectDist;
    public float moveSpeed;
    public LayerMask lm;
    [HideInInspector]
    public Vector2 vecToTarget;
    [SerializeField]
    private int damage;
    [HideInInspector]
    public float horizontal = -1;
    [SerializeField]
    private float hitGodModeDuration;

    bool noCollision = true;
    bool godMode = false;

    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public virtual void Update()
    {
        DetectTarget(target);
        Movement();
        if (godMode) Blink();
    }

    public virtual void Movement()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime * horizontal;
        transform.localScale = new Vector3(horizontal, transform.localScale.y, transform.localScale.z);
    }

    public virtual void DetectTarget(Transform target)
    {
        vecToTarget = new Vector3(target.position.x, target.position.y + 0.5f, target.position.z) - transform.position;
        if (vecToTarget.magnitude < detectDist)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, vecToTarget.normalized, detectDist, lm);
            if(hit.collider != null && hit.collider.CompareTag("Player"))
            {
                Attack();
            }
        }
    }

    public virtual void Attack()
    {
        print("Enemy attacks");
    }

    public int GetDamage() { return damage; }

    public void GetDamage(int damage)
    {
        if (!godMode)
        {
            GameManager._instance.sound.PlayBumpShot();
            GetComponent<Health>().LoseHealth(damage);
            StartCoroutine(ActiveGodMode(hitGodModeDuration));
        }
    }

    void Blink()
    {
        ChangeEnemyAlpha(Mathf.PingPong(Time.time * 5, 1) + 0.5f);
    }

    void ChangeEnemyAlpha(float value)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color auxColor = sr.color;
        auxColor.a = value;
        sr.color = auxColor;
    }

    IEnumerator ActiveGodMode(float duration)
    {
        godMode = true;
        yield return new WaitForSeconds(duration);
        godMode = false;
    }
}
