using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPebble : MonoBehaviour
{
    public float speed = 2f;
    public float lifetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}
