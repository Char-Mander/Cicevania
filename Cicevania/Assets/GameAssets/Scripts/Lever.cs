using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField]
    GameObject infoImage;

    Animator animLever;
    [SerializeField]
    Animator animDoor;

    bool canUse = true;

    private void Start()
    {
        animLever = GetComponent<Animator>();
        infoImage.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (canUse) {
            if (col.tag == "Player")
            {
                infoImage.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    animLever.SetTrigger("Activate");
                    animDoor.SetTrigger("Activate");
                    infoImage.SetActive(false);
                    canUse = false;
                }
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            infoImage.SetActive(false);
        }
    }
}
