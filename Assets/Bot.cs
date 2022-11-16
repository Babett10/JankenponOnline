using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public CardPlayer player;
    public CardGameManager gameManager;
    public float choosingInterval;

    private float timer = 0;

    int lastSelected = 0;
    Cards[] cards;

    private void Start()
    {
        cards = GetComponentsInChildren<Cards>();
    }
    
   
    void Update()
    {
        if (gameManager.State != CardGameManager.GameState.ChooseAttack )
        {
            timer = 0;
            return;
        }
        if (timer < choosingInterval)
        {
            timer += Time.deltaTime;
            return;
        }

        timer = 0;
        ChooseAttack();
    }

    public void ChooseAttack()
    {
        var random = Random.Range(1, cards.Length);        
        var selection = (lastSelected + random) % cards.Length;
        //last + random % length = value
        //(0 + 1 ) % 3 = 1
        //(0 + 2 ) % 3 = 2
        //(0 + 1 ) % 3 = 2
        //(0 + 2 ) % 3 = 0
        //(0 + 1 ) % 3 = 0
        //(0 + 2 ) % 3 = 1
        player.SetChosenCard(cards[selection]);
        lastSelected = selection;
    }
}
