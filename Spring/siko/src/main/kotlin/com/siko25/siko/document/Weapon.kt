package com.siko25.siko.document

import org.springframework.data.annotation.Id
import org.springframework.data.mongodb.core.mapping.Document

@Document
data class Weapon(
        @Id override val id: String = "0",
        override val name: String,
        override val type: List<String> = listOf("Weapon"),
        val effects: Array<Effect> = arrayOf(),
        val enhanceLevel: Int = 0,
) : Item
