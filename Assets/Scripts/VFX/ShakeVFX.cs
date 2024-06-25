using IdleHeaven;
using System.Collections;
using UnityEngine;

public class ShakeVFX : MonoBehaviour, VFX
{
    [SerializeField] float duration = 0.5f;
    [SerializeField] float magnitude = 1f;

    readonly float baseMagnitude = 0.1f;

    private bool isPlaying = false;

    public void Play()
    {
        Shake();
    }

    public ShakeVFX Set(float muliply = 1f)
    {
        magnitude = muliply;
        return this;
    }

    private void Shake()
    {
        if (isPlaying)
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
            float x = Random.Range(-1f, 1f) * magnitude * baseMagnitude + originalPosition.x;
            float y = Random.Range(-1f, 1f) * magnitude * baseMagnitude + originalPosition.y;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        isPlaying = false;
        transform.localPosition = originalPosition;
    }

    public void HandleOnDamage(Attack attacker, AttackType attackType)
    {
        switch (attackType)
        {
            case AttackType.None:
                Set(0.3f)
                    .Play();
                break;
            case AttackType.Melee:
                Set(0.7f)
                    .Play();
                break;
            case AttackType.ChargedMelee:
                Set(1.5f)
                    .Play();
                break;
            default:
                break;
        }
    }
}
