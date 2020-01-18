using System.Collections;
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
            print(this.gameObject.name + " has died");
            this.gameObject.GetComponent<Animator>().SetTrigger("Die");
            /*if (!this.gameObject.CompareTag("Player")){
                GetComponent<SpriteRenderer>().flipY = true;
                this.gameObject.GetComponent<CircleCollider2D>.enabled = false;
                this.gameObject.GetComponent<RigidBody2D>.addForce(Vector2.up, 150);
            }*/
            Destroy(this.gameObject, 3);
        }
        canvas.UpdateHealthBar(currentHealth, maxHealth);
    }

    public int GetMaxHealth() { return maxHealth; }

    public int GetCurrentHealth() { return currentHealth; }
}
