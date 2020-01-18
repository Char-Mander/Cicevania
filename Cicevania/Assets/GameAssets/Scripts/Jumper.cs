using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float reloadTime;

    RelativeJoint2D rj;
    bool canJump = true;
    // Start is called before the first frame update
    void Start()
    {
        rj = GetComponent<RelativeJoint2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (canJump)
            {
                if (GetComponent<Animator>()!= null) GetComponent<Animator>().SetTrigger("Jump");
                rj.maxForce = jumpForce;
                canJump = false;
                StartCoroutine(ReloadJumper());
            }
        }
    }

    IEnumerator ReloadJumper()
    {
        yield return new WaitForSeconds(reloadTime);
        canJump = true;
        rj.maxForce = 2;
    }
}
