using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Button))]
public class PlayerCardsRandomizer : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private int _minRandomValue = -2;
    [SerializeField] private int _maxRandomValue = 9;
    [SerializeField] private int _iterationCount = 2;

    [Header("Button")]
    Button button;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => ChangeSequenceStart());
    }

    private async void ChangeSequenceStart()
    {
        ButtonShake();
        ButtonInteractible(false);
        CardSounds.Sounds.SoundClick();

        for (var i = 0; i < _iterationCount; i++)
        {
            await ChangeIteration();
        }
        
        ButtonInteractible(true);
    }

    private const int _changeDelay = 200;
    private async UniTask ChangeIteration()
    {
        var cards = PlayerCards.GetPlayerCards();

        for (int i = 0; i < cards.Count; i++)
        {
            var card = cards[i];
            
            if(card == null) continue;
            
            card.ChangeRandomParameter(GetRandomParamValue());
            await UniTask.Delay(_changeDelay);
        }
    }
    
    private int GetRandomParamValue()
    {
        return Random.Range(_minRandomValue, _maxRandomValue);
    }
    
    private void ButtonInteractible(bool state)
    {
        button.interactable = state;
    }

    private void ButtonShake()
    {
        button.transform.DOShakePosition(0.2f, Vector3.down * 10);
        button.transform.DOShakeRotation(0.2f, Vector3.down * 10);
    }
}
