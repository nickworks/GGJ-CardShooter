using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tome
{
    /// <summary>
    /// The cards held within the tome.
    /// </summary>
    List<Card> cards = new List<Card>();

    /// <summary>
    /// Apply the effects of each card to a projectile.
    /// </summary>
    /// <param name="projectile">The projectile to modify.</param>
    public void ModifyProjectile(Projectile projectile) {
        foreach(Card card in cards) {
            card.ModifyProjectile(projectile);
        }
    }
}
