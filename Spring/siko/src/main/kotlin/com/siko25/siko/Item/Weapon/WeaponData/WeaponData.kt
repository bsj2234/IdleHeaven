package com.siko25.siko.item.weapon.weaponData

import com.siko25.siko.item.ItemData
import org.springframework.data.annotation.Id
import org.springframework.data.mongodb.core.mapping.Document

@Document
data class WeaponData(
        @Id override val id: String = "0",
        override val name: String = "Weapon",
        override val type: Array<String> = arrayOf("Weapon"),
        override val description: String = "",
        override val effectSet: String = "",
        val minDam: Int = 0,
        val maxDam: Int = 0,
        val level: Int = 0,
        val attackSpeed: Double = 0.0,
        val attackRange: Int = 0,
        val randomEffectType: String = "commonCloseWeaponEffect",
        val critChan: Double = 0.0,
        val requiredLevel: Int = 0,
        val rarity: String = "common",
) : ItemData
