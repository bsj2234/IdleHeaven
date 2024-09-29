package com.siko25.siko.item.itemdrop

import com.siko25.siko.random.*
import org.springframework.data.annotation.Id
import org.springframework.data.mongodb.core.mapping.Document

@Document
data class StageEnterDropSetData(
        @Id val id: String,
        val stageDropSet: StageDropSet,
        val clearDropSet: ClearDropSet
)

@Document data class StageDropSet(val dropSets: Array<WeightedItem>, val dropCount: RandomCount)

@Document data class ClearDropSet(val dropSets: Array<WeightedItem>)

@Document data class RandomCount(@Id val name: String, val min: Int, val max: Int)
