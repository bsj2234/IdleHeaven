package com.siko25.siko.item

import com.siko25.siko.item.rarity.Rarity
import com.siko25.siko.random.WeightedItem
import org.springframework.data.annotation.Id
import org.springframework.data.mongodb.core.mapping.Document

@Document(collection = "item_data")
data class ItemData(
        @Id val id: String,
        val name: String,
        val description: String,
        val rarity: Rarity,
        val components: ComponentContainer<ItemDataComponent>,
        override val weight: Double
) : WeightedItem
