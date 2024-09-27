package com.siko25.siko.item.effect

import org.springframework.data.annotation.Id
import org.springframework.data.mongodb.core.mapping.Document

@Document
data class RandomEffectType(
        @Id val id: String = "0",
        val name: String,
        val type: List<String> = listOf("RandomEffectType"),
)
