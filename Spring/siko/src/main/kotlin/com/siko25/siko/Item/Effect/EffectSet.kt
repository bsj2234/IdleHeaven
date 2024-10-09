package com.siko25.siko.item.effect

import com.siko25.siko.random.*
import org.springframework.data.mongodb.core.mapping.Document

@Document
data class EffectSet(
        val name: String,
        val effectDatas: Array<EffectData>,
        override val weight: Double
) : WeightedItem
