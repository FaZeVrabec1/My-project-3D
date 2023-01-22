using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int damageToGive = 1;
    public bool knockback;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 hitDirection = other.transform.position - transform.position;
            hitDirection.z = 0;
            hitDirection = hitDirection.normalized;

            if (knockback)
            {
                FindObjectOfType<HealthManager>().HurtPlayerAndKnockback(damageToGive, hitDirection);
            }
            else
            {
                FindObjectOfType<HealthManager>().HurtPlayer(damageToGive);
            }

            
            
        }
    }



}
