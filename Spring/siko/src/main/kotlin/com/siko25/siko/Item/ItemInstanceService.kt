package com.siko25.siko.item

import com.siko25.siko.character.player.inventory.InventoryService
import com.siko25.siko.item.effect.EffectDataRepository
import com.siko25.siko.item.effect.EffectSetRepository
import com.siko25.siko.random.*
import org.springframework.stereotype.Service

@Service
class ItemInstanceService(
        private val itemInstanceRepository: ItemInstanceRepository,
        private val effectSetRepository: EffectSetRepository,
        private val randomService: RandomService,
        private val effectDataRepository: EffectDataRepository,
        private val inventoryService: InventoryService,
        private val idCounter: Long = 0
) {

    fun createItemInstance(itemData: ItemData): ItemInstance? {
        val generatedItem = generateItem(itemData)
        if (generatedItem == null) {
            throw Exception("Failed to generate item instance")
        }
        itemInstanceRepository.save(generatedItem)
        return generatedItem
    }

    fun getItemInstance(id: String): ItemInstance? {
        return itemInstanceRepository.findById(id).orElseGet(null)
    }

    private fun generateItem(itemData: ItemData): ItemInstance? {

        val effectSet = effectSetRepository.findById(itemData.effectSet).orElseGet(null)
        if (effectSet == null) {
            return null
        }
        val effects = calculateItemEffect(effectSet.effects)
        val itemInstance =
                ItemInstance(idCounter.toString(), itemData.name, itemData.id, effects, 1)
        return itemInstance
    }

    private fun calculateItemEffect(effectSet: Array<WeightedItem>): Array<String> {
        val effect = randomService.getRandomWeightedItem(effectSet)
        if (effect == null) {
            return arrayOf()
        }
        var result = Array<String>(3) { "" }
        for (i in result.indices) {
            val rand = randomService.getRandomWeightedItem(effectSet)
            if (rand == null) {
                continue
            }
            val effectData = effectDataRepository.findById(rand.item).orElseGet(null)
            if (effectData == null) {
                continue
            }
            result[i] = effectData.id
        }
        return result
    }
}
