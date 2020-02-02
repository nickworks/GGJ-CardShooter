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

    Quaternion targetRotation;
    Vector2 targetPosition;
    float targetScale = 1;
    float delay = 0;

    RectTransform rt;
    float timeSinceLastAnim = 0;

    void Start() {
        rt = transform as RectTransform;
        MakeAllTheTextsOneSided(); // oof
    }
    void MakeAllTheTextsOneSided() {
        // the best I can do right now
        TextMeshProUGUI[] txts = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI txt in txts) txt.enableCulling = true;
    }
    public void SetDepth(int i) {
        GetComponent<Canvas>().sortingOrder = i;
    }
    public void Init(Card card) {
        foreach(EffectSprites sprite in spritesForCardEffects) {
            if(sprite.cardEffect == card.effect) {
                imgBackSprite.sprite = sprite.sprite;
                imgBackground.color = sprite.color;
                break;
            }
        }
    }
    void Update() {
        if (delay > 0) {
            delay -= Time.deltaTime;
            return;
        }
        if(timeSinceLastAnim < 10) timeSinceLastAnim += Time.deltaTime;
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
