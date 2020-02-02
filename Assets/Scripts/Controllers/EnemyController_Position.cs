using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Position : Controller
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
    /// The minimum distance from the player that this entitiy will attempt to maintain.
    /// </summary>
    public float minTargetDist = 3;
    /// <summary>
    /// The maximum distance from the player that this entity will attempt to maintain.
    /// </summary>
    public float maxTargetDist = 4;

    /// <summary>
    /// The minimum variance that this entity will tolerate being outside of their target distance.
    /// </summary>
    public float minComfortZone = .5f;
    /// <summary>
    /// The maximum variance that this entity will tolerate being outside of their target distance.
    /// </summary>
    public float maxComfortZone = 1.5f;

    /// <summary>
    /// The minimum variance that this entity will employ when approaching the player (offset by degrees from a direct head-on approach).
    /// </summary>
    public float minApproachAngleVariance = 15;
    /// <summary>
    /// The maximum variance that this enttity will employ when approaching the player (offset by degrees from a direct headon appraoch).
    /// </summary>
    public float maxApproachAngleVariance = 30;

    /// <summary>
    /// The minimum variance that this entity will employ when fleeing the player (offset by degrees from a direct head-on approach).
    /// </summary>
    public float minRetreatAngleVariance = 45;
    /// <summary>
    /// The maximum variance that this enttity will employ when fleeing the player (offset by degrees from a direct headon appraoch).
    /// </summary>
    public float maxRetreatAngleVariance = 90;

    /// <summary>
    /// The minimum ammount of time that can pass before the AI recalculates new comfort zones, approach angles, etc.
    /// </summary>
    public float minTimeTillRecalcVariance = 1;
    /// <summary>
    /// The maximum ammount of time that can pass before the AI recalculates new comfort zones, approach angles, etc.
    /// </summary>
    public float maxTimeTillRecalcVariance = 3;

    /// <summary>
    /// The distance in meters that this entity will attempt to maintain from the player.
    /// </summary>
    float targetDist = 5.5f;
    /// <summary>
    /// The variance in meters that this entity will tolerate being outside of their target dist.
    /// </summary>
    float comfortZone = 1;
    /// <summary>
    /// The variance in degrees away from head-on that this entity will employ when seeking the player.
    /// </summary>
    float approachingAngle = 20;
    /// <summary>
    /// The variance in degrees away from directly away that this entity will employ when fleeing the player.
    /// </summary>
    float retreatAngle = 60;
    /// <summary>
    /// The remaining time until random variance values are re-calculated.
    /// </summary>
    float timeTillRecalc = 2;

    /// <summary>
    /// The movement speed while actively trying to enter a comfort zone.
    /// </summary>
    public float actionSpeed = 1;
    /// <summary>
    /// The movement speed while in a comfort zone.
    /// </summary>
    public float wanderSpeed = .25f;

    /// <summary>
    /// The direction this enemy will randomly wander in when they are in their comfort zone;
    /// </summary>
    float wanderDir;
    /// <summary>
    /// The current direction of movement.
    /// </summary>
    Vector2 moveDir;
    

    // Start is called before the first frame update
    void Start()
    {

        calcNewVariance();

        pawn = GetComponent<Pawn>();
        pawn.PickupTome(Tome.Authored(new Card.Effect[] { Card.Effect.None }));

        findPlayer();

        Animation anim = GetComponentInChildren<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Game.isPaused) return;

        seekPlayer();

        timeTillRecalc -= Time.deltaTime;
        if(timeTillRecalc < 0)
        {
            calcNewVariance();
        }
    }

    void seekPlayer()
    {
        if (!player)
        {
            findPlayer();

            return;
        }
        if(pawn.currentRoom != player.currentRoom)
        {
            return;
        }
        Vector3 rawDirToPlayer = (player.transform.position - pawn.transform.position);
        Vector2 dirToPlayer = new Vector2(rawDirToPlayer.x, rawDirToPlayer.z);

        Vector2 heading = new Vector2();
        float speed = pawn.moveSpeed;

        // this entity is too far from the player and should advance.
        if (rawDirToPlayer.sqrMagnitude > (targetDist + (.5f * comfortZone)) * (targetDist + (.5f * comfortZone)))
        {
            heading = dirToPlayer;

            heading = Quaternion.Euler(0, 0, approachingAngle) * heading;

            speed = actionSpeed;
        }
        // this entity is too close to the player and should retreat.
        else if (rawDirToPlayer.sqrMagnitude < (targetDist - (.5f * comfortZone)) * (targetDist - (.5f * comfortZone)))
        {
            heading = -dirToPlayer;

            heading = Quaternion.Euler(0, 0, retreatAngle) * heading;

            speed = actionSpeed;
        }
        else
        {
            heading = Vector2.Perpendicular(dirToPlayer) * wanderDir;
            speed = wanderSpeed;
        }

        moveDir = Vector2.Lerp(heading.normalized, moveDir.normalized, .9f);
        pawn.Move(moveDir.normalized);
        pawn.moveSpeed = Mathf.Lerp(speed, pawn.moveSpeed, .99f);
    }
    void findPlayer()
    {
        player = GameObject.FindObjectOfType<PlayerController>().GetPawn();
    }

    void calcNewVariance()
    {
        wanderDir = (Random.Range(-1f, 1f) > 0)? -1 : 1;

        targetDist = Random.Range(minTargetDist, maxTargetDist);
        comfortZone = Random.Range(minComfortZone, maxComfortZone);

        float randAngle = Random.Range(minApproachAngleVariance, maxApproachAngleVariance);
        approachingAngle = Random.Range(-randAngle * .5f, randAngle * .5f);
        randAngle = Random.Range(minRetreatAngleVariance, maxRetreatAngleVariance);
        retreatAngle = Random.Range(-randAngle * .5f, randAngle * .5f);

        timeTillRecalc = Random.Range(minTimeTillRecalcVariance, maxTimeTillRecalcVariance);
    }

}
