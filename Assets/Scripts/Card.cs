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
        return new Card();
    }

    Effect effect = Effect.None;

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

}
