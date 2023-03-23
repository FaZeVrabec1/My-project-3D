using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource Soundsource;
    private AudioSource MusicSource;
    

    private void Awake()
    {

        Soundsource = GetComponent<AudioSource>();
        MusicSource = transform.GetChild(0).GetComponent<AudioSource>();

        //Keep this object when we go to new scene
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        //Destroy duplicate gameobjects
        else if (instance != null && instance != this)
            Destroy(gameObject);

        //Load settings
        ChangeSoundVolume(0);
        ChangeMusicVolume(0);

    }
    public void PlaySound(AudioClip _sound)
    {
        Soundsource.PlayOneShot(_sound);
    }

    public void ChangeSoundVolume(float change)
    {
        ChangeSourceVolume(1, "soundVolume", change, Soundsource);
    }

    public void ChangeMusicVolume(float change)
    {
        ChangeSourceVolume(0.3f, "musicVolume", change, MusicSource);
    }

    private void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource source)
    {
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
        currentVolume += change;
   
        if (currentVolume > 1)
        {
            currentVolume = 0;
        }
        else if (currentVolume < 0)
        {
            currentVolume = 1;
        }

        float finalVolume = currentVolume * baseVolume;
        source.volume = finalVolume;
        PlayerPrefs.SetFloat(volumeName, currentVolume); //Save volume settings


    }
}