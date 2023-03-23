using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    public AudioSource source;
    public AudioSource source2;
    public AudioClip clip;
    public AudioClip explode;
    public bool played;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //source2.PlayOneShot(clip);
            source.PlayOneShot(explode);
        }
    }


    public void playSound(AudioClip _clip)
    {
        source.PlayOneShot(explode);
        played = true;
    }

}
