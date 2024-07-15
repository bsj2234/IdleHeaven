using IdleHeaven;
using System.Collections;
using UnityEngine;

public class ShakeVfx : MonoBehaviour, Vfx
{
    [SerializeField] float duration = 0.2f;
    [SerializeField] float magnitude = 1f;

    readonly float baseMagnitude = 0.1f;

    private bool isPlaying = false;

    public void Play()
    {
        Shake();
    }

    public ShakeVfx Set(float muliply = 1f)
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
        if(gameObject.activeInHierarchy == false)
        {
            return;
        }
        StartCoroutine(ShakeEffectCoroutine());
    }

    public IEnumerator ShakeEffectCoroutine()
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
                Set(2f)
                    .Play();
                break;
            case AttackType.Melee:
                Set(3f)
                    .Play();
                break;
            case AttackType.ChargedMelee:
                Set(4f)
                    .Play();
                break;
            default:
                break;
        }
    }
}
