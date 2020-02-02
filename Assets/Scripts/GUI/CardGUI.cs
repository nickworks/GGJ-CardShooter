using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardGUI : MonoBehaviour {

    
    [System.Serializable]
    public struct EffectSprites {
        public Card.Effect cardEffect;
        public Sprite sprite;
        public Color color;
    }

    public EffectSprites[] spritesForCardEffects;

    public Image imgBackSprite;
    public Image imgBackground;
    public TextMeshProUGUI txtNumber;
    public TextMeshProUGUI txtDesc;

    Quaternion targetRotation;
    Vector2 targetPosition;
    float targetScale = 1;
    float delay = 0;

    /// <summary>
    /// The images that represent the durability of the card
    /// </summary>
    public Image[] damageOverlays;

    /// <summary>
    /// A reference to the car model
    /// </summary>
    Card card;
    RectTransform rt;
    float timeSinceLastAnim = 0;
    public bool destroyed = false;

    /// <summary>
    /// The parent GUI object. This is useful for positioning the card relative to its parent.
    /// </summary>
    [HideInInspector] public RectTransform tomeGUI;

    void Start() {
        rt = transform as RectTransform;
        MakeAllTheTextsOneSided(); // oof
    }
    public bool Matches(Card card) {
        return (this.card == card);
    }
    void MakeAllTheTextsOneSided() {
        // the best I can do right now
        TextMeshProUGUI[] txts = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI txt in txts) txt.enableCulling = true;
    }
    public void SetDepth(int i) {
        GetComponent<Canvas>().sortingOrder = i;
    }
    public void Init(Card card, RectTransform tomeGUI) {
        this.card = card;
        this.tomeGUI = tomeGUI;
        txtNumber.text = card.numberValue.ToString();

        foreach(EffectSprites sprite in spritesForCardEffects) {
            if(sprite.cardEffect == card.effect) {
                imgBackSprite.sprite = sprite.sprite;
                imgBackground.color = sprite.color;
                break;
            }
        }
    }
    void Update() {

        // time stuff for delaying animation
        #region time stuff
        if (delay > 0) { // don't animate for a period of time
            delay -= Time.deltaTime;
            return;
        } else {
            // keep track of how long it's been since this card was last "animated"
            // but only track for a maximum of 10 seconds
            if (timeSinceLastAnim < 10) timeSinceLastAnim += Time.deltaTime;
        }
        #endregion

        // animate
        EasePositionRotationScale();

        // respond to changes in card durability
        AssessDurability();

    }
    public void SetParentToTome() {
        transform.SetParent(tomeGUI);
    }

    private void AssessDurability() {
        float dur = card.GetDurability();
        if (dur <= 0) {
            destroyed = true;
        }
        for (int i = 0; i < damageOverlays.Length; i++) {
            Image overlay = damageOverlays[i];
            overlay.gameObject.SetActive(dur < i / (float)damageOverlays.Length);
        }
    }

    private void EasePositionRotationScale() {
        float percentRemainingAfter1Second = .001f;
        rt.localRotation = MathStuff.Damp(transform.localRotation, targetRotation, percentRemainingAfter1Second);
        rt.anchoredPosition = MathStuff.Damp(rt.anchoredPosition, targetPosition, percentRemainingAfter1Second);
        transform.localScale = Vector3.one * MathStuff.Damp(transform.localScale.x, targetScale, percentRemainingAfter1Second);
    }

    public void AnimateTo(Vector3 pos, Quaternion rot, float scale, float delay) {
        targetPosition = pos;
        targetRotation = rot;
        targetScale = scale;
        if(timeSinceLastAnim > 0.5f) this.delay = delay;
        timeSinceLastAnim = 0;
    }
}
