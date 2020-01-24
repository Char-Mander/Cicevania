using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    Color activeColor;
    [SerializeField]
    Color inactiveColor;
    bool isActive;
    CheckPointController cpController;

    private void Start()
    {
        cpController = FindObjectOfType<CheckPointController>();
    }


    public void SetAsCurrentCP(bool value)
    {
        isActive = value;
        if (isActive)
        {
            SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer sp in sprites)
            {
                sp.color = activeColor;
            }
        }
        else
        {
            SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sp in sprites)
            {
                sp.color = inactiveColor;
            }
        }
    }

    public bool IsActive() { return isActive; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActive)
        {
            cpController.ActiveCP(this);
        }
    }

    public Transform GetSpawnPoint() { return spawnPoint; }
}
