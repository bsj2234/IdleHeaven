package com.siko25.siko.item.weapon

import com.siko25.siko.item.Item
import com.siko25.siko.item.effect.Effect
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
