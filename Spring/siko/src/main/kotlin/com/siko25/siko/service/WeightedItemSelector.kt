package com.siko25.siko.service

import com.siko25.siko.document.Item
import kotlin.random.Random
import org.springframework.stereotype.Service

@Service
class WeightedItemSelector {
    fun selectRandomItem(items: List<Item>, weights: List<Double>): Item? {
        if (items.isEmpty() || items.size != weights.size) {
            return null
        }

        val totalWeight = weights.sum()
        val randomValue = Random.nextDouble() * totalWeight

        var cumulativeWeight = 0.0
        for (i in items.indices) {
            cumulativeWeight += weights[i]
            if (randomValue <= cumulativeWeight) {
                return items[i]
            }
        }

        return items.last()
    }
}
