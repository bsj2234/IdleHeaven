using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButtonHoldDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public float holdDuration = 1.0f; // Duration to detect hold
    public UnityEvent onHoldComplete; // Event to trigger after hold
    public UnityEvent onHoldUp; // Event to trigger after hold
    public UnityEvent onClick; // Event to trigger on click

    private bool isHolding = false;
    private bool holdComplete = false;
    private float holdTime = 0f;

    private Coroutine holdRoutine;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isHolding)
        {
            holdRoutine = StartCoroutine(HoldButtonRoutine());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isHolding)
        {
            StopCoroutine(holdRoutine);
            if (!holdComplete)
            {
                // If the hold was not complete, treat it as a click
                onClick.Invoke();
            }
            else
            {
                onHoldUp.Invoke();
            }
            ResetHold();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isHolding)
        {
            StopCoroutine(holdRoutine);
            ResetHold();
        }
    }

    private IEnumerator HoldButtonRoutine()
    {
        isHolding = true;
        holdComplete = false;
        holdTime = 0f;

        while (holdTime < holdDuration)
        {
            holdTime += Time.deltaTime;
            yield return null;
        }
        onHoldComplete.Invoke();
        holdComplete = true;
    }

    private void ResetHold()
    {
        isHolding = false;
        holdComplete = false;
        holdTime = 0f;
    }
}
