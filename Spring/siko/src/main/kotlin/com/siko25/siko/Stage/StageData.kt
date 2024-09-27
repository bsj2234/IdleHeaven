package com.siko25.siko.stage

import org.springframework.data.annotation.Id
import org.springframework.data.mongodb.core.mapping.Document

@Document(collection = "stageData")
data class StageData(
        @Id val id: String,
        val name: String,
        val description: String,
        val dropFamilySetId: String
)
