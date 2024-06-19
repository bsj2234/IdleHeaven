using IdleHeaven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDroper : MonoBehaviour
{
    [SerializeField] ItemSpawner spawner;

    private void Awake()
    {
        CharacterAIController characterController = GetComponent<CharacterAIController>();
        Health health = GetComponent<Health>();
        health.OnDeadWAttacker += DropItem;
    }

    public void DropItem(Attack attacker)
    {
        DroppedItem item = spawner.SpawnItem(transform, spawner.generator.GenerateItem(new GenerateInfo(10)).GetPrefab());
        item.GetComponent<ItemGrabber>().GrabToTarget(attacker.transform);
    }
}
