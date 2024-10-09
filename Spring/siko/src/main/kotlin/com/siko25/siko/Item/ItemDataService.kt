package com.siko25.siko.item

import org.springframework.stereotype.Service

@Service
class ItemDataService(private val itemDataRepository: ItemDataRepository) {
    fun getDataByName(name: String): ItemData {
        return itemDataRepository.findByName(name)
                ?: throw IllegalArgumentException("ItemData not found")
    }

    fun createItemData(itemData: ItemData): ItemData {
        itemDataRepository.save(itemData)
        return itemData
    }

    fun getAllItemDatas(): List<ItemData> {
        return itemDataRepository.findAll()
    }
}
