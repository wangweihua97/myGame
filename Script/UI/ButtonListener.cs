using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class ButtonListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent ClickDown;
    public UnityEvent ClickUp;
    public void OnPointerDown(PointerEventData eventData)
    {
        ClickDown.Invoke();
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        ClickUp.Invoke();
    }

}