package com.siko25.siko.service

import com.siko25.siko.document.Item
import com.siko25.siko.document.ItemDropRate
import com.siko25.siko.document.ItemDropRateFamily
import com.siko25.siko.document.Player
import com.siko25.siko.repository.ItemDropRateFamilyRepository
import org.springframework.stereotype.Service

@Service
class ItemDropService(
        private val itemDropRateFamilyRepository: ItemDropRateFamilyRepository,
        private val itemService: ItemService,
        private val playerService: PlayerService,
        private val weightedItemSelector: WeightedItemSelector
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
                                            )
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
                                            )
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
                                            )
                            ),
                    )
            itemDropRateFamilyRepository.saveAll(itemDropRates)
        }
    }

    fun getDropItem(playerId: String): Item? {
        val player = playerService.getPlayer(playerId)
        return player?.let { generateDrop(it) }
    }

    private fun generateDrop(player: Player): Item? {
        val allItems = itemService.getAllItems()
        val weights = allItems.map { item -> calculateItemWeight(item, player) }
        return weightedItemSelector.selectRandomItem(allItems, weights)
    }

    private fun calculateItemWeight(item: Item, player: Player): Double {
        // Implement your weight calculation logic here
        // This could be based on item rarity, player level, etc.
        return 1.0 // Placeholder: equal weights for all items
    }
}
