using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : MonoBehaviour
{

    public int value;
    public GameObject pickUpEffect;
    public AudioClip Heal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SoundManager.instance.PlaySound(Heal);
            Instantiate(pickUpEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            if (FindObjectOfType<HealthManager>().maxHealth < 10)
            {
                FindObjectOfType<HealthManager>().maxHealth += value;
                FindObjectOfType<HealthManager>().HealPlayer(value);
            }
        }
    }

}
