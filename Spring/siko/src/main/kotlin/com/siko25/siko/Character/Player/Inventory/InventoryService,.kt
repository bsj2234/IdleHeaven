package com.siko25.siko.character.player.inventory

import com.siko25.siko.item.Item
import org.springframework.stereotype.Service

@Service
class InventoryService(
        private val inventoryRepository: InventoryRepositoryRedis,
) {
    fun createInventory(inventory: Inventory) {
        inventoryRepository.save(inventory)
    }

    fun addItem(playerId: String, itemInstance: Item) {
        val inventory = getInventory(playerId)
        if (inventory == null) {
            return
        }
        inventory.items.add(itemInstance)
        inventoryRepository.save(inventory)
    }

    fun getInventory(playerId: String): Inventory? {
        return inventoryRepository.findById(playerId).orElseGet(null)
    }
}
