using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerAttack player;
    public float AttackColldown;
    public Transform firePoint;
    public GameObject[] fireballs;
    public AudioClip fireballSound;
    private bool dead;
    public HealthManager HM;

    public Animator anim;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        //anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (AttackColldown < 0.5f)
        {
            AttackColldown = 0.5f;
        }

        if (Input.GetMouseButton(0) && cooldownTimer > AttackColldown && !HM.dead)
        {
            cooldownTimer = 0;
            anim.SetTrigger("fireball");
        }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(player.transform.localScale.z));
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    public void UpgradeFireballs(float faster, int Stronger, int Better)
    {
        AttackColldown -= faster;
        for (int i = 0; i < fireballs.Length; i++)
        {
            fireballs[i].SetActive(true);
            FindObjectOfType<Projectile>().damage += Stronger;
            FindObjectOfType<Projectile>().speed += Better;
            fireballs[i].SetActive(false);
        } 
    }

}
