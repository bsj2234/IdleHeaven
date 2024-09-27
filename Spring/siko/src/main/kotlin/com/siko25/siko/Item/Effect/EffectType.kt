package com.siko25.siko.item.effect

import org.springframework.data.annotation.Id
import org.springframework.data.mongodb.core.mapping.Document

@Document
data class EffectType(
        @Id val id: String = "0",
        val typeName: String = "Currency",
        val rarity: String = "Common",
        val level: Int = 1
)
