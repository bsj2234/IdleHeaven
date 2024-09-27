package com.siko25.siko.item

import com.siko25.siko.item.rarity.Rarity
import org.springframework.stereotype.Service

@Service
class ItemGenerator {
    data class GenerateInfo(val enemyLevel: Int, val itemRarity: Rarity)

    private lateinit var weaponData: List<String>
    private lateinit var armorData: List<String>
}
