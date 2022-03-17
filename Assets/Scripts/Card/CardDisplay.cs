using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    [Header("Elements")]
    private List<CardDisplayText> displayTextList = new List<CardDisplayText>();
    private List<CardDisplayParam> displayParamsList = new List<CardDisplayParam>();

    #region Update Card Displays

    public void UpdateCardDisplays(CardInfo info)
    {
        UpdateTextInfo(info);
        UpdateValueInfo(info);
        UpdateDisplayArt(info);
    }

    private void UpdateTextInfo(CardInfo info)
    {
        displayTextList = transform.GetComponentsInChildren<CardDisplayText>().ToList();
        
        for (var i = 0; i < displayTextList.Count; i++)
        {
            displayTextList[i].SetStartInfo(info);
        }
    }

    private void UpdateValueInfo(CardInfo info)
    {
        displayParamsList = transform.GetComponentsInChildren<CardDisplayParam>().ToList();
        
        for (var i = 0; i < displayParamsList.Count; i++)
        {
            displayParamsList[i].SetStartInfo(info);
        }
    }

    private void UpdateDisplayArt(CardInfo info)
    {
        var art = transform.GetComponentInChildren<CardDisplayArt>();
        if(!art) { Debug.LogError("Failder to find CardDisplayArt in children"); return;} 
        art.GetArtSprite(info);
    }

    #endregion

    public void Edit(CardDisplayParam.CardParamType paramType, float value)
    {
        if (displayParamsList == null)
        {
            Debug.LogError("Failed to find displayValueList");
            return;
        }
        
        for (var i = 0; i < displayParamsList.Count; i++)
        {
            var displayValue = displayParamsList[i];
            
            if (displayValue.currentParamType == paramType)
            {
                displayValue.EditValue(value);
            }
        }
    }
}
