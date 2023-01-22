using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;
    public CharacterController controller;

    public Vector3 moveDirection;
    public float gravityScale;

    public Animator anim;

    private float horizontalInput;

    public float knockbackForce;
    public float knockbackTime;
    private float knockbackCounter;

    public AudioClip jumping;

    void Start()
    {
        //Grabs references for rigidbody and animator from game object.
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        float yStore = moveDirection.y;
        if (knockbackCounter <= 0)
        {

            //can move on the z axis
            //moveDirection = (transform.right * Input.GetAxis("Vertical") * -1) + (transform.forward * Input.GetAxis("Horizontal"));

            //can only move on the x,y axis
            moveDirection = (transform.right * 0) + (transform.forward * Input.GetAxis("Horizontal"));

            moveDirection = moveDirection.normalized * speed;
            moveDirection.y = yStore;

            if (controller.isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jump;
                    SoundManager.instance.PlaySound(jumping);
                }

            }

            

        }
        else
        {
            knockbackCounter -= Time.deltaTime;
        }

        //Flip player when facing left/right. (messes with setting player position)

        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(1, 1, -1);

        //Gravity Control
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);

        //Moving The Player with CharecterController
        controller.Move(moveDirection * Time.deltaTime);


        anim.SetBool("isGrounded", controller.isGrounded);
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Horizontal"))));


        //Detect LMB (Will be used later for attacking)
        if (Input.GetMouseButton(0))
        {

            //transform.position = new Vector3(1f, 1.5f, 1f);
            //SetPlayerPosition(new Vector3(1f, 1.5f, 1f));

            

            //moveDirection = new Vector3(0f, 10f, 0f);
        }

    }

    public void Knockback(Vector3 direction)
    {
        knockbackCounter = knockbackTime;

        moveDirection = direction * knockbackForce;

    }

    public void SetPlayerPosition(Vector3 position)
    {
        transform.position = new Vector3(1f, 1.5f, 1f);
    }


    


}
