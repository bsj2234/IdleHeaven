using UnityEngine;

public class ItemAcquirer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Test Trigger {other.gameObject.name}");
        //뭘로 판단할까
        if (other.TryGetComponent(out DroppedItem droppedItem))
        {
            if (droppedItem.GetAcquirer() == transform.parent)
            {
                Debug.Log("ItemAcquired");
                Destroy(droppedItem.gameObject);
            }
        }
    }
}
