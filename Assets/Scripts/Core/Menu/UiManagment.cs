using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManagment : MonoBehaviour
{

    public GameObject pauseScreen;

    private void Awake()
    {
        pauseScreen.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeInHierarchy)
            {
                PauseGame(false);
            }
            else
            {
                PauseGame(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Time.timeScale = 0.5f;
        }

    }


    


    #region Pause
    private void PauseGame(bool status)
    {
        //if true pause
        pauseScreen.SetActive(status);

        if (status)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        

    }
    


    public void Continue()
    {
        PauseGame(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    //Activate game over screen
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();//only works in build

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Only works in editor
        #endif
    }


    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }
    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }


    #endregion
}
