using TMPro;
using UnityEngine;

public class PooledDamageUi : PooledObject
{
    Vector3 targetPos;
    public void Init(float damageAmount, Color color, Transform Target)
    {
        TMP_Text damageText = GetComponent<TMP_Text>();
        if (damageText != null)
        {
            damageText.text = damageAmount.ToString("F2");
            damageText.color = color;
        }
        targetPos = Target.position;
    }

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(targetPos);
    }
}
