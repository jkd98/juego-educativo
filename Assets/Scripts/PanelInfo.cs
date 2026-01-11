using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelInfo : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI infoText;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colision con el simbolo");
        if (collision.gameObject.CompareTag("Player"))
        {
            panel.SetActive(true);
            infoText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            panel.SetActive(false);
            infoText.gameObject.SetActive(false);
        }
    }
}
