using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    public TextMeshProUGUI startText;
    public TextMeshProUGUI levelText;
    public string levelName;
    public int levelNum;
    private bool inDoor = false;

    // Para el tiempo en puertas
    private float doorTime = 3f;
    private float startTime = 3f;

    private int nivelAlcanzado;

    void Start()
    {
        // Obtenemos el nivel guardado. Si no hay nada, por defecto es el nivel 1 (índice de escena)
        nivelAlcanzado = PlayerPrefs.GetInt("NivelAlcanzado", 1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (nivelAlcanzado < levelNum)
            {
                levelText.gameObject.SetActive(true);
            }
            else
            {
                levelText.gameObject.SetActive(false);
                startText.gameObject.SetActive(true);
                inDoor = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            levelText.gameObject.SetActive(false);
            startText.gameObject.SetActive(false);
            inDoor = false;
            doorTime = startTime;
        }
    }

    void Update()
    {
        if (inDoor)
        {
            doorTime -= Time.deltaTime;
        }

        if (doorTime <= 0)
        {
            SceneManager.LoadScene(levelName);
        }

        if (inDoor && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Cargando nivel: " + levelName);
            SceneManager.LoadScene(levelName);
        }
    }

}
