package com.siko25.siko.character.enemy

import org.springframework.data.mongodb.repository.MongoRepository

interface EnemyDataRepository : MongoRepository<EnemyData, String> {
    fun findByName(name: String): EnemyData
}
