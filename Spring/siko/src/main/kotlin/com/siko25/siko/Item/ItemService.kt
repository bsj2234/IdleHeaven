package com.siko25.siko.item

import com.siko25.siko.character.player.Player
import com.siko25.siko.character.player.inventory.InventoryService
import com.siko25.siko.item.effect.*
import com.siko25.siko.item.itemdrop.StageDropSetData
import com.siko25.siko.random.*
import org.springframework.stereotype.Service

@Service
class ItemService(
        private val itemInstanceRepository: ItemRepository,
        private val effectSetRepository: EffectSetRepository,
        private val randomService: RandomService,
        private val effectDataRepository: EffectDataRepository,
        private val inventoryService: InventoryService,
        private val idCounter: Long = 0,
        private val effectService: EffectService,
        private val itemDataRepository: ItemDataRepository,
        private val itemComponentFactory: ItemComponentFactory,
        private val itemFactory: ItemFactory
) {

    fun getDropItem(stageDropSet: StageDropSetData): Item? {
        val stageDrop = stageDropSet.stageDropSet
        val dropSet = stageDrop.dropSets
        val itemData = randomService.getRandomWeightedItem(dropSet)
        val dropItem = generateItem(itemData)
        return dropItem
    }
    fun getClearItems(stageDropSet: StageDropSetData): Item? {
        val clearDrop = stageDropSet.clearDropSet
        val dropSet = clearDrop.dropSets
        val itemData = randomService.getRandomWeightedItem(dropSet)
        val dropItem = generateItem(itemData)
        return dropItem
    }

    fun createItem(itemData: ItemData): Item? {
        val generatedItem = generateItem(itemData)
        itemInstanceRepository.save(generatedItem)
        return generatedItem
    }
    private fun generateItem(itemData: ItemData): Item {
        val item = itemFactory.createItem(itemData)
        return item
    }

    fun getItem(id: String): Item? {
        return itemInstanceRepository.findById(id).orElseGet(null)
    }

    fun tryDropItem(player: Player, stageDropSet: StageDropSetData) {
        val dropItem = getDropItem(stageDropSet)
        if (dropItem != null) {
            inventoryService.addItem(player.id, dropItem)
        }
    }

    fun findEffectSetByName(effectSetName: String): EffectSet {
        return effectSetRepository.findByName(effectSetName)
                ?: throw Exception("Effect set not found")
    }
    fun findItemById(ownerId: String): Item {
        return itemInstanceRepository.findById(ownerId).orElseThrow { Exception("Item not found") }
    }

    fun getItemData(dataId: Long): ItemData {
        val itemData =
                itemDataRepository.findById(dataId).orElseThrow { Exception("Item data not found") }
        return itemData
    }

    fun getDataOwner(ownerName: String): ItemData {
        return itemDataRepository.findByName(ownerName) ?: throw Exception("Owner not found")
    }
}
