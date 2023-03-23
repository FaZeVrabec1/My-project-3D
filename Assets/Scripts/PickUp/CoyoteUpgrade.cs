using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoyoteUpgrade : MonoBehaviour
{

    public float value;
    public GameObject pickUpEffect;
    public AudioClip Boost;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SoundManager.instance.PlaySound(Boost);
            FindObjectOfType<PlayerMovement>().coyoteTime += value;
            Instantiate(pickUpEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}
