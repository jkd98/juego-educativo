using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetJump : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Activa la velocidad si se consumio fruta
            FindObjectOfType<PlayerMove>().RestablecerSaltoNormal();

        }
    }
}
