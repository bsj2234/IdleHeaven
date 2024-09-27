package com.siko25.siko.item.itemdrop

import org.springframework.data.annotation.Id
import org.springframework.data.mongodb.core.mapping.Document

@Document
data class ItemDropRateFamilySet(
        @Id val id: String,
        val dropRateFamilyRate: Array<ItemDropRateFamilyRate>
)
