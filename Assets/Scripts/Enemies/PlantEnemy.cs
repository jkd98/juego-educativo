using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class PlantEnemy : MonoBehaviour
{
    private float waitedtime;
    private float waitTimeToAttack = 3f;
    public Animator animator;

    public GameObject projectilePrefab;

    public Transform lauchPointProjectile;

    void Start()
    {
        waitedtime = waitTimeToAttack;
    }

    void Update()
    {
        if (waitedtime <= 0f)
        {
            waitedtime = waitTimeToAttack;
            animator.Play("Attack");
            Invoke("AttackRoutine", 0.5f);
        }
        else
        {
            waitedtime -= Time.deltaTime;
        }
    }

    void AttackRoutine()
    {
        GameObject nwBullete;
        nwBullete = Instantiate(projectilePrefab, lauchPointProjectile.position, lauchPointProjectile.rotation);
    }

}
