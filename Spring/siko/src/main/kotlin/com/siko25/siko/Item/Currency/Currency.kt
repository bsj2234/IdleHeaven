package com.siko25.siko.item.currency

import com.siko25.siko.item.ItemData
import org.springframework.data.annotation.Id
import org.springframework.data.mongodb.core.mapping.Document

@Document
data class Currency(
        @Id override val id: String = "0",
        val amount: Int,
        override val name: String = "Gold",
        override val type: Array<String> = arrayOf("Currency"),
        override val description: String = "Gold",
        val rarity: String = "Common",
        override val effectSet: String = ""
) : ItemData
