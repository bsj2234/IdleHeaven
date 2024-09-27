package com.siko25.siko.item.itemdrop

import com.siko25.siko.character.enemy.*
import com.siko25.siko.character.player.*
import com.siko25.siko.item.*
import com.siko25.siko.stage.*
import com.siko25.siko.stage.StageDataRepository
import kotlin.random.Random
import org.springframework.stereotype.Service

@Service
class ItemDropService(
        private val itemDropRateFamilyRepository: ItemDropRateFamilyRepository,
        private val itemService: ItemService,
        private val playerService: PlayerService,
        private val weightedItemSelector: WeightedItemSelector,
        private val itemRepository: ItemRepository,
        private val enemyDataRepository: EnemyDataRepository,
        private val stageDataRepository: StageDataRepository,
        private val itemDropRateFamilySetRepository: ItemDropRateFamilySetRepository
) {
        fun initDropItemData(hardInit: Boolean) {
                if (itemDropRateFamilyRepository.count() == 0L || hardInit) {
                        val itemDropRates =
                                listOf(
                                        ItemDropRateFamily(
                                                id = "0",
                                                name = "DropRateFamily0",
                                                dropTable =
                                                        arrayOf(
                                                                ItemDropRate(
                                                                        itemId = "1",
                                                                        dropRate = 0.1
                                                                ),
                                                                ItemDropRate(
                                                                        itemId = "2",
                                                                        dropRate = 0.2
                                                                ),
                                                                ItemDropRate(
                                                                        itemId = "3",
                                                                        dropRate = 0.3
                                                                )
                                                        ),
                                                dropId = "0"
                                        ),
                                        ItemDropRateFamily(
                                                id = "1",
                                                name = "DropRateFamily1",
                                                dropTable =
                                                        arrayOf(
                                                                ItemDropRate(
                                                                        itemId = "2",
                                                                        dropRate = 0.2
                                                                ),
                                                                ItemDropRate(
                                                                        itemId = "3",
                                                                        dropRate = 0.3
                                                                ),
                                                                ItemDropRate(
                                                                        itemId = "4",
                                                                        dropRate = 0.4
                                                                )
                                                        ),
                                                dropId = "1"
                                        ),
                                        ItemDropRateFamily(
                                                id = "2",
                                                name = "DropRateFamily2",
                                                dropTable =
                                                        arrayOf(
                                                                ItemDropRate(
                                                                        itemId = "3",
                                                                        dropRate = 0.3
                                                                ),
                                                                ItemDropRate(
                                                                        itemId = "4",
                                                                        dropRate = 0.4
                                                                ),
                                                                ItemDropRate(
                                                                        itemId = "5",
                                                                        dropRate = 0.5
                                                                )
                                                        ),
                                                dropId = "2"
                                        ),
                                )
                        itemDropRateFamilyRepository.saveAll(itemDropRates)
                }
        }

        fun getDropItem(enemy: String): Item? {
                val enemyData = enemyDataRepository.findById(enemy).orElse(null) ?: return null
                return generateDropOnEnemyDead(enemyData)
        }
        fun getDropItems(stageClearRequest: StageClearRequest): List<Item>? {
                val stageId = stageClearRequest.stage
                val stageData = stageDataRepository.findById(stageId).orElse(null) ?: return null
                val dropFamilySet =
                        itemDropRateFamilySetRepository
                                .findById(stageData.dropFamilySetId)
                                .orElse(null)
                                ?: return null

                val items = generateItemFromDropRateFamilySet(dropFamilySet)

                return items
        }

        private fun generateItemFromDropRateFamilySet(
                dropFamilySet: ItemDropRateFamilySet
        ): List<Item>? {
                val dropRateFamilyRate = dropFamilySet.dropRateFamilyRate
                val totalRate = dropRateFamilyRate.sumOf { it.dropRate }

                val random = Random.nextDouble() * totalRate

                var accumulatedRate = 0.0
                val items = mutableListOf<Item>()
                for (rate in dropRateFamilyRate) {
                        accumulatedRate += rate.dropRate
                        if (accumulatedRate >= random) {
                                val itemDropRateFamily =
                                        itemDropRateFamilyRepository.findByDropId(rate.familyId)
                                val item = calculateDropItem(itemDropRateFamily)
                                if (item != null) {
                                        items.add(item)
                                }
                        }
                }
                return items
        }

        private fun generateDropOnEnemyDead(enemy: EnemyData): Item? {
                val itemDropRateFamily = itemDropRateFamilyRepository.findByDropId(enemy.dropId)
                return calculateDropItem(itemDropRateFamily)
        }

        private fun calculateDropItem(table: ItemDropRateFamily): Item? {
                val weightedItemSelector = WeightedItemSelector()
                return weightedItemSelector.selectRandomItem(table, itemRepository)
        }
}
