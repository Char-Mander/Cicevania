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
            if (this.CompareTag("Enemy")){
                this.gameObject.GetComponent<Animator>().SetTrigger("Die");
                GetComponent<SpriteRenderer>().flipY = true;
                GetComponent<Collider2D>().enabled = false;
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * 50);
                Destroy(this.gameObject, 3);
            }
            else if (this.CompareTag("Player"))
            {
                LoseLife();
            }
        }
        if(canvas!=null) canvas.UpdateHealthBar(currentHealth, maxHealth);
    }

    void LoseLife()
    {
        if (GameManager._instance.GetCurrentLifes() > 1)
        {
            GameManager._instance.SetCurrentLifes(GameManager._instance.GetCurrentLifes() - 1);
            GameManager._instance.ResetValues(false, false, true);
            FindObjectOfType<CheckPointController>().Respawn();
        }
        else
        {
            GameManager._instance.sceneC.LoadGameOver();
            //Resetear en GameOver los valores
        }
    }

    public int GetMaxHealth() { return maxHealth; }

    public int GetCurrentHealth() { return currentHealth; }
}
