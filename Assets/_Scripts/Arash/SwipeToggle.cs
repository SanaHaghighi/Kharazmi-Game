using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(Toggle))]
public class SwipeToggle : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("References")]
    [SerializeField] private RectTransform thumb;
    [SerializeField] private RectTransform track;

    [Header("Settings")]
    [SerializeField] private float animationDuration = 0.2f;
    [SerializeField] private float dragThreshold = 5f;
    [SerializeField] private Ease easeType = Ease.OutQuad;

    private Toggle toggle;
    private float trackWidth;
    private Vector2 startDragPosition;
    private Tween currentTween;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        trackWidth = track.rect.width - thumb.rect.width;
        UpdateThumbPosition(instant: true);
    }

    private void OnEnable()
    {
        toggle.onValueChanged.AddListener(HandleValueChanged);
    }

    private void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(HandleValueChanged);
        currentTween?.Kill();
    }

    private void HandleValueChanged(bool isOn)
    {
        UpdateThumbPosition();
    }

    private void UpdateThumbPosition(bool instant = false)
    {
        var targetX = toggle.isOn ? trackWidth / 2 : -trackWidth / 2;
        Vector2 targetPosition = new Vector2(targetX, thumb.anchoredPosition.y);

        currentTween?.Kill();

        if (instant)
        {
            thumb.anchoredPosition = targetPosition;
        }
        else
        {
            currentTween = thumb.DOAnchorPos(targetPosition, animationDuration)
                .SetEase(easeType)
                .OnKill(() => currentTween = null);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        currentTween?.Kill();
        startDragPosition = thumb.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            track,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint);

        float newX = Mathf.Clamp(
            localPoint.x,
            -trackWidth / 2,
            trackWidth / 2);

        thumb.anchoredPosition = new Vector2(newX, thumb.anchoredPosition.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float dragDistance = thumb.anchoredPosition.x - startDragPosition.x;
        bool newValue = Mathf.Abs(dragDistance) > dragThreshold
            ? dragDistance > 0
            : !toggle.isOn;

        toggle.isOn = newValue;
    }
}