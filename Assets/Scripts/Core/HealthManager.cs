using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth;
    public float currentHealth;

    [Header("Set Player")]
    public PlayerMovement thePlayer;
    public Transform PT;
    public Renderer playerRenderer;

    [Header("Invincibility Settings")]
    public float invincibilityLenghth;
    private float invincibilityCounter;
    private float flashCounter;
    public float flashLenght = 0.1f;

    [Header("Death & Respawn Settings")]
    public GameObject DeathEffect;
    private bool isRespawning;
    public Transform startRespawn;
    public float RespawnLenghth;
    public Image DeathScreen;
    private bool isFadeToBlack;
    private bool isFadeFromBlack;
    public float fadeSpeed;
    public float waitForFade;
    public Transform respawnPoint;

    [Header("Sounds and Effects")]
    public AudioClip Hurt;
    public AudioClip Dead;
    public AudioClip Heal;


    
    private int Pdamage;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        thePlayer = FindObjectOfType<PlayerMovement>();
        respawnPoint = startRespawn;
       // respawnPoint = thePlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        //Invincibility
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
            flashCounter -= Time.deltaTime;

            if (flashCounter <= 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashLenght;
            }

            if (invincibilityCounter <= 0)
            {
                playerRenderer.enabled = true;
            }

        }

        //Fading screen
        if (isFadeToBlack)
        {
            DeathScreen.color = new Color(DeathScreen.color.r, DeathScreen.color.g, DeathScreen.color.b, Mathf.MoveTowards(DeathScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (DeathScreen.color.a == 1f)
            {
                isFadeToBlack = false;
            }
        }

        if (isFadeFromBlack)
        {
            DeathScreen.color = new Color(DeathScreen.color.r, DeathScreen.color.g, DeathScreen.color.b, Mathf.MoveTowards(DeathScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (DeathScreen.color.a == 0f)
            {
                isFadeFromBlack = false;
            }
        }

    }

    public void HurtPlayer(int damage)
    {
        //if invincibility dont do damage
        if (invincibilityCounter <= 0)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
               Respawn();
            }
            else
            {
                SoundManager.instance.PlaySound(Hurt);
                invincibilityCounter = invincibilityLenghth;

                playerRenderer.enabled = false;
                flashCounter = flashLenght;
            }
        }
        

    }

    public void HurtPlayerAndKnockback(int damage, Vector3 direction)
    {
        //if invincibility dont do damage
        if (invincibilityCounter <= 0)
        {
            Pdamage = damage;
            currentHealth -= Pdamage;

            if (currentHealth <= 0)
            {
                
                Respawn();
                
            }
            else
            {
                SoundManager.instance.PlaySound(Hurt);
                thePlayer.Knockback(direction);

                invincibilityCounter = invincibilityLenghth;

                playerRenderer.enabled = false;
                flashCounter = flashLenght;
            }
        }
    }

    public void HealPlayer(int heal)
    {
        SoundManager.instance.PlaySound(Heal);
        currentHealth += heal;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Respawn()
    {

        if (!isRespawning)
        {
            StartCoroutine(RespawnCo());
        }
    }


   

    IEnumerator RespawnCo()
    {
        //Set isRespawning to true
        isRespawning = true;
        SoundManager.instance.PlaySound(Dead);

        //Make the player invisible and wait before moving him and fade screen to black
        thePlayer.gameObject.SetActive(false);
        Instantiate(DeathEffect, thePlayer.transform.position, thePlayer.transform.rotation);

        yield return new WaitForSeconds(RespawnLenghth);

        isFadeToBlack = true;

        yield return new WaitForSeconds(waitForFade);

        isFadeToBlack = false;
        isFadeFromBlack = true;

        PT.position = respawnPoint.position;
        thePlayer.gameObject.SetActive(true);
        isRespawning = false;
        
        //Makes setting the position work
        thePlayer.enabled = false;
        yield return new WaitForSeconds(0.1f);
        thePlayer.enabled = true;

        //reset health and velocity
        currentHealth = maxHealth;
        thePlayer.moveDirection = new Vector3(0, 0, 0);

        //Give player invincibility after respawning
        invincibilityCounter = invincibilityLenghth;
        playerRenderer.enabled = false;
        flashCounter = flashLenght;





        
    }

}
