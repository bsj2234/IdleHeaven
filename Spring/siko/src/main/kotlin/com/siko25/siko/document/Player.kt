package com.siko25.siko.document

import org.springframework.data.annotation.Id
import org.springframework.data.mongodb.core.mapping.Document

@Document
data class Player(
        @Id val id: Int = 0,
        val name: String,
        val level: Int,
        val experience: Int,
        val health: Int,
        val mana: Int,
        val strength: Int,
        val dexterity: Int,
)
