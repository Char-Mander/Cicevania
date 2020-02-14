using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurpriseBox : MonoBehaviour
{
    [SerializeField]
    List<GameObject> posibleObjects;
    [SerializeField]
    Transform objectPos;
    [SerializeField]
    Sprite usedImage;

    Animator anim;
    bool enabled = true;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enabled)
        {
            enabled = false;
            anim.enabled = false;
            GameManager._instance.sound.PlayPowerUpAppearShot();
            GetComponent<SpriteRenderer>().sprite = usedImage;
            GameObject go = Instantiate(posibleObjects[Random.Range(0, posibleObjects.Count)], objectPos);
            if(go.GetComponent<Rigidbody2D>() != null) go.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 2 * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
}
