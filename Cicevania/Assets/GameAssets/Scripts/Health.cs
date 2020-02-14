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
                GameManager._instance.sound.PlayEnemyKilledShot();
                GetComponent<SpriteRenderer>().flipY = true;
                GetComponent<Collider2D>().enabled = false;
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * 50);
                GameManager._instance.enemyAmmount++;
                Destroy(this.gameObject, 3);
            }
            else if (this.CompareTag("Player"))
            {
                Missile[] missiles = FindObjectsOfType<Missile>();
                foreach(Missile m in missiles)
                {
                    Destroy(m.gameObject);
                }
                LoseLife();
            }
        }
        if(canvas!=null) canvas.UpdateHealthBar(currentHealth, maxHealth);
    }

    
    void LoseLife()
    {
        GameManager._instance.SetCurrentLifes(GameManager._instance.GetCurrentLifes() - 1);
        this.gameObject.GetComponent<Animator>().SetTrigger("Die");
        if (GameManager._instance.GetCurrentLifes() > 0)
        {
            StartCoroutine(WaitForRespawn());
        }
        else
        {
            PlayerDeath();
            StartCoroutine(WaitForLoadGameOver());
        }
    }

    public int GetMaxHealth() { return maxHealth; }

    public int GetCurrentHealth() { return currentHealth; }


    private void PlayerDeath()
    {
        GameManager._instance.sound.StopMusic();
        GameManager._instance.sound.PlayPeachDiesShot();
        this.gameObject.GetComponent<PlayerController>().GodModeOn(2.5f);
        this.gameObject.GetComponent<PlayerController>().enabled = false;
    }

    IEnumerator WaitForRespawn()
    {
        PlayerDeath();
        yield return new WaitForSeconds(2.5f);
        this.gameObject.GetComponent<PlayerController>().enabled = true;
        FindObjectOfType<CheckPointController>().Respawn();
        canvas.UpdateLifesAmmount(GameManager._instance.GetCurrentLifes());
        canvas.UpdateCoinAmmount(GameManager._instance.coinAmmount);
        canvas.UpdateHealthBar(currentHealth, maxHealth);
    }

    IEnumerator WaitForLoadGameOver()
    {
        yield return new WaitForSeconds(1.7f);
        this.gameObject.GetComponent<PlayerController>().enabled = true;
        GameManager._instance.sceneC.LoadGameOver();
    }
}
