using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeFireball : MonoBehaviour
{

    public float faster;
    public int Stronger;
    public int Better;
    public GameObject pickUpEffect;
    public AudioClip Boost;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SoundManager.instance.PlaySound(Boost);
            FindObjectOfType<PlayerAttack>().UpgradeFireballs(faster, Stronger, Better);
            Instantiate(pickUpEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}
