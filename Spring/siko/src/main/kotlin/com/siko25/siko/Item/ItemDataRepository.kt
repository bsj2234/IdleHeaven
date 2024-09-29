package com.siko25.siko.item

import org.springframework.data.mongodb.repository.MongoRepository

interface ItemDataRepository : MongoRepository<ItemData, String> {
    fun findByName(name: String): ItemData
    fun findByType(type: String): List<ItemData>
}
