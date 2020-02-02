using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Pawn : MonoBehaviour
{
    public float moveSpeed = 3;
    [Range(.00001f, .05f)] public float rotationDampening = .05f;
    public float health = 100;

    public Projectile projectilePrefab;

    CharacterController body;
    Quaternion lookDirection = Quaternion.identity;

    public List<Tome> tomes = new List<Tome>();
    int currentTomeIndex = 0;

    public bool wantsToAttack = false;

    public Room_Volume currentRoom;

    float cooldownBeforeAttacking = 0;

    void Start()
    {
        body = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        if (Game.isPaused) return;
        if (cooldownBeforeAttacking > 0) cooldownBeforeAttacking -= Time.deltaTime;
        else if (wantsToAttack) Attack();
        transform.rotation = MathStuff.Damp(transform.rotation, lookDirection, rotationDampening);
    }

    #region API (call these funcs from controller classes):
    /// <summary>
    /// This function tells the pawn to move.
    /// </summary>
    /// <param name="dir">The direction to move the pawn. This should be a 2D value, as if viewing from above.</param>
    public void Move(Vector2 dir) {

        Vector3 dis = new Vector3(dir.x, 0, dir.y) * moveSpeed;
        body.SimpleMove(dis);
    }
    public void LookAim(float angle) {
        lookDirection = Quaternion.Euler(0, angle, 0);
    }
    private void Attack() {

        if (cooldownBeforeAttacking > 0) return;

        int projectileCount = CurrentTome().HowManyProjectiles();
        cooldownBeforeAttacking = CurrentTome().GetDelayBetweenShots();

        CurrentTome().Use();

        float degreesBetweenBullets = 10;
        float offset = (projectileCount - 1)* degreesBetweenBullets / 2;

        for (int i = 0; i < projectileCount; i++) {
            ShootProjectile(-offset + degreesBetweenBullets * i);
        }
    }
    public void StartAttack() {
        Attack();
        wantsToAttack = true;
    }
    public void StopAttack() {
        wantsToAttack = false;
        cooldownBeforeAttacking = 0;
    }
    public void NextTome() {
        if (++currentTomeIndex >= tomes.Count) currentTomeIndex = 0;
    }
    public void PrevTome() {
        if (--currentTomeIndex < 0) currentTomeIndex = tomes.Count - 1;
    }


    public Tome CurrentTome() {
        
        if (currentTomeIndex < 0) return new Tome(); // empty tome
        if (currentTomeIndex >= tomes.Count) return new Tome(); // empty tome
        return tomes[currentTomeIndex] ?? new Tome();

    }

    public void PickupTome(Tome tome) {
        currentTomeIndex = tomes.Count;
        tomes.Add(tome);
    }

    public void PickupCard(Card card) {
        CurrentTome().AddCard(card);
    }
    #endregion

    //this is currently called by the projectile hitting this object. I do not know if that is consistent with the player controller design pattern
    /// <summary>
    /// applys damage to the pawn
    /// </summary>
    /// <param name="damage">The damage to be reducted from this pawn's health</param>
    public void ApplyDamage(float damage) {
        health -= damage;
        //print("Ouch! My health is now just:" + health);
    }
   
    void ShootProjectile(float yaw) {

        Tome tome = CurrentTome();

        yaw += transform.rotation.eulerAngles.y;

        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, yaw, 0));
        projectile.Init(gameObject);

        tome.ModifyProjectile(projectile);
    }
    
}
