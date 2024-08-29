 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] RectTransform ThumbStickTrans;
    [SerializeField] RectTransform BackgroundTrans;
    [SerializeField] RectTransform CenterTrans;

    public delegate void OnStickInputValueUpdated(Vector2 inputVal);
    public delegate void OnStickTaped();

    public event OnStickInputValueUpdated onStickValueUpdated;
    public event OnStickTaped onStickTaped;

    bool bWasDragging;
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log($"On Drag Fired {eventData.position}");
        Vector2 TouchPos = eventData.position;
        Vector2 centerPos = BackgroundTrans.position;

        Vector2 localOffset = Vector2.ClampMagnitude(TouchPos - centerPos, BackgroundTrans.sizeDelta.x/2);
        
        Vector2 inputVal = localOffset / (BackgroundTrans.sizeDelta.x / 2);

        ThumbStickTrans.position = centerPos + localOffset;
        onStickValueUpdated?.Invoke(inputVal);
        bWasDragging = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        BackgroundTrans.position = eventData.position;
        ThumbStickTrans.position = eventData.position;
        bWasDragging = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        BackgroundTrans.position = CenterTrans.position;
        ThumbStickTrans.position = BackgroundTrans.position;
        onStickValueUpdated?.Invoke(Vector2.zero);
        if(!bWasDragging)
        {
            onStickTaped?.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
