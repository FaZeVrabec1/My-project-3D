using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement settings")]
    public float speed;
    public float jump;
    public CharacterController controller;
    public AudioClip jumping;
    public int ExtraJumps;
    private int jumpCounter;


    [Header("Coyote Time")]
    public float coyoteTime; 
    private float coyoteCounter;

    [Header("Movement & Gravity")]
    public Vector3 moveDirection;
    public float gravityScale;

    [Header("Animator")]
    public Animator anim;

    private float horizontalInput;

    [Header("Knockback Settings")]
    public float knockbackForce;
    public float knockbackTime;
    private float knockbackCounter;

    [Header("Misc Settings")]
    public bool drawGizmos;
    public LayerMask groundLayer;
    public Transform PT;
    public Transform TeleportPos;


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
                jumpCounter = ExtraJumps;
                coyoteCounter = coyoteTime;
            }
            else
            {
                coyoteCounter -= Time.deltaTime;
            }


            if (Input.GetButtonDown("Jump"))
            {
                if (controller.isGrounded)
                {
                    
                    moveDirection.y = jump;
                    SoundManager.instance.PlaySound(jumping);
                }
                else
                {
                    if (coyoteCounter > 0)
                    {
                        moveDirection.y = jump;
                        SoundManager.instance.PlaySound(jumping);
                    }
                    else
                    {
                        if (jumpCounter > 0)
                        {
                            moveDirection.y = jump;
                            SoundManager.instance.PlaySound(jumping);
                            jumpCounter--;
                        }
                    }
                }
                coyoteCounter = 0;
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

    }

    public void Knockback(Vector3 direction)
    {
        knockbackCounter = knockbackTime;

        moveDirection = direction * knockbackForce;

    }

    public void SetPlayerPosition(Vector3 position)
    {
        PT.position = position;
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            if (controller.isGrounded)
            {
                Debug.Log("Grounded");
            }
            else
            {
                Debug.Log("isn't grounded");
            }
        }
    }

}
