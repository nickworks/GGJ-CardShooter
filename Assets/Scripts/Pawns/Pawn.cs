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

    List<Tome> tomes = new List<Tome>();
    int currentTomeIndex = 0;



    void Start()
    {
        body = GetComponent<CharacterController>();
    }
    
    void Update()
    {
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
    public void Attack() {
        ShootProjectile();
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
    #endregion

    //this is currently called by the projectile hitting this object. I do not know if that is consistent with the player controller design pattern
    /// <summary>
    /// applys damage to the pawn
    /// </summary>
    /// <param name="damage">The damage to be reducted from this pawn's health</param>
    public void ApplyDamage(float damage) {
        health -= damage;
        print("Ouch! My health is now just:" + health);
    }
   
    void ShootProjectile() {

        Tome tome = CurrentTome();

        Projectile projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.Init(this.gameObject);

        tome.ModifyProjectile(projectile);
    }
}
