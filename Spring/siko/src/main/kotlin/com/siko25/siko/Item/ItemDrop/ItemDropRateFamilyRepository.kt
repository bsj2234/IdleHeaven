package com.siko25.siko.item.itemdrop

import org.springframework.data.mongodb.repository.MongoRepository

interface ItemDropRateFamilyRepository : MongoRepository<ItemDropRateFamily, String> {
        fun findByName(name: String): ItemDropRateFamily
        fun findByDropId(dropId: String): ItemDropRateFamily
}
