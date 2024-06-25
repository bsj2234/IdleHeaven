using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeVFX : MonoBehaviour, VFX
{
    [SerializeField] float duration = 0.5f;
    [SerializeField] float magnitude = 0.1f;

    private bool isPlaying = false;

    public void Play()
    {
        Shake();
    }

    public void Set(float muliply = 1f)
    {
        magnitude *= muliply;
    }

    private void Shake()
    {
        if(isPlaying)
        {
            return;
        }
        StartCoroutine(ShakeEffectCoroutine());
    }

    IEnumerator ShakeEffectCoroutine()
    {
        isPlaying = true;
        Vector3 originalPosition = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude + originalPosition.x;
            float y = Random.Range(-1f, 1f) * magnitude + originalPosition.y;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        isPlaying = false;
        transform.localPosition = originalPosition;
    }
}
