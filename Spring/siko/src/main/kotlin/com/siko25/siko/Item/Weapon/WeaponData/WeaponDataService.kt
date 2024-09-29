package com.siko25.siko.item.weapon.weaponData

import org.springframework.stereotype.Service

@Service
class WeaponDataService(private val weaponDataRepository: WeaponDataRepository) {
        fun initWeaponData(hardInit: Boolean) {
                if (weaponDataRepository.count() == 0L || hardInit) {
                        val weaponData =
                                listOf(
                                        WeaponData(
                                                id = "0",
                                                name = "Siko's Sword",
                                                type = arrayOf("Sword"),
                                                description = "A powerful sword",
                                                effectSet = "swordEffectSet",
                                                minDam = 15,
                                                maxDam = 20,
                                                level = 10,
                                                attackSpeed = 1.15,
                                                attackRange = 3,
                                                randomEffectType = "commonCloseWeaponEffect",
                                                critChan = 0.3,
                                                requiredLevel = 10,
                                                rarity = "rare"
                                        ),
                                        WeaponData(
                                                id = "1",
                                                name = "Siko's Staff",
                                                type = arrayOf("Staff"),
                                                description = "A magical staff",
                                                effectSet = "staffEffectSet",
                                                minDam = 15,
                                                maxDam = 20,
                                                level = 10,
                                                attackSpeed = 1.15,
                                                attackRange = 15,
                                                randomEffectType = "commonStaffEffect",
                                                critChan = 0.15,
                                                requiredLevel = 10,
                                                rarity = "rare"
                                        )
                                )
                        weaponDataRepository.saveAll(weaponData)
                }
        }

        fun getAllWeaponData(): List<WeaponData> = weaponDataRepository.findAll()
}
