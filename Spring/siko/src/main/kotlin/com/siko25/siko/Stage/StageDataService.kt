package com.siko25.siko.stage

import com.siko25.siko.item.itemdrop.ItemDropRateFamilySetRepository
import org.springframework.stereotype.Service

@Service
class StageDataService(
        private val stageDataRepository: StageDataRepository,
        private val itemDropRateFamilySetRepository: ItemDropRateFamilySetRepository
) {

    fun initStageData(hardInit: Boolean) {
        if (hardInit) {
            stageDataRepository.deleteAll()
        }

        val stageData =
                StageData(
                        id = "0",
                        name = "1-1",
                        description = "1-1",
                        dropFamilySetId = "0",
                )
        stageDataRepository.save(stageData)
    }

    fun getStageData(stage: String): StageData? {
        return stageDataRepository.findById(stage).orElse(null)
    }
}
