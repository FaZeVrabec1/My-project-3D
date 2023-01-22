using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{

    public int value;
    public GameObject pickUpEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (FindObjectOfType<HealthManager>().currentHealth != FindObjectOfType<HealthManager>().maxHealth)
            {
                FindObjectOfType<HealthManager>().HealPlayer(value);

                Instantiate(pickUpEffect, transform.position, transform.rotation);

                Destroy(gameObject);
            }
            
        }
    }

}
