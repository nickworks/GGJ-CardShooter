using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [System.Serializable]
    public struct Effect {
        public Card.Effect effect;
        public int amount;
    }

    public float speed = 10;
    public float lifespan = 2;

    /// <summary>
    /// The damage of this projectile, before card modifications
    /// </summary>
    float baseDamage;

    private bool sinWave;
    private Transform homingTarget;

    public List<Effect> effects = new List<Effect>();

    private float timeUntilShake = 0;

    /// <summary>
    /// The Pawn that is responsible for the creation of this projectile
    /// </summary>
    GameObject owner;

    //public Vector3 velocity;
    float age = 0;

    void Start()
    {
        
    }
    /// <summary>
    /// Use this to pass info from Pawn to Projectile.
    /// </summary>
    public void Init(GameObject owner) {
        this.owner = owner;
    }
    public void Init(float tomeBaseDamage) {
        baseDamage = tomeBaseDamage;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Game.isPaused) return;


        if (homingTarget) {

            Vector3 vectorToTarget = (homingTarget.position - transform.position).normalized;
            Quaternion goal = Quaternion.FromToRotation(Vector3.forward, vectorToTarget);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, goal, 360 * Time.deltaTime);

            if (timeUntilShake <= 0) {
                Quaternion rand = Quaternion.FromToRotation(Vector3.forward, Random.onUnitSphere);
                transform.rotation *= rand;
                timeUntilShake = .1f;
            }

            //Vector3 force = (vectorToTarget - velocity);
            //force = force.normalized * .2f;
            //velocity += force;
        }

        transform.position += transform.forward * speed * Time.deltaTime;
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
            if (target != null) CrashedIntoPawn(target);

            HandleDeath();
        }
    }
    void CrashedIntoPawn(Pawn p) {

        float damage = baseDamage;


        foreach(Effect card in effects) {

            if (card.effect == Card.Effect.ProjectileDamage2X) baseDamage *= card.amount;

        }

        // apply other effects to the hit pawn
        p.ApplyDamage(damage);
    }


    void HandleDeath() {

        Destroy(gameObject);
    }

    public void MakeHoming() {
        //print("I home?");

        Pawn[] targets = GameObject.FindObjectsOfType<Pawn>();

        homingTarget = targets[Random.Range(0, targets.Length)].transform;
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.up);// velocity = Vector3.up * speed * .8f;
        lifespan = 4;
        speed = 10;
    }
    public void MakeBig(int cardValue) {
        float currentScale = transform.localScale.x;
        currentScale += cardValue / (float) 2;
        transform.localScale = Vector3.one * currentScale;
    }
}
