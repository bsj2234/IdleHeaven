package com.siko25.siko.item

import org.springframework.data.mongodb.repository.MongoRepository
import org.springframework.stereotype.Repository

@Repository
interface ItemTypeRepository : MongoRepository<ItemType, String> {
    fun findByName(name: String): List<ItemType>
}
