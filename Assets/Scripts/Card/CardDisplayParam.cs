using TMPro;
using UnityEngine;
using DG.Tweening;

public class CardDisplayParam : MonoBehaviour
{
    [Header("Type")]
    public CardParamType currentParamType;

    public enum CardParamType
    {
        Attack, Health, Mana
    }
    
    [Header("Text")]
    private TextMeshProUGUI cardValueText;
    private Transform _transform;
    
    private void Awake()
    {
        cardValueText = GetComponentInChildren<TextMeshProUGUI>();
        _transform = GetComponent<Transform>();
    }

    #region Start Info

    public void SetStartInfo(CardInfo cardInfo)
    {
        if (!cardInfo)
        {
            Debug.LogError("Failed to find cardInfo"); return;
        }

        if (!cardValueText)
        {
            Debug.LogError("Failed to find cardValueText"); return;
        }
        
        switch (currentParamType)
        {
            case CardParamType.Attack:
                cardValueText.text = cardInfo._cardAttackValueMax.ToString();
                break;
            case CardParamType.Health:
                cardValueText.text = cardInfo._cardHealthValueMax.ToString();
                break;
            case CardParamType.Mana:
                cardValueText.text = cardInfo._cardManaValueMax.ToString();
                break;
            default:
                cardValueText.text = null;
                break;
        }
    }

    #endregion
    
    public void EditValue(float value)
    {
        cardValueText.text = value.ToString();
        DoShake();
        CardSounds.Sounds.SoundEdit();
    }
    
    private void DoShake(float duration = 0.4f, float strength = 2f)
    {
        _transform.DOShakePosition(duration, strength);
        _transform.DOShakeRotation(duration, strength);
    }
}
