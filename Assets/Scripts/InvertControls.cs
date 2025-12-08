using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertControls : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Activa la velocidad si se consumio fruta
            FindObjectOfType<PlayerMove>().ActivarControlesInvertidos(10.0f);

        }
    }
}
