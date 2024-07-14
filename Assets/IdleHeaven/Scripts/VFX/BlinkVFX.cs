using IdleHeaven;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BlinkVFX : MonoBehaviour, VFX
{
    [SerializeField] float duration = 0.2f;
    [SerializeField] float interval = .05f;

    readonly float baseMagnitude = 0.1f;

    private bool isPlaying = false;

    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material _whiteMaterial;
    [SerializeField] private Material _originalMaterial;

    private Coroutine _blinkCoroutine;

    private void Awake()
    {
        if (_whiteMaterial == null)
        {
            _whiteMaterial = Resources.Load<Material>("Materials/White");
        }
        if (_originalMaterial == null)
        {
            _renderer = transform.FindComponentInChildOrSelf<Renderer>();
            if(_renderer != null)
            {
                _originalMaterial = _renderer.sharedMaterial;
            }
        }
    }

    public void Play()
    {
        Blink();
    }

    public BlinkVFX Set(float interval = .05f)
    {
        this.interval = interval;
        return this;
    }

    private void Blink()
    {
        if (isPlaying)
        {
            return;
        }
        if(gameObject.activeInHierarchy == false)
        {
            return;
        }
        _blinkCoroutine = StartCoroutine(BlinkEffectCoroutine());
    }

    public IEnumerator BlinkEffectCoroutine()
    {
        isPlaying = true;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            _renderer.material = _whiteMaterial;
            yield return new WaitForSeconds(interval);
            _renderer.material = _originalMaterial;
            elapsed += interval;
        }
        isPlaying = false;
    }

    public void HandleOnDamage(Attack attacker, AttackType attackType)
    {
        Play();
    }

    public void ResetEffect(Attack attacker, Health health)
    {
        StopCoroutine(_blinkCoroutine);
        _renderer.material = _originalMaterial;
        isPlaying = false;
    }
}
