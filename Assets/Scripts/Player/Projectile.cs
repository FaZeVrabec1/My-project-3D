using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public float speed;
    private float direction;
    private bool hit;
    private float lifetime;
    public GameObject ExplosionEffect;
    public AudioClip ExplosionSound;

    private Animator anim;
    private SphereCollider Collider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        Collider = GetComponent<SphereCollider>();
    }
    private void Update()
    {
        if (hit) return;

        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);


        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);


    }


    private void OnTriggerEnter(Collider other)
    {
        hit = true;
        Collider.enabled = false;
        SoundManager.instance.PlaySound(ExplosionSound);
        Instantiate(ExplosionEffect, transform.position, transform.rotation);
        Deactivate();
        if (other.tag == "enemy")
        {
            other.GetComponent<Health>().TakeDamage(damage);
        }

    }
    public void SetDirection(float _direction)
    {
        lifetime = 0; //reset lifetime
        direction = _direction; //get the direction where the player is facing
        gameObject.SetActive(true); //use fireball and activate it (make it visible)
        hit = false; //hasn't hit anything
        Collider.enabled = true; //enable collider

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}