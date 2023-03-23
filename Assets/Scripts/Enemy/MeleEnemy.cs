using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemy : MonoBehaviour
{
    [Header ("Attack settings")]
    public float attackcolldown;
    public float range;
    public int damage;
    private bool SeePlayer;
    public bool DrawRange;

    [Header("Collider settings")]
    public BoxCollider enemyCollider;

    private float cooldownTimer = Mathf.Infinity;

    [Header("Attack Sound")]
    public AudioClip attackSound;

    private Animator anim;
    private Health playerHealth;
    private Health enemyHealth;
    private PlayerMovement player;
    public HealthManager HM;
    private EnemyPatrol enemyPatrol;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        enemyHealth = GetComponent<Health>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        
        Vector3 direction = new Vector3(transform.localScale.x,0,0);
        Ray theRay = new Ray(enemyCollider.bounds.center, transform.TransformDirection(direction * range));

        if (Physics.Raycast(theRay, out RaycastHit hit, range))
        {
            if (hit.collider.tag == "Player")
            {
               if (cooldownTimer >= attackcolldown && HM.currentHealth > 0)
                {
                    cooldownTimer = 0;
                    anim.SetTrigger("meleeAttack");
                    SoundManager.instance.PlaySound(attackSound);
                }
                SeePlayer = true;
            }
            else
            {
                SeePlayer = false;
            }
        }
        else
        {
            SeePlayer = false;
        }
        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !SeePlayer;
        }

    }

    private void OnDrawGizmos()
    {
        if (DrawRange)
        {
            Vector3 direction = new Vector3(transform.localScale.x, 0, 0);
            Gizmos.DrawRay(enemyCollider.bounds.center, transform.TransformDirection(direction * range));
        }
    }

    private void DamagePLayer()
    {
        if (SeePlayer)
        {
            HM.HurtPlayer(damage);
        }
    }

}
