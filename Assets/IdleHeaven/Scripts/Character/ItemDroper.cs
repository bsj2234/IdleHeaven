using IdleHeaven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDroper : MonoBehaviour
{
    private ItemSpawner spawner;

    private void Awake()
    {
        CharacterAIController characterController = GetComponent<CharacterAIController>();
        Health health = GetComponent<Health>();
        health.OnDead.AddListener(HandleOnDead);
    }
    public void Init(ItemSpawner spawner)
    {
        this.spawner = spawner;
    }
    public void HandleOnDead(Attack attacker, Health gotHit)
    {
        DropItem(attacker, gotHit.GetComponent<Enemy>());
    }

    private void DropItem(Attack attacker, Enemy enemy)
    {
        Item generatedItem = spawner.generator.GenerateItem(new GenerateInfo(enemy.GetComponent<CharacterStats>().LevelSystem.Level, Rarity.Error));
        DroppedItem item = spawner.SpawnItem(transform, generatedItem);
        item.SetAcquirer(attacker.transform);
        ItemGrabber grabber = item.GetComponent<ItemGrabber>();
        grabber.GrabToTarget(attacker.transform);
        grabber.OnHalfway += item.TriggerAcquirable;
    }
}
