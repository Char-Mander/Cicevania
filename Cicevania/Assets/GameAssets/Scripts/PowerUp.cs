using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class EventPowerUps : UnityEvent { }


public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private string targetTag;
    public EventPowerUps OnPowerUpTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            OnPowerUpTrigger.Invoke();
            Destroy(this.gameObject);
        }
    }
}
