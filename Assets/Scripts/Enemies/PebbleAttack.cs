using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PebbleAttack : MonoBehaviour
{
    public Animator animator;
    private float distanceRaycast = 2f;
    private float attackCooldown = 1.5f;
    private float actualCooldown; //0
    public GameObject bulletPrefab;
    void Start()
    {
        actualCooldown = 0f;
    }

    void Update()
    {
        actualCooldown -= Time.deltaTime; // para que baje el cooldown con el tiempo
        Debug.DrawRay(transform.position, Vector2.down, Color.red, distanceRaycast);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distanceRaycast);
       
        if (hit.collider != null)
        {
            Debug.Log("Golpeé a: " + hit.collider.name + " con el Tag: " + hit.collider.tag);
            if (hit.collider.CompareTag("Player") && actualCooldown <= 0f)
            {
                Debug.Log("Hittt");
                Invoke("LaunchBullet", 0.5f);
                animator.Play("Attack");
                actualCooldown = attackCooldown;
            }
        }
    }

    void LaunchBullet()
    {
        GameObject newBullet;
        newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
