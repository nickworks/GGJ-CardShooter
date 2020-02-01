using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10;
    public float lifespan = 2;


    /// <summary>
    /// The Damage applyed to the actor hitby this projectile, after card modifications
    /// </summary>
    public float damage;

    /// <summary>
    /// The Pawn that is responsible for the creation of this projectile
    /// </summary>
    GameObject owner;

    Vector3 velocity;
    float age = 0;

    void Start()
    {
        velocity = transform.forward * speed;
    }
    /// <summary>
    /// Use this to pass info from Pawn to Projectile.
    /// </summary>
    public void Init(GameObject owner) {
        // TODO
        this.owner = owner;
    }

    // Update is called once per frame
    void Update()
    {
        if (Game.isPaused) return;

        transform.position += velocity * Time.deltaTime;
        age += Time.deltaTime;
        if(age > lifespan) {
            HandleDeath();
        }
    }

    /*
    void OnCollisionEnter(Collision collision) {
        print("Collision");
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
    }
    */

    //some refactoring may be in order here do decuple damage calculation

    /// <summary>
    /// We hit something! This applys damage and thn destroys this projectile if relevent
    /// </summary>
    /// <param name="other">the "other" colider this game object has impacted</param>
    private void OnTriggerEnter(Collider other)
    {
       //print("Collision " + owner);
        if (other.gameObject != owner) {
            //print("hit");
            Pawn target = other.gameObject.GetComponent<Pawn>();
            if (target != null) {
                target.ApplyDamage(damage);
            }

            HandleDeath();
        }
    }


    void HandleDeath() {

        Destroy(gameObject);
    }
}
