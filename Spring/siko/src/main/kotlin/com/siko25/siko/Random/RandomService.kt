package com.siko25.siko.random

import kotlin.random.Random
import org.springframework.stereotype.Service

@Service
class RandomService {
    fun randomDouble(min: Double = 0.0, max: Double = 1.0): Double {
        return Random.nextDouble(min, max)
    }

    fun <T : WeightedItem> getRandomWeightedItem(items: Array<T>): T {
        val totalWeight = items.sumOf { it.weight }
        val randomValue = randomDouble(0.0, totalWeight)
        var cumulativeWeight = 0.0

        for (item in items) {
            cumulativeWeight += item.weight
            if (randomValue <= cumulativeWeight) {
                return item
            }
        }
        throw Exception("Failed to get random weighted item")
    }

    fun getRandomFloat(min: Float, max: Float): Float {
        return Random.nextFloat() * (max - min) + min
    }
}
