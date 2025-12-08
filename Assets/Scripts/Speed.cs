using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Activa la velocidad si se consumio fruta
            FindObjectOfType<PlayerMove>().ActivarReducVelocidadTemporal(2.0f, 3.0f);

        }
    }
}
