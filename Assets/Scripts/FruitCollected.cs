using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCollected : MonoBehaviour
{

    // Cuando algo entre en contacto con el colider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Desactivar el sprite renderer de la fruta
            GetComponent<SpriteRenderer>().enabled = false;
            //Desactivar el colider de la fruta
            GetComponent<Collider2D>().enabled = false;

            // Activar animación collected
            gameObject.transform.GetChild(0).gameObject.SetActive(true);

            //Eliminar la fruta en un tempo de 0.5s
            Destroy(gameObject,0.5f);
        }
    }
}
