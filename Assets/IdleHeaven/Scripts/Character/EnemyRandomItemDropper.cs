using IdleHeaven;
using UnityEngine;

public class EnemyRandomItemDropper : MonoBehaviour
{
    private ItemSpawnManager spawner;

    private void Awake()
    {
        CharacterAIController characterController = GetComponent<CharacterAIController>();
        Health health = GetComponent<Health>();
        health.OnDead.AddListener(HandleOnDead);
        spawner = ItemSpawnManager.Instance;
    }
    public void HandleOnDead(Attack attacker, Health gotHit)
    {
        DropItem(attacker, gotHit.GetComponent<Enemy>());
    }

    private void DropItem(Attack attacker, Enemy enemy)
    {
        Item generatedItem = spawner.generator.GenerateItem(enemy.GetComponent<CharacterStats>().LevelSystem.Level);
        if (generatedItem == null)
        {
            return;
        }
        DroppedItem item = spawner.SpawnItem(transform, generatedItem);


        item.SetAcquirer(attacker.transform);

        ItemGrabber grabber = item.GetComponent<ItemGrabber>();
        grabber.GrabToTarget(attacker.transform);
        grabber.OnHalfway += item.TriggerAcquirable;
    }
}
