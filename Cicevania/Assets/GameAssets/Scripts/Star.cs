using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField]
    private float godModeTime;
    [SerializeField]
    float detectDist;
    [SerializeField]
    private List<Color> colors = new List<Color>();
    [SerializeField]
    float forceJump;
    private int colorIndex = 0;
    private SpriteRenderer spriteRenderer;
    private bool canChangeColor = true;
    bool isActive = false;
    Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.isKinematic = true;
    }

    private void Update()
    {
        if (canChangeColor)
        {
            canChangeColor = false;
            StartCoroutine(ChangeOfColor());
            if (colorIndex == colors.Count - 1) colorIndex = 0;
            else colorIndex++;
        }

        if (!isActive)
        {
            if (detectDist < Vector2.Distance(this.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position))
            {
                isActive = true;
                rb.isKinematic = false;
                if (Random.Range(0,1) == 0)
                {
                    rb.AddForce(new Vector2(1,1) * forceJump, ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(new Vector2(-1, 1) * forceJump, ForceMode2D.Impulse);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().GodModeOn(godModeTime);
            Destroy(this.gameObject);
        }
    }

    IEnumerator ChangeOfColor()
    {
        spriteRenderer.color = colors[colorIndex];
        yield return new WaitForSeconds(0.7f);
        canChangeColor = true;
    }
}
