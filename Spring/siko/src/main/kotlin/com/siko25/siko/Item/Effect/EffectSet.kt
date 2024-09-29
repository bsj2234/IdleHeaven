package com.siko25.siko.item.effect

import com.siko25.siko.random.WeightedItem
import org.springframework.data.mongodb.core.mapping.Document

@Document data class EffectSet(val effects: Array<WeightedItem>)
