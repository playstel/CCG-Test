using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Card), typeof(CardRenderer))]
public class CardDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Parents")] 
    public Transform DefaultParent;
    public Transform DefaultTempCardParent;
    
    [Header("Cache")]
    private Transform _transform, _transformTempCard, _transformCanvas;
    private Card _card;
    private CardRenderer _cardRenderer;
    
    [Header("Other")]
    private Vector2 offset;

    private void Awake()
    {
        _transform = transform;
        _transformTempCard = PlayerCards.playerCards.temporaryCard;
        _transformCanvas = PlayerCards.playerCards.canvas;
        _card = GetComponent<Card>();
        _cardRenderer = GetComponent<CardRenderer>();
        
        DefaultParent = DefaultTempCardParent = _transform.parent;
    }

    private float dragScale = 1.2f;
    public void OnBeginDrag(PointerEventData eventData)
    {
        var pos = _transform.position;
        offset = new Vector2(pos.x, pos.y) - eventData.position;

        DefaultParent = DefaultTempCardParent = _transform.parent;
        
        _transformTempCard.SetParent(DefaultParent);
        _transformTempCard.SetSiblingIndex(_transform.GetSiblingIndex());
        
        _transform.SetParent(DefaultParent.parent);

        _cardRenderer.BlocksRaycasts(false);
        _cardRenderer.EnableOutline(true);
        
        _card.Scale(_transform, dragScale);
        
        CardSounds.Sounds.SoundPick();
    }

    public void OnDrag(PointerEventData eventData)
    {
        var newPos = eventData.position;
        _transform.position = newPos + offset;

        if (_transformTempCard.parent != DefaultTempCardParent)
        {
            _transformTempCard.SetParent(DefaultTempCardParent);
        }
        
        CheckPosition(_transform);
    }

    public async void OnEndDrag(PointerEventData eventData)
    {
        _cardRenderer.BlocksRaycasts(true);
        _cardRenderer.EnableOutline(false);

        await _card.Move(_transformTempCard);
        _card.Scale(_transform);
        
        _transform.SetParent(DefaultParent);
        _transform.SetSiblingIndex(_transformTempCard.GetSiblingIndex());
        
        _transformTempCard.SetParent(_transformCanvas);
        _transformTempCard.localPosition = new Vector3(3000, 0);
        
        CardSounds.Sounds.SoundPut();
    }

    private int newSibilingIndex;
    private float posX;
    public void CheckPosition(Transform _transformObject)
    { 
        posX = _transformObject.position.x;
        
        for (var i = 0; i < DefaultTempCardParent.childCount; i++)
        {
            var currentChild = DefaultTempCardParent.GetChild(i);
            
            if (posX > currentChild.position.x)
            {
                newSibilingIndex = currentChild.GetSiblingIndex();
            }
        }
        
        _transformTempCard.SetSiblingIndex(newSibilingIndex);
    }

    public void FreeUpSpace()
    {
        for (var i = 0; i < DefaultParent.childCount; i++)
        {
            if (_transform.GetSiblingIndex() < i)
            {
                var currentChild = DefaultParent.GetChild(i);

                if (DefaultParent.GetChild(i - 1))
                {
                    var previousChildPos = DefaultParent.GetChild(i - 1).position;
                    
                    currentChild.DOMove(previousChildPos, 0.2f, true)
                        .SetEase(Ease.InQuad);
                }
            }
        }
    }
}
