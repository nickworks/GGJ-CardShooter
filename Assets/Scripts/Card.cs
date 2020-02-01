using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card {
    public enum Effect {
        None,
        ProjectileDamage2X,
        ProjectileFire,
        ProjectilePoison,
        ProjectileLightning,
        ProjectileIce,
        ProjectileExplosive,
        ProjectileHoming,
        ProjectileHitScan,
        ProjectileSinWave,
        ProjectileSpread,
        ProjectileRapidFire,
        ProjectileHuge,
        ProjectileVampire,
        ProjectileCharm,
        PawnDefense2X,
        PawnSpeed2X
    }

    /// <summary>
    /// This function allows a simple way to create random cards.
    /// It should only generate card combinations that have been unlocked.
    /// </summary>
    /// <returns>A random card</returns>
    public static Card Random() {
        // TODO

        var possibleEffects = Effect.GetValues(typeof(Effect));
        Effect effect = (Effect)possibleEffects.GetValue(UnityEngine.Random.Range(1, possibleEffects.Length));

        return new Card() {
            effect = effect
        };
    }

    public Effect effect = Effect.None;

    
    /// <summary>
    /// This function creates a card with a specific effect
    /// This is for creating authored enemies and testing projectiles
    /// </summary>
    /// <param name="desiredEffect">The effect the returned card should create</param>
    /// <returns> a card with a specific effect </returns>
    public static Card GetSpecificCard(Effect desiredEffect) {

        return new Card()
        {
            effect = desiredEffect
        };

    }
    

    /// <summary>
    /// Apply the effects of this card to a projectile.
    /// </summary>
    /// <param name="projectile">The projectile to modify.</param>
    public void ModifyProjectile(Projectile projectile) {
        switch (effect) {
            case Effect.None:
                break;
            case Effect.ProjectileDamage2X:
                break;
            case Effect.ProjectileFire:
                break;
            case Effect.ProjectilePoison:
                break;
            case Effect.ProjectileLightning:
                break;
            case Effect.ProjectileIce:
                break;
            case Effect.ProjectileExplosive:
                break;
            case Effect.ProjectileHoming:
                break;
            case Effect.ProjectileHitScan:
                break;
            case Effect.ProjectileSinWave:
                break;
            case Effect.ProjectileSpread:
                break;
            case Effect.ProjectileRapidFire:
                break;
            case Effect.ProjectileHuge:
                break;
            case Effect.ProjectileVampire:
                break;
            case Effect.ProjectileCharm:
                break;
            case Effect.PawnDefense2X:
                break;
            case Effect.PawnSpeed2X:
                break;
        }
    }

    /// <summary>
    /// Should be called when a card is added to a tome. This is indended to facilitate passive modifyers to speed or defence 
    /// </summary>
    public void OnTomeAddition() { 
    
    
    }

    /// <summary>
    /// Should be callled when a card is removed from a tome durring normal play. This is indended to facilitate passive modifyers to speed or defence 
    /// </summary>
    public void OnDestruction() { 
    
    }



}
