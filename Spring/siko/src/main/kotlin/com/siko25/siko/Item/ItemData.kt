package com.siko25.siko.item

import org.springframework.data.annotation.Id
import org.springframework.data.mongodb.core.mapping.Document

interface ItemData {
        val id: String
        val name: String
        val type: Array<String>
        val description: String
        val effectSet: String
}

@Document
data class Equipment(
        @Id override val id: String = "0",
        override val name: String,
        override val type: Array<String>,
        override val description: String,
        val rarity: String,
        override val effectSet: String
) : ItemData
