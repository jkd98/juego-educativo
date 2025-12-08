using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    // Cuando algo entre en contacto con el collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Player Damage");
            //Destroy(collision.gameObject);
            collision.transform.GetComponent<PlayerRespawn>().PlayerDamage(); // Llama al método PlayerDamage del script PlayerRespawn

        }
    }
}
