using System.Collections.Generic;
using UnityEngine;

public class PlayerCards : MonoBehaviour
{
    public static PlayerCards playerCards;
    
    [Header("Cards")]
    private static List<Card> cards = new List<Card>();

    [Header("Setup")] 
    public Transform temporaryCard;
    public Transform canvas;

    public void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        if (!playerCards)
        {
            playerCards = this;
        }
        else
        {
            if (playerCards != this)
            {
                Debug.Log("Destroy " + name);
                Destroy(playerCards.gameObject);
                playerCards = this;
            }
        }
    }

    public static void AddCard(Card card)
    {
        if (!cards.Contains(card)) cards.Add(card);
    }

    public static List<Card> GetPlayerCards()
    {
        return cards;
    }

    public static async void RemoveCard(Card card)
    {
        card.CardDrag.FreeUpSpace();
        await card.RemoveCardTween();
        cards.Remove(card);
        Destroy(card.gameObject);
    }

    public static void RemoveExistingCards()
    {
        for (var i = 0; i < cards.Count; i++)
        {
            RemoveCard(cards[i]);
        }
    }
}
