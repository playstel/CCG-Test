using UnityEngine;
using UnityEngine.EventSystems;

public class DropField : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Transform _transform;
    private IPointerEnterHandler _pointerEnterHandlerImplementation;
    private IPointerExitHandler _pointerExitHandlerImplementation;

    private void Awake()
    {
        _transform = transform;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var cardDrag = eventData.pointerDrag.GetComponent<CardDrag>();

        if (cardDrag)
        {
            cardDrag.DefaultParent = _transform;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        
        var cardDrag = eventData.pointerDrag.GetComponent<CardDrag>();
        
        if (cardDrag)
        {
            cardDrag.DefaultTempCardParent = _transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    { 
        if (eventData.pointerDrag == null) return;
        
        var cardDrag = eventData.pointerDrag.GetComponent<CardDrag>();
        
        if (cardDrag && cardDrag.DefaultTempCardParent == _transform)
        {
            cardDrag.DefaultTempCardParent = cardDrag.DefaultParent;
        }
    }
}
