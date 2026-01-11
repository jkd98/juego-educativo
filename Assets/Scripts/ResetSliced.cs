using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSliced : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {


            //collision.transform.GetComponent<PlayerMove>().EliminarDeslizamientoManual();
            collision.transform.GetComponent<PlayerMove>().RestablecerVelocidadNormal();



            //Eliminar la fruta en un tempo de 0.5s
            //Destroy(gameObject, 0.7f);
        }
    }
}