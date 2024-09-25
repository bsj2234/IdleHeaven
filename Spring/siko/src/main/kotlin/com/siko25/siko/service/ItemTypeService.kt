package com.siko25.siko.service

import com.siko25.siko.document.ItemType
import com.siko25.siko.repository.ItemTypeRepository
import org.springframework.stereotype.Service

@Service
class ItemTypeService(private val itemTypeRepository: ItemTypeRepository) {
    fun initItemTypes(hardInit: Boolean) {
        if (itemTypeRepository.count() == 0L || hardInit) {
            val itemTypes =
                    listOf(
                            ItemType("0", "Equipment"),
                            ItemType("1", "Consumable"),
                            ItemType("2", "Currency"),
                            ItemType("3", "Quest"),
                            ItemType("4", "Material"),
                            ItemType("5", "Key"),
                            ItemType("6", "Weapon"),
                            ItemType("7", "Armor"),
                            ItemType("8", "Accessory"),
                            ItemType("9", "Consumable"),
                            ItemType("10", "Currency"),
                            ItemType("11", "Quest"),
                            ItemType("12", "Material"),
                            ItemType("13", "Key"),
                            ItemType("14", "Other")
                    )
            itemTypeRepository.saveAll(itemTypes)
        }
    }

    fun getAllItemTypes(): List<ItemType> = itemTypeRepository.findAll()
}
