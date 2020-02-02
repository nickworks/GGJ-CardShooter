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
    public TomeGUI tomeGUIPrefab;

    public float cardScaleSmall = .01f;
    public float cardScaleBig = .1f;

    /// <summary>
    /// The current state of the gui.
    /// </summary>
    State state = State.Mini;
    
    /// <summary>
    /// The background image. We store a reference to this to fade it in/out when paused/unpaused.
    /// </summary>
    Image bg;

    /// <summary>
    /// The current tome held by the player
    /// </summary>
    Tome currentTome;

    /// <summary>
    /// The cardGUI objects managed by this CardManager.
    /// The manager gives the cards position, rotation, and scale info.
    /// </summary>
    List<CardGUI> cards = new List<CardGUI>(); // card index 0 should be on bottom

    /// <summary>
    /// The tome icons on-screen. Managed by this CardManager.
    /// </summary>
    List<TomeGUI> tomes = new List<TomeGUI>();
    public int currentTomeIndex = 0;

    public RectTransform loadoutArea;

    void Start() {
        bg = GetComponent<Image>();
    }
    public void SwitchTomes(List<Tome> tomes, Tome tome, int shift) {

        for (int i = 0; i < tomes.Count; i++) {
            Tome t = tomes[i];
            bool thisTomeExistsInGui = false;
            foreach(TomeGUI tg in this.tomes) {
                if (tg.tome == t) {
                    thisTomeExistsInGui = true;
                    break;
                }
            }
            if (!thisTomeExistsInGui) {
                TomeGUI newTomeGUI = Instantiate(tomeGUIPrefab, loadoutArea);
                newTomeGUI.tome = t;
                this.tomes.Insert(i, newTomeGUI);
            }
        }
        currentTomeIndex = tomes.IndexOf(tome);

        float dx = 130;
        float x = -(this.tomes.Count - 1) * dx / 2;
        for (int i = 0; i < this.tomes.Count; i++) {
            TomeGUI t = this.tomes[i];
            bool isCurrent = (i == currentTomeIndex) ;

            t.AnimateTo(new Vector3(x + dx * i, 0), isCurrent);

        }

        RemoveAllCards();
        currentTome = tome;
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
    public void AddCard(Card card, float delay = 0) {

        RectTransform tome = (tomes[currentTomeIndex].transform as RectTransform);

        CardGUI newCard = Instantiate(cardGUIPrefab, transform);
        newCard.Init(card, tome); // give the gui a reference to the card data
        cards.Insert(0, newCard);

        if(state == State.Mini) {
            // size / position of new card if added while in mini mode:
            RectTransform rt = (newCard.transform as RectTransform);
            rt.SetParent(newCard.tomeGUI);
            rt.anchoredPosition = new Vector2(0, 0);
            rt.localScale = Vector3.zero;

        }

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


        cards[index].AnimateTo(new Vector3(100, 0, 0), Quaternion.Euler(0, 0, 1000), transform.localScale.x, 0);
        Destroy(cards[index].gameObject, .2f);

        cards.RemoveAt(index);
        PositionCards();
        print("lose card??");
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
        float y = 100;
        float dy = 50;
        for (int i = 0; i < cards.Count; i++) {
            Vector3 pos = new Vector2(0, y + dy * i);
            Quaternion rot = Quaternion.Euler(0, 180, 90);
            cards[i].AnimateTo(pos, rot, cardScaleSmall, (cards.Count - i) * .1f);
            //cards[i].transform.SetParent(tomes[0]);
            cards[i].SetParentToTome();
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

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
            ChangeState((state == State.Inspect) ? State.Mini : State.Inspect);

        CheckForUpdatesToTome();


        float a = bg.color.a;
        if (a < 0.6f && state == State.Inspect) a += 2 * Time.deltaTime;
        if (a > 0.0f && state == State.Mini) a -= 2 * Time.deltaTime;
        bg.color = new Color(0, 0, 0, a);
    }

    private void CheckForUpdatesToTome() {
        if (currentTome.updatedSinceLastRendered) {

            for (int i = currentTome.cards.Count - 1; i >= 0; i--) {
                bool cardAlreadyExists = false;
                foreach (CardGUI card in cards) {
                    if (card.Matches(currentTome.cards[i])){
                        cardAlreadyExists = true;
                        break;
                    }
                }
                if (!cardAlreadyExists) {
                    AddCard(currentTome.cards[i]);
                }
            }
            currentTome.updatedSinceLastRendered = false;
        }

        // TODO: also check for cards that were removed...
    }
}
