using CartoonFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiDrawerEffect : MonoBehaviour
{
    Vector3 _targetPosition;
    Vector3 _hidePosition;
    bool _isShown = false;

    RectTransform _rect;
    private void Start()
    {
        _rect = GetComponent<RectTransform>();
        _targetPosition = _rect.anchoredPosition;
        _hidePosition = new Vector3(_targetPosition.x, -50f, _targetPosition.z);
        transform.position = _hidePosition;
    }

    public void TriggerDrawer()
    {
        StopAllCoroutines();
        if(_isShown)
        {
            StartCoroutine(HideDrawerCoroutine());
        }
        else
        {
            StartCoroutine(ShowDrawerCoroutine());
        }
        _isShown = !_isShown;
    }
    private IEnumerator ShowDrawerCoroutine()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 5;
            _rect.anchoredPosition = Vector3.Lerp(_hidePosition, _targetPosition, t);
            yield return null;
        }
    }
    private IEnumerator HideDrawerCoroutine()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 5;
            _rect.anchoredPosition = Vector3.Lerp(_targetPosition, _hidePosition, t);
            yield return null;
        }
    }
}
