package com.siko25.siko.character.player.playerId

import org.springframework.data.annotation.Id
import org.springframework.data.redis.core.RedisHash

@RedisHash("PlayerId") data class PlayerId(@Id val id: String, val token: String)
