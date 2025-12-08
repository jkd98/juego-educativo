using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // NECESARIO para usar TextMeshPro

public class FruitManager : MonoBehaviour
{
    public TextMeshProUGUI fruitText; // Texto UI para mostrar las frutas recogidas
    public GameObject transitionLevel;
    public TextMeshProUGUI totalFruitsText; // Texto UI para mostrar el total de frutas (si es necesario)
    public TextMeshProUGUI fruitsCollected; // Texto UI para mostrar las frutas recogidas (contador)

    // Índice al que se regresará al terminar todos los niveles (generalmente el menú principal)
    private const int MENU_SCENE_INDEX = 0; 

    void Start()
    {
        // Esto muestra el total inicial de frutas en el nivel (los hijos de este GameObject)
        totalFruitsText.text = transform.childCount.ToString();
    }

    private void Update()
    {
        // Actualiza el contador de frutas recogidas en tiempo real
        fruitsCollected.text = transform.childCount.ToString();
        
        AllFruitsCollected();
    }

    // Preguntar si quedan frutas
    public void AllFruitsCollected()
    {
        // Comprueba si el número de hijos (frutas) es 0
        if (transform.childCount == 0)
        {
            Debug.Log("Todas las frutas han sido recogidas.");

            fruitText.gameObject.SetActive(true); // Activa el texto de frutas recogidas
            transitionLevel.SetActive(true); // Activa la transición de nivel
            
            // Llama a ChangeScene después de 2 segundos.
            // Es crucial que esta lógica se ejecute solo una vez.
            if (!IsInvoking("ChangeScene"))
            {
                Invoke("ChangeScene", 2f); 
            }
        }
    }

    public void ChangeScene()
    {
        // 1. Obtener el índice de la escena actual.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        // 2. Calcular el índice de la siguiente escena.
        int nextSceneIndex = currentSceneIndex + 1;
        
        // 3. Obtener el total de escenas en las Build Settings.
        int totalScenesInBuild = SceneManager.sceneCountInBuildSettings;

        // 4. Comprobar si el siguiente índice es válido (existe un siguiente nivel).
        if (nextSceneIndex < totalScenesInBuild)
        {
            // Hay un siguiente nivel. Cárgalo.
            Debug.Log("Cargando el siguiente nivel: Índice " + nextSceneIndex);
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // Ya no hay más niveles después de este. Regresar al menú.
            Debug.Log("¡Fin de todos los niveles! Regresando al Menú Principal (Índice " + MENU_SCENE_INDEX + ")");
            SceneManager.LoadScene(MENU_SCENE_INDEX);
            
            // NOTA: Si tu menú no es el índice 0, reemplaza MENU_SCENE_INDEX 
            // con el número de índice de tu escena de menú o usa:
            // SceneManager.LoadScene("NombreDeTuEscenaDeMenu");
        }
    }
}