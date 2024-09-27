package com.siko25.siko.item.itemdrop

import com.siko25.siko.item.*
import kotlin.random.Random
import org.springframework.stereotype.Service

@Service
class WeightedItemSelector {
    fun selectRandomItem(table: ItemDropRateFamily, itemtable: ItemRepository): Item? {
        val items = table.dropTable.map { it.itemId }
        val weights = table.dropTable.map { it.dropRate }
        if (items.isEmpty() || items.size != weights.size) {
            return null
        }

        val totalWeight = weights.sum()
        val randomValue = Random.nextDouble() * totalWeight

        var cumulativeWeight = 0.0
        for (i in items.indices) {
            cumulativeWeight += weights[i]
            if (randomValue <= cumulativeWeight) {
                return itemtable.findById(items[i]).get()
            }
        }

        return null
    }
}
