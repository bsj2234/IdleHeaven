using TMPro;
using UnityEngine;

public class PooledDamageUi : PooledObject
{
    Vector3 targetPos;
    [SerializeField] TMP_Text Text_damage;
    [SerializeField] Scailer _Scailer;
    [SerializeField] Transflator _translator;
    public void Init(float damageAmount, Color color, Transform Target)
    {
        if (Text_damage != null)
        {
            Text_damage.text = damageAmount.ToString("F2");
            Text_damage.color = color;
        }
        targetPos = Target.position;
    }

    private void Update()
    {
        Vector3 BendiedPosition = BendingManager.Instance.ModifiedPosition(targetPos);
        transform.position = Camera.main.WorldToScreenPoint(BendiedPosition);
    }
}
