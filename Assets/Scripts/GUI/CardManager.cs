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
    public float cardScaleSmall = .01f;
    public float cardScaleBig = .1f;

    State state = State.Mini;
    List<CardGUI> cards = new List<CardGUI>(); // card index 0 should be on bottom
    Image bg;

    void Start() {
        bg = GetComponent<Image>();
    }
    public void SwitchTomes(Tome tome) {
        RemoveAllCards();
        AddCards(tome.cards.ToArray());
    }
    public void RemoveAllCards() {
        foreach(CardGUI card in cards) {
            Destroy(card.gameObject);
        }
        cards.Clear();
    }
    
    public void AddCards(Card[] cards) {
        foreach(Card card in cards) {
            AddCard(card);
        }
    }
    public void AddCard(Card card) {
        CardGUI newCard = Instantiate(cardGUIPrefab, transform);
        newCard.Init(card); // give the gui a reference to the card data
        cards.Insert(0, newCard);
        PositionCards();
    }
    public void PopCard() {
        LoseCardAt(cards.Count - 1);
    }
    public void DestroyCard() {
        LoseCardAt(0);
    }
    void LoseCardAt(int index) {
        if (cards.Count == 0) return;
        if (index < 0) return;
        if (index >= cards.Count) return;
        Destroy(cards[index].gameObject);
        cards.RemoveAt(index);
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
            cards[i].AnimateTo(pos, rot, cardScaleSmall, (cards.Count - i) * .1f);
            cards[i].transform.SetParent(areaLoadout);
        }
    }
    void PositionCardsInspect() {
        float dx = 150;
        float x = -dx * (cards.Count - 1) / 2f;
        for (int i = 0; i < cards.Count; i++) {
            float percent = cards.Count == 1 ? .5f : i / (float)(cards.Count - 1);
            
            float y = Mathf.Sin(percent * 40) * 25;
            y -= Mathf.Abs(.5f - percent) * 75 + 25;
            Vector3 pos = new Vector2(x + dx * i, y);
            float roll = MathStuff.Lerp(20, -20, percent);
            Quaternion rot = Quaternion.Euler(0,0,roll);
            cards[i].AnimateTo(pos, rot, cardScaleBig, i * .1f);
            cards[i].transform.SetParent(transform);
        }
    }
    void ChangeState(State newState) {
        state = newState;
        Game.isPaused = (state == State.Inspect);
        PositionCards();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ChangeState((state == State.Inspect) ? State.Mini : State.Inspect);

        if (Input.GetKeyDown(KeyCode.KeypadPlus)) AddCard(Card.Random());
        if (Input.GetKeyDown(KeyCode.KeypadMinus)) PopCard();

        float a = bg.color.a;
        if (a < 0.6f && state == State.Inspect) a += 2 * Time.deltaTime;
        if (a > 0.0f && state == State.Mini) a -= 2 * Time.deltaTime;
        bg.color = new Color(0, 0, 0, a);
    }
}
