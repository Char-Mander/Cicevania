using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField]
    private float godModeTime;
    [SerializeField]
    private List<Color> colors = new List<Color>();
    private int colorIndex = 0;
    private SpriteRenderer spriteRenderer;
    private bool canChangeColor = true;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
