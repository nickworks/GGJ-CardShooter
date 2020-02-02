﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tome
{
    /// <summary>
    /// The base damage of this tome, projectiles will modify this falue in order to calculate their final damage output
    /// </summary>
    public float tomeBaseDamage = 3;

    /// <summary>
    /// The number of cards this tome can hold.
    /// </summary>
    public int cardCap = 3;
    
    public static Tome Random() {
        Tome tome = new Tome();

        tome.cardCap = UnityEngine.Random.Range(2, 6);
        tome.tomeBaseDamage = UnityEngine.Random.Range(1, 8);

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

    public bool updatedSinceLastRendered = true;

    public void AddCard(Card card) {
        // add card:
        cards.Add(card);

        // remove cards if we've exceeded this book's limit:
        if (cards.Count > cardCap) cards.RemoveAt(0);

        // mark tome as "dirty":
        updatedSinceLastRendered = true;
    }

    public void Use() {
        Card card = TopCard();
        if (card == null) return;

        card.Use();
        IsTheTopCardDestroyed();
    }
    public Card TopCard() {
        if (cards.Count == 0) return null;
        return cards[cards.Count - 1];
    }
    public bool IsTheTopCardDestroyed() {
        if (cards.Count == 0) return false;

        int i = cards.Count - 1;

        Card topCard = cards[i];
        if (topCard.GetDurability() <= 0) {
            cards.RemoveAt(i);
            updatedSinceLastRendered = true;
            return true;
        }
        return false;
    }
    public int HowManyProjectiles() {
        int total = 1;
        foreach (Card card in cards) {
            if (card.effect == Card.Effect.ProjectileSpread) total += card.numberValue;
        }
        return total;
    }
    /// <summary>
    /// Apply the effects of each card to a projectile.
    /// </summary>
    /// <param name="projectile">The projectile to modify.</param>
    public void ModifyProjectile(Projectile projectile) {

        projectile.Init(tomeBaseDamage);

        foreach (Card card in cards) {
            card.ModifyProjectile(projectile);
        }
    }
}
