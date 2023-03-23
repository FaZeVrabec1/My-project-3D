using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    public float StartingHealth;
    public float CurrentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("Assaing effects and points")]
    public GameObject DeathEffect;
    public MeleEnemy you;
    public Transform effectPoint;

    [Header("Death Sound")]
    public AudioClip deathSound;

    [Header("Hurt Sound")]
    public AudioClip hurtSound;

    private void Awake()
    {
        CurrentHealth = StartingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {

        CurrentHealth = Mathf.Clamp(CurrentHealth - _damage, 0, StartingHealth);

        if (CurrentHealth > 0)
        {
            //anim.SetTrigger("hurt");
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {
                //deactivate all attached components(movment'n stuff)
                
                gameObject.SetActive(false);
                Instantiate(DeathEffect, effectPoint.transform.position, effectPoint.transform.rotation);
                SoundManager.instance.PlaySound(deathSound);
                dead = true;
            }

        }

    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }



}
