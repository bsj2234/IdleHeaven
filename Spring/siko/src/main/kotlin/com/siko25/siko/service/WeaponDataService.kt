package com.siko25.siko.service

import com.siko25.siko.document.WeaponData
import com.siko25.siko.repository.WeaponDataRepository
import org.springframework.stereotype.Service

@Service
class WeaponDataService(private val weaponDataRepository: WeaponDataRepository) {
        fun initWeaponData(hardInit: Boolean) {
                if (weaponDataRepository.count() == 0L || hardInit) {
                        val weaponData =
                                listOf(
                                        WeaponData(
                                                "0",
                                                "Siko's Sword",
                                                listOf("Sword"),
                                                15,
                                                20,
                                                level = 10,
                                                attackSpeed = 1.15,
                                                attackRange = 3,
                                                randomEffectType = "commonCloseWeaponEffect",
                                                critChan = .3,
                                                requiredLevel = 10,
                                                rarity = "rare",
                                        ),
                                        WeaponData(
                                                "0",
                                                "Siko's Staff",
                                                listOf("Staff"),
                                                15,
                                                20,
                                                level = 10,
                                                attackSpeed = 1.15,
                                                attackRange = 15,
                                                randomEffectType = "commonStaffEffect",
                                                critChan = .15,
                                                requiredLevel = 10,
                                                rarity = "rare",
                                        ),
                                )
                        weaponDataRepository.saveAll(weaponData)
                }
        }

        fun getAllWeaponData(): List<WeaponData> = weaponDataRepository.findAll()
}
