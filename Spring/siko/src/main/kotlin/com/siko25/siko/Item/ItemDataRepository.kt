package com.siko25.siko.item

import org.springframework.data.mongodb.repository.MongoRepository
import org.springframework.stereotype.Repository

@Repository
interface ItemDataRepository : MongoRepository<ItemData, Long> {
    fun findByName(name: String): ItemData?
}
