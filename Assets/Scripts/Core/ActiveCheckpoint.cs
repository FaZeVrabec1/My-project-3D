using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCheckpoint : MonoBehaviour
{
    
    public Material respawnNotSet;
    Renderer rend;
    public AudioClip Checksound;


    public GameObject[] Checkpoints;

    //Reset all checkpoints
    public void Purge()
    {
        for (int i = 0; i < Checkpoints.Length; i++)
        {
            SoundManager.instance.PlaySound(Checksound);
            rend = Checkpoints[i].GetComponent<Renderer>();
            rend.sharedMaterial = respawnNotSet;
        }
    }


}
