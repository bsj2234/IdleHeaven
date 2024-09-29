package com.siko25.siko.item

import com.siko25.siko.item.currency.Currency
import com.siko25.siko.item.weapon.WeaponData
import org.springframework.stereotype.Service

@Service
class ItemDataService(private val itemDataRepository: ItemDataRepository) {
    fun initItemDatas(hardInit: Boolean) {
        if (itemDataRepository.count() == 0L || hardInit) {
            val items =
                    listOf(
                            Currency("0", 1050, "Gold", arrayOf("Currency")),
                            Currency("1", 500, "Silver", arrayOf("Currency")),
                            Currency("2", 100, "Bronze", arrayOf("Currency")),
                            Currency("3", 50, "Diamond", arrayOf("Currency")),
                            Currency("4", 25, "Ruby", arrayOf("Currency")),
                            WeaponData(
                                    "5",
                                    "Siko's Sword",
                                    arrayOf("Equipment", "Weapon", "Sword")
                            ),
                    )

            itemDataRepository.saveAll(items)
        }
    }

    fun saveItem(item: ItemData): ItemData {
        return itemDataRepository.save(item)
    }

    fun getItemByName(name: String): ItemData? {
        return itemDataRepository.findByName(name)
    }

    fun getItemsByType(type: String): List<ItemData> {
        return itemDataRepository.findByType(type)
    }

    fun getAllItems(): List<ItemData> {
        return itemDataRepository.findAll()
    }
}
