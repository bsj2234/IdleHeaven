package com.siko25.siko.item.effect

import com.siko25.siko.random.RandomService
import org.springframework.stereotype.Service

@Service
class EffectService(
        val effectSetRepository: EffectSetRepository,
        val randomService: RandomService
) {
    fun getEffectSet(name: String): EffectSet {
        return effectSetRepository.findByName(name)
                ?: throw IllegalArgumentException("Effect set not found")
    }

    fun createEffect(effectData: EffectData): Effect {
        val effectValues = effectData.effectValues
        val randomValues =
                FloatArray(effectValues.size) { i ->
                    randomService.getRandomFloat(effectValues[i].min, effectValues[i].max)
                }
        return Effect(effectData.id, randomValues)
    }

    fun getEffects(effectSetName: String, count: Int): List<Effect> {
        val effectSet = getEffectSet(effectSetName)
        val effects = mutableListOf<Effect>()
        for (i in 0 until count) {
            val effectData = effectSet.effectDatas[i]
            val effect = createEffect(effectData)
            effects.add(effect)
        }
        return effects
    }

    fun getEffectsBySetName(name: String, count: Int): List<Effect> {
        val effects = mutableListOf<Effect>()
        for (i in 0 until count) {
            val effectSet = getEffectSet(name)
            val effectData = randomService.getRandomWeightedItem(effectSet.effectDatas)
            val effect = createEffect(effectData)
            effects.add(effect)
        }
        return effects
    }
}
