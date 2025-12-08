using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public static bool isGrounded; //static para usar la variable en otro script

    //Para cuando el colider entre a una geometría
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
    }

    //Para cuando salga de una geometria
    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
}
