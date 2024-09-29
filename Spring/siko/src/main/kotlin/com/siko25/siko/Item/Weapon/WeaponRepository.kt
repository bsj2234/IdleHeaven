package com.siko25.siko.item.weapon

import org.springframework.data.mongodb.repository.MongoRepository

interface WeaponRepository : MongoRepository<WeaponData, String> {
    fun findByName(name: String): List<WeaponData>
}
