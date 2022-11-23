using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Unique GameManager
    private static GameManager gameManagerInstance;
    AudioSource audioSource;
    [SerializeField] AudioClip menuAudio;
    [SerializeField] AudioClip[] mainAudio, midAudio, endAudio, overAudio;

    void Awake()
    {
        DontDestroyOnLoad(this);
        audioSource = this.GetComponent<AudioSource>();
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MenuToMain()
    {
        audioSource.Stop();
        SceneManager.LoadScene("Main");
    }

    public void MainToMenu()
    {
        audioSource.Stop();
        SceneManager.LoadScene("Menu");
        audioSource.clip = menuAudio;
        audioSource.Play();
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void AudioON(int type, int number) //Main, Mid, End, Over
    {
        if (type == 0)
            audioSource.clip = mainAudio[number];
        else if (type == 1)
            audioSource.clip = midAudio[number];
        else if (type == 2) 
            audioSource.clip = endAudio[number];
        else
            audioSource.clip = overAudio[number];

        audioSource.Play();
    }
}
