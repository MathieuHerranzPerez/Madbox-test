using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : PlayerMovementInputsGetter, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    [Header("Setup")]
    [SerializeField] private RectTransform bounds = default;
    [SerializeField] private RectTransform innerStick = default;


    private Vector2 startingPoint;
    private Vector2 defaultPosition;
    private Vector2 radius;

    private Canvas canvas;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        radius = bounds.sizeDelta / 2f;
        defaultPosition = bounds.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        input = (eventData.position - startingPoint) / (radius * canvas.scaleFactor);
        float sqrMagnitude = input.sqrMagnitude;

        if (sqrMagnitude > 1)
            input = input.normalized;

        innerStick.anchoredPosition = input * radius;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsInputDetected = true;
        startingPoint = eventData.position;
        bounds.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsInputDetected = false;
        input = Vector2.zero;
        innerStick.anchoredPosition = Vector2.zero;

        bounds.position = defaultPosition;
    }
}
