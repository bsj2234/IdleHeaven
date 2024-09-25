package com.siko25.siko.service

import com.siko25.siko.document.Rarity
import org.springframework.stereotype.Service

@Service
class ItemGenerator {
    data class GenerateInfo(val enemyLevel: Int, val itemRarity: Rarity)

    private lateinit var weaponData: List<String>
    private lateinit var armorData: List<String>
}

// Placeholder classes - implement these based on your game's needs
interface Item

data class Gold(val amount: Int) : Item

interface ItemData {
    val itemName: String
    fun getRandomItemInstance(name: String, generateInfo: ItemGenerator.GenerateInfo): Item
}
