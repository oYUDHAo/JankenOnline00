using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public Player player;
    public GameManager gameManager;
    public float choosingInterval;

    private float timer = 0;

    int lastSelected = 0;
    Card[] cards;

    void Start()
    {
        cards = GetComponentsInChildren<Card>();
    }

    void Update()
    {
        if (gameManager.State != GameManager.GameState.ChoseAttack)
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
        lastSelected = selection;
        player.SetChosenCard(cards[selection]);
    }
}
