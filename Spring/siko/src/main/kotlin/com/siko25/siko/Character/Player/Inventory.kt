package com.siko25.siko.character.player

import com.siko25.siko.item.ItemInstance
import org.springframework.data.annotation.Id
import org.springframework.data.redis.core.RedisHash

@RedisHash("Inventory")
data class Inventory(
        @Id val playerId: String,
        val items: MutableList<ItemInstance> = mutableListOf()
)
