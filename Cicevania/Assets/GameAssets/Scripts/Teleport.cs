using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    GameObject infoImage;
    [SerializeField]
    Transform destiny;
    bool canUse = true;

    private void Start()
    {
        infoImage.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (canUse)
        {
            if (col.tag == "Player")
            {
                infoImage.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) || (Input.GetKeyDown(KeyCode.DownArrow) && this.gameObject.CompareTag("Tube")))
                {
                    GameManager._instance.sound.PlayPipeShot();
                    col.gameObject.transform.position = new Vector3(destiny.position.x, destiny.position.y, destiny.position.z);
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
