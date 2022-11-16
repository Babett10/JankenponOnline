using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CardPlayer : MonoBehaviour
{
    public Transform atkPosRef;
    public Cards chosenCard;
    [SerializeField] private TMP_Text nameText;
    public TMP_Text healthText;
    public HealthBar healthBar;
    public float Health;
    public float MaxHealth;

    private Tweener animationTweener;

    public TMP_Text NickName { get => nameText; }

    public void Start()
    {
        Health = MaxHealth;
    }

    public Attack? AttackValue
    {
        get => chosenCard == null ? null : chosenCard.AttackValue;
        // get
        // {
        //    if(chosenCard = null)
        //       return null;
        //    else 
        //       return chosenCard.AttackValue;
        // }
    }

    public void Reset()
    {
        if (chosenCard != null)
        {
            chosenCard.Reset();
        }
        chosenCard = null;
    }

    public void SetChosenCard(Cards newCard)
    {
        if (chosenCard != null)
        {
            chosenCard.Reset();
        }

        chosenCard = newCard;
        chosenCard.transform.DOScale(chosenCard.transform.localScale * 1.2f, 0.2f);
    }

    public void ChangeHealth(float amount)
    {
        Health += amount;
        Health = Mathf.Clamp(Health, 0, 100);

        // healthbar 
        healthBar.UpdateBar(Health / MaxHealth);
        //text
        healthText.text = Health + "/" + MaxHealth;
    }

    public void AnimateAttack()
    {
        animationTweener = chosenCard.transform
        .DOMove(atkPosRef.position, 0.5f);
    }

    public void AnimateDamage()
    {
        var image = chosenCard.GetComponent<Image>();
        animationTweener = image
        .DOColor(Color.red, 0.1f)
        .SetLoops(3, LoopType.Yoyo)
        .SetDelay(0.2f);
    }

    public void AnimateDraw()
    {
        animationTweener = chosenCard.transform
        .DOMove(chosenCard.OriginalPosition, 1)
        .SetEase(Ease.InBack)     //bagian ini perlu di ganti biar beda (animasi)
        .SetDelay(0.2f);
    }
    public bool IsAnimating()
    {
        return animationTweener.IsActive();
    }

    public void isClickable(bool value)
    {
        Cards[] card = GetComponentsInChildren<Cards>();
        foreach (var cards in card)
        {
            cards.SetClickable(value);
        }
    }
}
