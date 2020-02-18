using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMechanism : MonoBehaviour
{
    [SerializeField]
    GameObject camera;
    [SerializeField]
    List<GameObject> blockers;

    Animator tazaAnim;
    bool triggered = false;

    private void Start()
    {
        tazaAnim = GetComponentInChildren<Animator>();
        EnableOrDisableElements(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !triggered)
        {
            triggered = true;
            EnableOrDisableElements(true);
        }
    }

    public void EnableOrDisableElements(bool value)
    {
        foreach (GameObject block in blockers)
            block.SetActive(value);
        tazaAnim.SetBool("Detected", value);
        camera.SetActive(value);
    }

    public void Reset()
    {
        triggered = false;
        EnableOrDisableElements(false);
    }
}
