using UnityEngine;

[RequireComponent(typeof(CardDisplay))]
public class CardParams : MonoBehaviour
{
    public int currentHealth { get; private set; }
    public int currentAttack { get; private set; }
    public int currentMana { get; private set; }
    
    private CardDisplay CardDisplay;
    private const int minValue = 0;
    
    public void Awake()
    {
        CardDisplay = GetComponent<CardDisplay>();
    }

    public void UpdateStartParams(CardInfo info)
    {
        currentHealth = info._cardHealthValueMax;
        currentAttack = info._cardAttackValueMax;
        currentMana = info._cardManaValueMax;
    }

    public void EditMana(int value)
    {
        currentMana += value;
        
        if (currentMana <= minValue)
        {
            //currentMana = minValue;
        }
        
        CardDisplay.Edit(CardDisplayParam.CardParamType.Mana, currentMana);
    }
    
    public void EditAttack(int value)
    {
        currentAttack += value;
        
        if (currentAttack <= minValue)
        {
            //currentAttack = minValue;
        }
        
        CardDisplay.Edit(CardDisplayParam.CardParamType.Attack, currentAttack);
    }

    public void EditHealth(int value)
    {
        currentHealth += value;
        
        if (currentHealth <= minValue)
        {
            PlayerCards.RemoveCard(GetComponent<Card>()); return;
        }
        
        CardDisplay.Edit(CardDisplayParam.CardParamType.Health, currentHealth);
    }
}
