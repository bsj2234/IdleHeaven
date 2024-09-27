package com.siko25.siko.item

import org.springframework.data.annotation.Id
import org.springframework.data.mongodb.core.mapping.Document

@Document
data class ItemType(
        @Id val id: String = "0",
        val name: String = "Equipment",
)