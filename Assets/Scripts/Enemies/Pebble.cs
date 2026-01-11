using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pebble : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float spee = 0.5f;
    private float waitTime; // detenga en el punto que al que llega
    public float startWaitTime = 2f; // tiempo que espera en cada punto
    private int i; //punto al que se dirige
    private Vector2 actualPosition; //posicion actual del enemigo
    public Transform[] moveSpots; //puntos a los que se mueve el enemigo


    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CheckEnemyMove());
        // hacia (current, target, maxDistanceDelta)
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[i].position, spee * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpots[i].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                if (moveSpots[i] != moveSpots[moveSpots.Length - 1])
                {
                    i++;
                }
                else
                {
                    i = 0;
                }
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        {

        }
    }

    IEnumerator CheckEnemyMove()
    {
        actualPosition = transform.position;
        yield return new WaitForSeconds(0.5f);
        if (actualPosition.x < transform.position.x)
        {
            //se mueve a la derecha
            spriteRenderer.flipX = false;
            //animator.SetBool("Idle", false);
        }
        else if (actualPosition.x > transform.position.x)
        {
            //se mueve a la izquierda
            spriteRenderer.flipX = true;
            //animator.SetBool("Idle", false);
        }
        else if (transform.position.x == actualPosition.x)
        {
            //esta quieto
            //animator.SetBool("Idle", true);
        }

    }
}