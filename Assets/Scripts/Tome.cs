using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tome
{
    public static Tome Random() {
        Tome tome = new Tome();
        tome.cards.Add(Card.Random());
        tome.cards.Add(Card.Random());
        tome.cards.Add(Card.Random());
        return tome;
    }

    /// <summary>
    /// The cards held within the tome.
    /// </summary>
    public List<Card> cards = new List<Card>();

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
