using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public HealthManager playerHealth;
    public Image totalhealthbar;
    public Image currenthealthbar;

    // Start is called before the first frame update
    void Start()
    {
        totalhealthbar.fillAmount = playerHealth.maxHealth / 10;
    }

    // Update is called once per frame
    void Update()
    {
        currenthealthbar.fillAmount = playerHealth.currentHealth / 10;
    }
}
