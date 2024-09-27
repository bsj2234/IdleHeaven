package com.siko25.siko.item

import org.springframework.data.mongodb.repository.MongoRepository

interface ItemRepository : MongoRepository<Item, String> {
    fun findByName(name: String): Item
    fun findByType(type: String): List<Item>
}
