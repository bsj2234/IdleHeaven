package com.siko25.siko.item.effect

import org.springframework.stereotype.Service

@Service
class EffectDataService(private val effectDataRepository: EffectDataRepository) {
    fun getEffectData(id: String): EffectData {
        return effectDataRepository.findById(id).orElseThrow {
            IllegalArgumentException("EffectData not found")
        }
    }
}
