﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tome
{
    public float tomeBaseDamage = 3; 
    
    public static Tome Random() {
        Tome tome = new Tome();
        tome.cards.Add(Card.Random());
        tome.cards.Add(Card.Random());
        tome.cards.Add(Card.Random());
        return tome;
    }

    /// <summary>
    /// This returns a tome with a pre-ordained set of cards
    /// </summary>
    /// <param name="cardEffects">The effects of the cards that this tome will be created with</param>
    /// <returns>A Tome populated with the specified cards</returns>
    public static Tome Authored(Card.Effect[] cardEffects )
    {
        Tome tome = new Tome();
        foreach (Card.Effect e in cardEffects ){
            tome.cards.Add(Card.GetSpecificCard(e));
        }
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
