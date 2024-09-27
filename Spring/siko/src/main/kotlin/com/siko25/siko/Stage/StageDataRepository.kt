package com.siko25.siko.stage

import org.springframework.data.mongodb.repository.MongoRepository

interface StageDataRepository : MongoRepository<StageData, String> {
        fun findByName(name: String): StageData
}
