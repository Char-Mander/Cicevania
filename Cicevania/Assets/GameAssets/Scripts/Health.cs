﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private CharacterCanvasController canvas;
    [SerializeField]
    private int maxHealth;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void GainHealth(int ammount)
    {
        currentHealth += ammount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        canvas.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void LoseHealth(int ammount)
    {
        currentHealth -= ammount;
        if (currentHealth <= 0)
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Die");
            if (this.CompareTag("Enemy")){
                GetComponent<SpriteRenderer>().flipY = true;
                GetComponent<Collider2D>().enabled = false;
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * 50);
                Destroy(this.gameObject, 3);
            }
            Destroy(this.gameObject, 2);
        }
        canvas.UpdateHealthBar(currentHealth, maxHealth);
    }

    public int GetMaxHealth() { return maxHealth; }

    public int GetCurrentHealth() { return currentHealth; }
}
