using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCard : MonoBehaviour
{
    Card card;

    void Start() {
        card = Card.Random();
    }

    void OnTriggerEnter(Collider collider) {
        PlayerController pc = collider.GetComponent<PlayerController>();
        if (pc) {
            pc.GetComponent<Pawn>().PickupCard(card);
            Destroy(gameObject);
        }
    }
}
