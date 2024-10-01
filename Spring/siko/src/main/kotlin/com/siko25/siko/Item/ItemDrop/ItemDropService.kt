package com.siko25.siko.item.itemdrop

import com.siko25.siko.character.enemy.*
import com.siko25.siko.character.player.*
import com.siko25.siko.item.*
import com.siko25.siko.item.effect.*
import com.siko25.siko.random.RandomService
import com.siko25.siko.stage.*
import org.springframework.stereotype.Service

@Service
class ItemDropService(
        private val playerService: PlayerService,
        private val itemDataRepository: ItemDataRepository,
        private val enemyDataRepository: EnemyDataRepository,
        private val stageDataRepository: StageDataRepository,
        private val stageEnterDropSetDataRepository: StageEnterDropSetDataRepository,
        private val randomService: RandomService,
        private val effectSetRepository: EffectSetRepository,
        private val effectDataRepository: EffectDataRepository,
        private val itemInstanceService: ItemInstanceService,
        private val inventoryService: InventoryService
) {

        fun getDropItems(stageEnterDropSet: StageEnterDropSetData): ItemData? {
                val stageDrop = stageEnterDropSet.stageDropSet
                val dropItems = calculateDropItemOnStageEnter(stageDrop)
                return dropItems
        }
        fun getClearDropItems(stageEnterDropSet: StageEnterDropSetData): ItemData? {
                val clearDrop = stageEnterDropSet.clearDropSet
                val clearDropItems = calculateClearDropItem(clearDrop)
                return clearDropItems
        }

        fun calculateDropItemOnStageEnter(stageDrop: StageDropSet): ItemData? {
                val dropSet = stageDrop.dropSets
                val randomID = randomService.getRandomWeightedItem(dropSet)

                if (randomID == null) {
                        return null
                }
                val itemData = itemDataRepository.findById(randomID.item).orElseGet(null)
                return itemData
        }

        fun calculateClearDropItem(stageDrop: ClearDropSet): ItemData? {
                val dropSet = stageDrop.dropSets
                val items = Array<ItemInstance?>(10) { null }
                for (i in items.indices) {
                        val itemDataId = randomService.getRandomWeightedItem(dropSet)

                        if (itemDataId == null) {
                                continue
                        }

                        val itemData = itemDataRepository.findById(itemDataId.item).orElseGet(null)

                        if (itemData == null) {
                                continue
                        }

                        val effectSet =
                                effectSetRepository.findById(itemData.effectSet).orElseGet(null)
                        if (effectSet == null) {
                                continue
                        }
                        items[i] = itemInstanceService.createItemInstance(itemData)
                }
                return null
        }

        fun tryDropItem(player: Player, stageEnterDropSet: StageEnterDropSetData) {
                val itemData = calculateDropItemOnStageEnter(stageEnterDropSet.stageDropSet)
                if (itemData == null) {
                        return
                }
                val item = itemInstanceService.createItemInstance(itemData)
                if (item == null) {
                        throw Exception("Failed to generate item instance")
                }
                inventoryService.addItem(player.id, item)
        }

        var playerDropTable = HashMap<String, Array<ItemData>>()
}
