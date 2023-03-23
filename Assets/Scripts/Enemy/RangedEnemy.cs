using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack settings")]
    public float attackcolldown;
    public float range;
    public float lifetime;
    public int damage;
    public bool SeePlayer;
    public bool DrawRange;
    public Transform firePoint;
    public GameObject[] Enemyfireball;

    [Header("Collider settings")]
    public BoxCollider enemyCollider;

    private float cooldownTimer = Mathf.Infinity;

    [Header("Attack Sound")]
    public AudioClip attackSound;

    private Animator anim;
    private Health playerHealth;
    private Health enemyHealth;
    public RangedEnemy enemy;
    public HealthManager HM;
    private EnemyPatrol enemyPatrol;


    private void Awake()
    {
        SetLifetime(lifetime);
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        enemyHealth = GetComponent<Health>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        Vector3 direction = new Vector3(transform.localScale.x, 0, 0);
        Ray theRay = new Ray(enemyCollider.bounds.center, transform.TransformDirection(direction * range));

        if (Physics.Raycast(theRay, out RaycastHit hit, range))
        {
            if (hit.collider.tag == "Player")
            {
                if (cooldownTimer >= attackcolldown && HM.currentHealth > 0)
                {
                    cooldownTimer = 0;
                    anim.SetTrigger("RangedAttack");
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

    private void RangedAttack()
    {
        Enemyfireball[FindFireball()].transform.position = firePoint.position;
        Enemyfireball[FindFireball()].GetComponent<EnemyProjectile>().SetDirection(Mathf.Sign(enemy.transform.localScale.x));
    }
    private int FindFireball()
    {
        for (int i = 0; i < Enemyfireball.Length; i++)
        {
            if (!Enemyfireball[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void SetLifetime(float lifetime)
    {
        for (int i = 0; i < Enemyfireball.Length; i++)
        {
            Enemyfireball[i].SetActive(true);
            FindObjectOfType<EnemyProjectile>().lifetime = lifetime;
            Enemyfireball[i].SetActive(false);
        }
    }

    public void DisableFireballs()
    {
        for (int i = 0; i < Enemyfireball.Length; i++)
        {
            Enemyfireball[i].SetActive(false);
        }
    }

}
