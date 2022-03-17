using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CardDisplay), typeof(CardParams), typeof(CardDrag))]
public class Card : MonoBehaviour
{
    [Header("Info")]
    public CardInfo CardInfo;
    
    [HideInInspector] 
    public CardDrag CardDrag;
    private CardDisplay CardDisplay;
    private CardParams CardParams;
    private CardRenderer CardRenderer;
    
    private Transform _transform;

    private void Awake()
    {
        CardDisplay = GetComponent<CardDisplay>();
        CardParams = GetComponent<CardParams>();
        CardDrag = GetComponent<CardDrag>();
        CardRenderer = GetComponent<CardRenderer>();
        
        _transform = transform;
    }

    public void InitializeCard(CardInfo info)
    {
        if (!info) { Debug.LogError("Failed to find CardInfo"); return; }
        
        CardInfo = info;
        
        PlayerCards.AddCard(this);

        CardParams.UpdateStartParams(info);
        CardDisplay.UpdateCardDisplays(info);
    }

    #region Random Change

    private const int cardParamsCount = 3;
    public void ChangeRandomParameter(int randomValue)
    {
        var randomParamNumbler = Random.Range(0, cardParamsCount);

        switch (randomParamNumbler)
        {
            case 0: CardParams.EditAttack(randomValue); break;
            case 1: CardParams.EditHealth(randomValue); break;
            case 2: CardParams.EditMana(randomValue); break;
            default: Debug.LogError("Random parameter number is out of range"); return;
        }
    }

    #endregion

    #region Tween

    public async UniTask RemoveCardTween()
    {
        ShakeUp();
        CardRenderer.EnableTransparency();
        
        await _transform.DOMove(_transform.position + Vector3.up * 150, 0.2f, true)
            .SetEase(Ease.InQuad)
            .AsyncWaitForCompletion();
        
        CardSounds.Sounds.SoundRemove();
    }
    
    public async UniTask StartingPunch()
    {
        CardRenderer.DisableTransparency();
        
        await _transform.DOPunchPosition(Vector3.up * 150, 0.15f)
            .SetEase(Ease.InQuad)
            .AsyncWaitForCompletion();

        CardSounds.Sounds.SoundSpawn();

        ShakeUp();
    }

    public async UniTask Move(Transform _transformTarget)
    {
        await _transform.DOMove(_transformTarget.position, 0.15f, true)
            .SetEase(Ease.InQuad)
            .AsyncWaitForCompletion();
    }

    public async UniTask Scale(Transform _transform, float endValue = 1f, float duration = 0.2f)
    {
        _transform.DOScale(endValue, duration).AsyncWaitForCompletion();
    }

    public void ShakeUp(float duration = 0.2f, float strength = 40f)
    {
        _transform.DOShakePosition(duration, Vector3.up * strength);
        _transform.DOShakeRotation(duration, Vector3.up * strength);
    }

    #endregion
    
}
