package com.siko25.siko.document

import org.springframework.data.annotation.Id
import org.springframework.data.mongodb.core.mapping.Document

@Document
data class ItemDropRateFamily(
        @Id val id: String,
        val name: String,
        val dropTable: Array<ItemDropRate>,
        val dropId: String
)

data class ItemDropRate(@Id val id: String, val itemId: String, val dropRate: Double)
