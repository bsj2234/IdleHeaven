package com.siko25.siko.item.itemdrop

import org.springframework.data.annotation.Id
import org.springframework.data.mongodb.core.mapping.Document

@Document
data class ItemDropRateFamily(
        @Id val id: String,
        val name: String,
        val dropTable: Array<ItemDropRate>,
        val dropId: String
)
