using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] float speed;
    Vector3 initScale; //make sure the player starts right-wards
    Tween moveTween;
    Tween scaleTween;
    public List<GraphicRaycaster> raycasters;
    public EventSystem eventSystem;
    private void Start()
    {
        initScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    public bool IsPointerOverUI()
    {
        if (eventSystem == null || raycasters == null)
            return false;

        PointerEventData eventData = new PointerEventData(eventSystem);

        // Check mouse input
        if (Input.mousePresent)
        {
            eventData.position = Input.mousePosition;
            foreach (GraphicRaycaster r in raycasters)
            {
                if (RaycastUI(eventData, r)) 
                    return true;
            };
        }

        // Check touch input
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                eventData.position = touch.position;
                foreach (GraphicRaycaster r in raycasters)
                {
                    if (RaycastUI(eventData, r)) return true;
                }
            }
        }

        return false;
    }

    private bool RaycastUI(PointerEventData eventData, GraphicRaycaster raycaster)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results);
        foreach (RaycastResult r in results)
        {
            if (!r.gameObject.CompareTag("BG")) return true;
        }
        return false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI()) // For mouse click
        {
            Vector3 worldPosition = GetWorldPosition(Input.mousePosition);
            MoveToPoint(worldPosition);
        }
        else
        {
            if (Input.touchCount > 0) // For touch
            {
                Touch touch = Input.GetTouch(0);
                Vector3 worldPosition = GetWorldPosition(touch.position);
                MoveToPoint(worldPosition);
            }
        }
    }
    void MoveToPoint(Vector3 point)
    {
        moveTween?.Kill();
        if(point.x < transform.position.x)
        {
            transform.localScale=new Vector3(-initScale.x, initScale.y, initScale.z);
        }
        else
        {
            transform.localScale = new Vector3(initScale.x, initScale.y, initScale.z);
        }
        moveTween=transform.DOMoveX(point.x, speed).SetSpeedBased(true);
    }
    Vector3 GetWorldPosition(Vector3 screenPosition)
    {
        Camera mainCamera = Camera.main;
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(
            new Vector3(screenPosition.x, screenPosition.y, mainCamera.nearClipPlane)
        );
        return worldPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rain"))
        {
            var rc = collision.gameObject.GetComponent<RainingCollectible>();
            RainingCollectibleManager.Instance.ClickedRain(rc);
            scaleTween?.Kill(true);
            scaleTween = transform.DOPunchScale(transform.localScale * 0.1f, 0.1f);
        }
    }
}
