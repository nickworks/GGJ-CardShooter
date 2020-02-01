using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{

    public enum State {
        Mini,
        Inspect
    }

    public CardGUI cardGUIPrefab;
    public RectTransform areaLoadout;

    State state = State.Mini;
    List<CardGUI> cards = new List<CardGUI>(); // card index 0 should be on bottom

    void Start() {
        AddCard(new Card());
        AddCard(new Card());
        AddCard(new Card());
        AddCard(new Card());
        AddCard(new Card());
    }
    
    public void AddCards(Card[] cards) {
        foreach(Card card in cards) {
            AddCard(card);
        }
    }
    public void AddCard(Card card) {
        CardGUI newCard = Instantiate(cardGUIPrefab, transform);
        // TODO: copy stats and stuff over to card GUI
        if (cards.Count == 0) newCard.Tint();
        cards.Add(newCard);
        PositionCards();
    }
    void PositionCards() {

        for(int i = 0; i < cards.Count; i++) {
            cards[i].SetDepth(cards.Count - i);
        }

        switch (state) {
            case State.Mini:
                PositionCardsMini();
                break;
            case State.Inspect:
                PositionCardsInspect();
                break;
        }
    }
    void PositionCardsMini() {
        float dy = 50;
        for (int i = 0; i < cards.Count; i++) {
            Vector3 pos = new Vector2(0, 0 + dy * i);
            Quaternion rot = Quaternion.Euler(0, 180, 90);
            float scale = 1;
            cards[i].AnimateTo(pos, rot, scale, (cards.Count - i) * .1f);
            cards[i].transform.SetParent(areaLoadout);
        }
    }
    void PositionCardsInspect() {
        float dx = 150;
        float x = -dx * (cards.Count - 1) / 2f;
        for (int i = 0; i < cards.Count; i++) {
            Vector3 pos = new Vector2(x + dx * i, 0);
            Quaternion rot = Quaternion.identity;
            float scale = 2;
            cards[i].AnimateTo(pos, rot, scale, i * .1f);
            cards[i].transform.SetParent(transform);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            state = (state == State.Inspect) ? State.Mini : State.Inspect;
            PositionCards();
        }
    }
}
