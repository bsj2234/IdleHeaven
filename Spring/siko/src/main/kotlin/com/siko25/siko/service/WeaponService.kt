package com.siko25.siko.service

import com.siko25.siko.document.Effect
import com.siko25.siko.document.Weapon
import com.siko25.siko.repository.WeaponRepository
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
