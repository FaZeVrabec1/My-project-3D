using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickUp : MonoBehaviour
{
    public int value;
    public GameObject pickUpEffect;
    public AudioClip CashMoney;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SoundManager.instance.PlaySound(CashMoney);
            FindObjectOfType<GameManager>().AddGold(value);
            Instantiate(pickUpEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
