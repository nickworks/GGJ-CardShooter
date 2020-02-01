using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is just a shell for testing projectiles
/// </summary>
public class EnemyController_ProjectileTest : Controller
{
    /// <summary>
    /// This is the "pawn" currently "possessed" by this controller.
    /// Currently, the pawn is just a script also loaded onto this same game object.
    /// </summary>
    Pawn pawn;

    int timerSet = 3;
    int timer = 0;

    public bool rotate;

    //float angle = 0;
    float anglMod = 25;
    float anglemax = 360;

    // Start is called before the first frame update
    void Start() {
        timerSet *= 60;
        pawn = GetComponent<Pawn>();
        pawn.PickupTome(Tome.Authored(new Card.Effect[] { Card.Effect.None } ));
    }

    // Update is called once per frame
    void Update() {
        if (timer <= 0) {
            pawn.Attack();
            timer = timerSet;
            Rotate();
        }

        timer--;
    }

    void Rotate() {

        if (rotate)
        {
            float newRot = transform.rotation.eulerAngles.y + anglMod;
            // print(newRot);
            pawn.LookAim(newRot);

           

            if (transform.rotation.y > anglemax)
            {
                pawn.LookAim(0);
            }
        }

    }
}
