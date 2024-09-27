package com.siko25.siko.item.weapon

import com.siko25.siko.item.effect.Effect
import org.springframework.stereotype.Service

@Service
class WeaponService(private val weaponRepository: WeaponRepository) {
        fun initWeaponData(hardInit: Boolean) {
                if (weaponRepository.count() == 0L || hardInit) {
                        val weapon =
                                listOf(
                                        Weapon(
                                                "0",
                                                "Siko's Sword",
                                                listOf("Sword"),
                                                arrayOf(
                                                        Effect(
                                                                effectType = "STR Bonus",
                                                                effectValues = floatArrayOf(10f),
                                                        ),
                                                        Effect(
                                                                effectType = "HP Bonus",
                                                                effectValues = floatArrayOf(3f),
                                                        ),
                                                        Effect(
                                                                effectType = "DEF Bonus",
                                                                effectValues = floatArrayOf(7f),
                                                        ),
                                                ),
                                                enhanceLevel = 0,
                                        )
                                )
                        weaponRepository.saveAll(weapon)
                }
        }

        fun getAllWeaponData(): List<Weapon> = weaponRepository.findAll()
}
