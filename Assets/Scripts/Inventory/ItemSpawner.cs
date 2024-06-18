using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public void SpawnItem(bool autoGet, Transform position, GameObject itemPrefab)
    {
        GameObject itemObj = Instantiate(itemPrefab, position.position, position.rotation);
        itemObj.TryGetComponent(out ItemGrabber magnatic);
        magnatic.enabled = autoGet;
    }
}
