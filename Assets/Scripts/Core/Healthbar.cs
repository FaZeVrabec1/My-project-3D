using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public HealthManager playerHealth;
    public Image totalhealthbar;
    public Image currenthealthbar;

    // Update is called once per frame
    void Update()
    {
        totalhealthbar.fillAmount = playerHealth.maxHealth / 10;
        currenthealthbar.fillAmount = playerHealth.currentHealth / 10;
    }
}
