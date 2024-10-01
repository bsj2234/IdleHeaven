package com.siko25.siko.item

import org.springframework.data.annotation.Id
import org.springframework.data.redis.core.RedisHash

@RedisHash("ItemInstance")
data class ItemInstance(
        @Id val id: String,
        val name: String,
        val dataId: String,
        val effects: Array<String>,
        val count: Int,
)
