package com.siko25.siko.repository

import com.siko25.siko.document.EnemyData
import com.siko25.siko.document.Item
import com.siko25.siko.document.ItemDropRateFamily
import com.siko25.siko.document.ItemType
import com.siko25.siko.document.Weapon
import com.siko25.siko.document.WeaponData
import org.springframework.data.mongodb.repository.MongoRepository

interface ItemRepository : MongoRepository<Item, String> {
    fun findByName(name: String): Item
    fun findByType(type: String): List<Item>
}

interface ItemTypeRepository : MongoRepository<ItemType, String> {
    fun findByName(name: String): List<ItemType>
}

interface WeaponDataRepository : MongoRepository<WeaponData, String> {
    fun findByName(name: String): List<WeaponData>
}

interface WeaponRepository : MongoRepository<Weapon, String> {
    fun findByName(name: String): List<Weapon>
}

interface ItemDropRateFamilyRepository : MongoRepository<ItemDropRateFamily, String> {
    fun findByName(name: String): ItemDropRateFamily
    fun findByDropId(dropId: String): ItemDropRateFamily
}

interface EnemyDataRepository : MongoRepository<EnemyData, String> {
    fun findByName(name: String): EnemyData
}
