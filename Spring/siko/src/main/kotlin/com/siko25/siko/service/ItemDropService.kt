package com.siko25.siko.service

import com.siko25.siko.document.EnemyData
import com.siko25.siko.document.Item
import com.siko25.siko.document.ItemDropRate
import com.siko25.siko.document.ItemDropRateFamily
import com.siko25.siko.repository.EnemyDataRepository
import com.siko25.siko.repository.ItemDropRateFamilyRepository
import com.siko25.siko.repository.ItemRepository
import org.springframework.stereotype.Service

@Service
class ItemDropService(
        private val itemDropRateFamilyRepository: ItemDropRateFamilyRepository,
        private val itemService: ItemService,
        private val playerService: PlayerService,
        private val weightedItemSelector: WeightedItemSelector,
        private val itemRepository: ItemRepository,
        private val enemyDataRepository: EnemyDataRepository
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
                                                                        id = "0",
                                                                        itemId = "1",
                                                                        dropRate = 0.1
                                                                ),
                                                                ItemDropRate(
                                                                        id = "1",
                                                                        itemId = "2",
                                                                        dropRate = 0.2
                                                                ),
                                                                ItemDropRate(
                                                                        id = "2",
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
                                                                        id = "1",
                                                                        itemId = "2",
                                                                        dropRate = 0.2
                                                                ),
                                                                ItemDropRate(
                                                                        id = "2",
                                                                        itemId = "3",
                                                                        dropRate = 0.3
                                                                ),
                                                                ItemDropRate(
                                                                        id = "3",
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
                                                                        id = "2",
                                                                        itemId = "3",
                                                                        dropRate = 0.3
                                                                ),
                                                                ItemDropRate(
                                                                        id = "3",
                                                                        itemId = "4",
                                                                        dropRate = 0.4
                                                                ),
                                                                ItemDropRate(
                                                                        id = "4",
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
                return generateDropOnEnemyDead(enemyDataRepository.findById(enemy).get())
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
