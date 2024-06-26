using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageUIManager : MonoSingleton<DamageUIManager>
{
    [SerializeField] GameObject damageUIPrefab;
    [SerializeField] Canvas canvas_parent;
    private Canvas canvas;
    [SerializeField] float _lifeTime = 2f;

    private void Start()
    {
        canvas = new GameObject("Canvas_DamageUi").AddComponent<Canvas>();
        canvas.transform.SetParent(canvas_parent.transform);
    }

    public void ShowDamage(Transform targetTrf, float damageAmount, Color color)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTrf.position);

        IPooledObject damageUIInstance = ObjectPoolingManager.Instance.SpawnFromPool(damageUIPrefab, screenPosition, Quaternion.identity);
        PooledDamageUi setTarget = damageUIInstance as PooledDamageUi;
        damageUIInstance.Init(damageUIPrefab);
        damageUIInstance.transform.SetParent(canvas.transform);
        setTarget.Init(damageAmount, color, targetTrf);

        ObjectPoolingManager.Instance.ReturnToPool(damageUIPrefab, damageUIInstance, _lifeTime);
    }
}