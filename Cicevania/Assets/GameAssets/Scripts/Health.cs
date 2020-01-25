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
        if (this.gameObject.CompareTag("Player")) canvas.UpdateLifesAmmount(GameManager._instance.GetCurrentLifes());
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
        GameManager._instance.SetCurrentLifes(GameManager._instance.GetCurrentLifes() - 1);
        if (GameManager._instance.GetCurrentLifes() > 0)
        {
            FindObjectOfType<CheckPointController>().Respawn();
            canvas.UpdateLifesAmmount(GameManager._instance.GetCurrentLifes());
            canvas.UpdateCoinAmmount(GameManager._instance.coinAmmount);
            canvas.UpdateHealthBar(currentHealth, maxHealth);
        }
        else
        {
            GameManager._instance.sceneC.LoadGameOver();
        }
    }

    public int GetMaxHealth() { return maxHealth; }

    public int GetCurrentHealth() { return currentHealth; }
}
