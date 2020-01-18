using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EnemyMovement()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public int GetDamage() { return damage; }
}
