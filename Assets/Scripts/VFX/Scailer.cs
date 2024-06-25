using System.Collections;
using UnityEngine;

public class Scailer : MonoBehaviour
{
    [SerializeField] private float _scaleSecond = 1f;
    [SerializeField] private float _scaleSize = 1.5f;
    [SerializeField] private float _scaleNormalizedPeak = 0.5f;
    [SerializeField] private bool _smooth = true;
    [SerializeField] private bool _return = true;


    Vector3 startScale;
    Vector3 targetScale;
    private void OnEnable()
    { 
        startScale = transform.localScale;
        targetScale = startScale * _scaleSize;
        StartCoroutine(Scale(_scaleSecond * _scaleNormalizedPeak, startScale, targetScale));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private IEnumerator Scale(float time, Vector3 startScale, Vector3 targetScale)
    {
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / time;
            float curveValue = _smooth ? Mathf.SmoothStep(0, 1, normalizedTime) : normalizedTime;
            transform.localScale = Vector3.Lerp(startScale, targetScale, curveValue);
            yield return null;
        }
        transform.localScale = targetScale;
        if (_return)
        {
            StartCoroutine(Scale(_scaleSecond * (1 - _scaleNormalizedPeak), targetScale, startScale));
        }
    }
}
