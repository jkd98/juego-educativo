using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    //Referencias a las vidas
    public GameObject[] hearts;
    private int life;
    //Guardar posición del jugador para saber si uso el checkpoint
    private float checkpointPositionX, checkpointPositionY; // Para guardar player prefs
    public Animator animator;

    void Start()
    {
        life = hearts.Length; //Inicializa las vidas según la cantidad de corazones

        //Detecta si ya se ha guardado una posición de checkpoint
        if (PlayerPrefs.GetFloat("checkPointPositionX") != 0)
        {
            checkpointPositionX = PlayerPrefs.GetFloat("checkPointPositionX");
            checkpointPositionY = PlayerPrefs.GetFloat("checkPointPositionY");
            transform.position = new Vector2(checkpointPositionX, checkpointPositionY); //Al dar play se mueve a la posición del checkpoint guardada
        }
    }

    private void CheckLife()
    {
        if (life < 1)
        {
            //morir
            animator.Play("Hit");//Animación de daño
            hearts[0].SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Recarga la escena actual
        }
        else if (life < 2)
        {
            //Destrir un corazon
            animator.Play("Hit");//Animación de daño
            hearts[1].SetActive(false);
        }
        else if (life < 3)
        {
            // Destrir primer corazon
            //Destroy(hearts[2]);
            animator.Play("Hit");//Animación de daño
            hearts[2].SetActive(false);
        }

    }

    public void ReachedCheckpoint(float x, float y)
    {
        PlayerPrefs.SetFloat("checkPointPositionX", x);
        PlayerPrefs.SetFloat("checkPointPositionY", y);
    }

    public void PlayerDamage()
    {
        life--;
        CheckLife();
    }
}
