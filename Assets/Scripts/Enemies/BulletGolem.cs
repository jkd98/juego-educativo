using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGolem : MonoBehaviour
{
    public float speed = 2f;
    public float lifeTime = 2f;
    public bool left = true;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (left)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }
}
