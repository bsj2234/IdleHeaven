package com.siko25.siko.character.player

import org.springframework.data.mongodb.repository.MongoRepository

interface PlayerRepository : MongoRepository<Player, String> {
    fun findByName(name: String): Player
}
