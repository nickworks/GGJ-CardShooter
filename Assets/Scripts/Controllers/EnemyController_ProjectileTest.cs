using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is just a shell for testing projectiles, it spinns and shoots
/// </summary>
public class EnemyController_ProjectileTest : Controller
{
    /// <summary>
    /// This is the "pawn" currently "possessed" by this controller.
    /// Currently, the pawn is just a script also loaded onto this same game object.
    /// </summary>
    Pawn pawn;

    /// <summary>
    /// how long in seconds between shots. Frame rate dependent
    /// </summary>
    public int timerSet = 3;
    int timer = 0;

    /// <summary>
    /// should th epawn spin while shooting
    /// </summary>
    public bool rotate;

    //float angle = 0;
    /// <summary>
    /// How far to spin in between shots
    /// </summary>
    public float anglMod = 25;
    float anglemax = 360;

    // Start is called before the first frame update
    void Start() {
        timerSet *= 60;
        pawn = GetComponent<Pawn>();
        pawn.PickupTome(Tome.Authored(new Card.Effect[] { Card.Effect.None } ));
    }

    // Update is called once per frame
    void Update() {
        if (Game.isPaused) return;
        if (timer <= 0) {
            pawn.StartAttack();
            pawn.StopAttack();
            timer = timerSet;
            Rotate();
        }

        timer--;
    }

    /// <summary>
    /// Rotate the pawn a set interval if that is desierable. Ths is called after each shot is fired.
    /// </summary>
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
