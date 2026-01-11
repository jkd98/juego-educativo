using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    public GameObject optionsPanel;
    public AudioSource btnSound;


    public void OptionsPanel()
    {
        Time.timeScale = 0f;
        optionsPanel.SetActive(true);
        PlaySound();
    }

    public void ReturnPanel()
    {
        Time.timeScale = 1f;
        optionsPanel.SetActive(false);
        PlaySound();
    }

    // Para abrir otras opciones en el futuro
    public void AnotherOptions()
    {
        //sound
        //graphics
        PlaySound();

    }

    public void GoMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        PlaySound();
    }

    public void QuitGame()
    {
        Application.Quit();
        PlaySound();
    }

    public void PlaySound()
    {
        btnSound.Play();
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Recarga la escena actual

    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Recarga la escena actual
    }
}
