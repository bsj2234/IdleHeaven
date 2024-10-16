package com.siko25.siko.service

import com.siko25.siko.document.Currency
import com.siko25.siko.document.Item
import com.siko25.siko.document.Weapon
import com.siko25.siko.repository.ItemRepository
import org.springframework.stereotype.Service

@Service
class ItemService(private val itemRepository: ItemRepository) {
    fun InitItems(hardInit: Boolean) {
        if (itemRepository.count() == 0L || hardInit) {
            val items =
                    listOf(
                            Currency("0", 1050, "Gold", listOf("Currency")),
                            Currency("1", 500, "Silver", listOf("Currency")),
                            Currency("2", 100, "Bronze", listOf("Currency")),
                            Currency("3", 50, "Diamond", listOf("Currency")),
                            Currency("4", 25, "Ruby", listOf("Currency")),
                            Weapon("5", "Siko's Sword", listOf("Equipment", "Weapon", "Sword")),
                    )

            itemRepository.saveAll(items)
        }
    }

    fun saveItem(item: Item): Item {
        return itemRepository.save(item)
    }

    fun getItemByName(name: String): Item? {
        return itemRepository.findByName(name)
    }

    fun getItemsByType(type: String): List<Item> {
        return itemRepository.findByType(type)
    }

    fun getAllItems(): List<Item> {
        return itemRepository.findAll()
    }
}
