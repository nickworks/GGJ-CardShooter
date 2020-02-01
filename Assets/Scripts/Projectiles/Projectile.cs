using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10;
    public float lifespan = 2;

    Vector3 velocity;
    float age = 0;

    void Start()
    {
        velocity = transform.forward * speed;
    }
    /// <summary>
    /// Use this to pass info from Pawn to Projectile.
    /// </summary>
    public void Init() {
        // TODO
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
        age += Time.deltaTime;
        if(age > lifespan) {
            Destroy(gameObject);
        }
    }
}
