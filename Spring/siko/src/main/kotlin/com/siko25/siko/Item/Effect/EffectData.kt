package com.siko25.siko.item.effect

import com.siko25.siko.random.WeightedItem
import org.springframework.data.mongodb.core.mapping.Document

enum class EffectParamType {
        FLOAT,
        BOOLEAN
}

@Document
data class EffectData(
        val id: String,
        val name: String,
        val description: String,
        val effectValues: List<EffectValue>,
        override val weight: Double
) : WeightedItem

data class EffectValue(val min: Float, val max: Float, val description: String)
