using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SwipeDetector : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public enum SwipeDirection
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    public float minSwipeDistance = 50f; // Minimum swipe distance to be considered a swipe
    private Vector2 startDragPosition;
    private SwipeDirection swipeDirection;

    public UnityEvent<SwipeDirection> OnSwipe;
    public UnityEvent OnSwipeLeft;
    public UnityEvent OnSwipeRight;
    public UnityEvent OnSwipeUp;
    public UnityEvent OnSwipeDown;

    public void OnDrag(PointerEventData eventData)
    {
        // No implementation needed for drag event
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DetectSwipe(eventData);
    }

    private void DetectSwipe(PointerEventData eventData)
    {
        Vector2 endDragPosition = eventData.position;
        Vector2 swipeVector = endDragPosition - startDragPosition;

        if (swipeVector.magnitude < minSwipeDistance)
        {
            swipeDirection = SwipeDirection.None;
            return;
        }

        float angle = Vector2.Angle(Vector2.up, swipeVector);
        if (angle < 45)
        {
            swipeDirection = SwipeDirection.Up;
        }
        else if (angle > 135)
        {
            swipeDirection = SwipeDirection.Down;
        }
        else if (Vector2.Dot(Vector2.right, swipeVector) > 0)
        {
            swipeDirection = SwipeDirection.Right;
        }
        else
        {
            swipeDirection = SwipeDirection.Left;
        }

        OnSwipe?.Invoke(swipeDirection);
    }

    void Start()
    {
        swipeDirection = SwipeDirection.None;
        // Add a listener for the swipe event
        OnSwipe.AddListener(HandleSwipe);
    }

    public void HandleSwipe(SwipeDirection direction)
    {
        switch (direction)
        {
            case SwipeDirection.Up:
                OnSwipeUp?.Invoke();
                break;
            case SwipeDirection.Down:
                OnSwipeDown?.Invoke();
                break;
            case SwipeDirection.Left:
                OnSwipeLeft?.Invoke();
                break;
            case SwipeDirection.Right:
                OnSwipeRight?.Invoke();
                break;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startDragPosition = eventData.position;
    }
}
