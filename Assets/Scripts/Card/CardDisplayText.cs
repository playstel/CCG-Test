using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CardDisplayText : MonoBehaviour
{
    [Header("Type")]
    public CardTextType currentTextType;

    public enum CardTextType
    {
        Name, Description
    }
    
    [Header("Text")]
    private TextMeshProUGUI cardText;

    private void Awake()
    {
        cardText = GetComponent<TextMeshProUGUI>();
    }

    #region Start Info

    public void SetStartInfo(CardInfo cardInfo)
    {
        if (!cardInfo)
        {
            Debug.LogError("Failed to find cardInfo"); return;
        }

        if (!cardText)
        {
            Debug.LogError("Failed to find cardText"); return;
        }

        switch (currentTextType)
        {
            case CardTextType.Name:
                cardText.text = cardInfo._cardName;
                break;
            case CardTextType.Description:
                cardText.text = cardInfo._cardDescription;
                break;
            default:
                cardText.text = null;
                break;
        }
    }

    #endregion
}
