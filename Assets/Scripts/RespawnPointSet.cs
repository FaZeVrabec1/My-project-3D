using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPointSet : MonoBehaviour
{
    public Transform respawnPoint;
    public GameObject checkpoint;
    public Material respawnNotSet;
    public Material respawnSet;
    Renderer rend;

    void Start()
    {
        rend = checkpoint.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = respawnNotSet;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //only run the command if the checkpoint is not set
            if (rend.sharedMaterial == respawnNotSet)
            {
                FindObjectOfType<HealthManager>().respawnPoint = transform;

                FindObjectOfType<ActiveCheckpoint>().Purge();

                rend.sharedMaterial = respawnSet;
            }
        }
    }
}
