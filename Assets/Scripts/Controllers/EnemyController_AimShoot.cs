using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_AimShoot : MonoBehaviour
{
    /// <summary>
    /// This is the "pawn" currently "possessed" by this controller.
    /// Currently, the pawn is just a script also loaded onto this same game object.
    /// </summary>
    Pawn pawn;

    /// <summary>
    /// A reference to the player so enemies may seek it.
    /// </summary>
    Pawn player;

    /// <summary>
    /// should th epawn spin while shooting
    /// </summary>
    public bool rotate;

    /// <summary>
    /// The minimum value for shot delays.
    /// </summary>
    public float minShotDelay = .25f;
    /// <summary>
    /// The maximum value for shot delays.
    /// </summary>
    public float maxShotDelay = 1.5f;
    /// <summary>
    /// The time in seconds to pass before the next shot.
    /// </summary>
    float shotDelay;

    // Start is called before the first frame update
    void Start()
    {
        shotDelay = 2 * Random.Range(minShotDelay, maxShotDelay);

        pawn = GetComponent<Pawn>();
        pawn.PickupTome(Tome.Authored(new Card.Effect[] { Card.Effect.None }));

        findPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        aimAtPlayer();
        if (pawn.currentRoom != player.currentRoom)
        {
            return;
        }

        if (shotDelay <= 0)
        {

            pawn.StartAttack();
            pawn.StopAttack();
            shotDelay = Random.Range(minShotDelay, maxShotDelay);
        }

        shotDelay-=Time.deltaTime;
    }

    void aimAtPlayer()
    {
        if (!player)
        {
            findPlayer();
            return;
        }
        
        Vector3 dis = player.transform.position - transform.position;
        float lookYaw = Mathf.Rad2Deg * Mathf.Atan2(dis.x, dis.z);
        pawn.LookAim(lookYaw);
        
    }
    
    void findPlayer()
    {
        player = GameObject.FindObjectOfType<PlayerController>().GetPawn();
    }
}
