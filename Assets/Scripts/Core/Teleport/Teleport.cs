using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [Header("Teleport Settings")]
    public bool DrawRange;
    public float DetectionRange;
    public BoxCollider You;
    public Transform TeleportPos;
    public AudioClip teleporting;
    public PlayerMovement thePlayer;
    public Transform PT;

    [Header("Rooms Settings")]
    public GameObject oldRoom;
    public GameObject newRoom;
    public bool SpawnPoint;
    public RangedEnemy[] RangedEnemys;


    private void Update()
    {
        Vector3 direction = new Vector3(0, 1, 0);
        Ray theRay = new Ray(You.bounds.center, transform.TransformDirection(direction * DetectionRange));
            if (Physics.Raycast(theRay, out RaycastHit hit, DetectionRange))
            {
                if (hit.collider.tag == "Player")
                {
                StartCoroutine(TeleportCo());
                }
            }
            
        
    }

    private void OnDrawGizmos()
    {
        if (DrawRange)
        {
            Vector3 direction = new Vector3(0, 1, 0);
            Gizmos.DrawRay(You.bounds.center, transform.TransformDirection(direction * DetectionRange));
        }
    }

    IEnumerator TeleportCo()
    {
        //Activate new objects
        newRoom.SetActive(true);

        SoundManager.instance.PlaySound(teleporting);
        thePlayer.enabled = false;
        PT.position = TeleportPos.position;
        thePlayer.gameObject.SetActive(true);

        //Makes setting the position work
        thePlayer.enabled = false;
        yield return new WaitForSeconds(0.1f);
        thePlayer.enabled = true;

        //reset and velocity
        thePlayer.moveDirection = new Vector3(0, 0, 0);

        //Disable previus objects
        oldRoom.SetActive(false);
        //There is a bit of a delay with the activation so make sure to put the teleportPos in the air

    }

}
