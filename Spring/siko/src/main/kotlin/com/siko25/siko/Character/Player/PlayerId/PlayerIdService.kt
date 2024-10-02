package com.siko25.siko.character.player.playerId

import org.springframework.stereotype.Service

@Service
class PlayerIdService(private val playerIdRepositoryRedis: PlayerIdRepositoryRedis) {
    fun getPlayerId(id: String): PlayerId? {
        return playerIdRepositoryRedis.findById(id).orElse(null)
    }

    fun createPlayerId(id: String, token: String): PlayerId {
        val playerId = PlayerId(id, token)
        return playerIdRepositoryRedis.save(playerId)
    }

    fun getToken(id: String): String? {
        val playerId = getPlayerId(id)
        return playerId?.token
    }
}
