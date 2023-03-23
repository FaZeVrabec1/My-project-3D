using UnityEngine;
using UnityEngine.UI;

public class VolumeText : MonoBehaviour
{
    public string volumeName;
    public string textIntro; //Sound or music
    private TMPro.TextMeshProUGUI txt;

    private void Awake()
    {
        txt = GetComponent<TMPro.TextMeshProUGUI>();
    }


    private void Update()
    {
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        float volumeValue = PlayerPrefs.GetFloat(volumeName) * 100;
        txt.text = textIntro + volumeValue.ToString() + "%";
    }


}
