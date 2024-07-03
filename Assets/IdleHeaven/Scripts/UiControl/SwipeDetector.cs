using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
//Todo 자식(자신위에뜨워진 UI)무시하고 인풋 받는것 하려다
//정지 시간이 많이 소요될것같아 나중으로) 자식에
//모두 스크립트를 추가해서 감시하는게 제일 좋을지도
//아니면 클릭을 자식을 잠시 꺼두었다가 스와이프가 이니면 자식을 강제로 클릭?

public class SwipeDetector : MonoBehaviour, IDragHandler,IBeginDragHandler, IEndDragHandler, IPointerUpHandler, IPointerDownHandler
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


    public void OnBeginDrag(PointerEventData eventData)
    {
        startDragPosition = eventData.position;
        Debug.Log("Start Drag Position: " + startDragPosition);
    }
    public void OnDrag(PointerEventData eventData)
    {
        // No implementation needed for drag event
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        DetectSwipe(eventData);
        Debug.Log("End Drag Position: " + eventData.position);
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
        Debug.Log("Angle: " + angle);
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

    public void OnPointerUp(PointerEventData eventData)
    {
        OnEndDrag(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnBeginDrag(eventData);
    }
}
