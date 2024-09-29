package com.siko25.siko.random

import kotlin.random.Random
import org.springframework.stereotype.Service

@Service
class RandomService {
    fun randomDouble(min: Double = 0.0, max: Double = 1.0): Double {
        return Random.nextDouble(min, max)
    }

    fun getRandomWeightedItem(items: Array<WeightedItem>): WeightedItem? {
        val totalWeight = items.sumOf { it.weight }
        val randomValue = randomDouble(0.0, totalWeight)
        var cumulativeWeight = 0.0

        for (item in items) {
            cumulativeWeight += item.weight
            if (randomValue <= cumulativeWeight) {
                return item
            }
        }
        return null
    }
}
