using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public ItemGrabber SpawnItem(bool autoGet, Transform position, GameObject itemPrefab, ICombat attacker)
    {
        GameObject itemObj = Instantiate(itemPrefab, position.position, position.rotation);
        itemObj.TryGetComponent(out ItemGrabber magnatic);
        itemObj.TryGetComponent(out DroppedItem drop);
        magnatic.enabled = autoGet;
        magnatic.Grab();
        drop.SetAcquirer(attacker.GetTransform());
        return magnatic;
    }
}
