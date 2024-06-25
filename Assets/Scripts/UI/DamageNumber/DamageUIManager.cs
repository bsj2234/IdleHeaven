using UnityEngine;
using UnityEngine.UI;

public class DamageUIManager : MonoBehaviour
{
    private GameObject damageUIPrefab;
    private Canvas canvas;

    private void Start()
    {
        canvas = new GameObject("Canvas_DamageUi").AddComponent<Canvas>();
    }

    public void ShowDamage(Vector3 worldPosition, int damageAmount)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        GameObject damageUIInstance = Instantiate(damageUIPrefab, canvas.transform);

        damageUIInstance.transform.position = screenPosition;

        Text damageText = damageUIInstance.GetComponent<Text>();
        if (damageText != null)
        {
            damageText.text = damageAmount.ToString();
        }

        Destroy(damageUIInstance, 2f);
    }
}