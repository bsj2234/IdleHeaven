package com.siko25.siko.item.weapon

import com.siko25.siko.item.ItemData
import com.siko25.siko.item.effect.Effect
import org.springframework.data.annotation.Id
import org.springframework.data.mongodb.core.mapping.Document

@Document
data class WeaponData(
        @Id override val id: String = "0",
        override val name: String,
        override val type: Array<String> = arrayOf("Weapon"),
        val effects: Array<Effect> = arrayOf(),
        val enhanceLevel: Int = 0,
        override val description: String = "",
        val rarity: String = "Common",
        override val effectSet: String = ""
) : ItemData
