package com.siko25.siko.stage

import org.springframework.stereotype.Service

@Service
class StageDataService(
        private val stageDataRepository: StageDataRepository,
) {

    fun initStageData(hardInit: Boolean) {
        if (hardInit) {
            stageDataRepository.deleteAll()
        }

        val stageData =
                StageData(
                        id = "0",
                        name = "1-1",
                        stageDropSetId = "0",
                        description = "1-1",
                )
        stageDataRepository.save(stageData)
    }

    fun getStageData(stage: String): StageData? {
        return stageDataRepository.findById(stage).orElse(null)
    }
}
