using IdleHeaven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDroper : MonoBehaviour
{
    [SerializeField] ItemSpawner spawner;

    private void Awake()
    {
        AICharacterController characterController = GetComponent<AICharacterController>();
        Health combat = GetComponent<Health>();
        combat.OnDeadWAttacker += DropItem;
    }

    public void DropItem(Attack attacker)
    {
        DroppedItem item = spawner.SpawnItem(transform, spawner.generator.GenerateItem(new GenerateInfo(10)).GetPrefab());
        item.GetComponent<ItemGrabber>().GrabToTarget(attacker.transform);
    }
}
