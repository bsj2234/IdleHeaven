package com.siko25.siko.item.weapon.weaponData

import org.springframework.data.mongodb.repository.MongoRepository

interface WeaponDataRepository : MongoRepository<WeaponData, String> {
    fun findByName(name: String): List<WeaponData>
}
