package com.siko25.siko.character.enemy

import com.siko25.siko.character.enemy.*
import org.springframework.stereotype.Service

@Service
class EnemyDataService(private val enemyDataRepository: EnemyDataRepository) {
    fun initEnemyData(hardInit: Boolean) {
        if (enemyDataRepository.count() == 0L || hardInit) {
            val enemyData =
                    listOf(
                            EnemyData(id = "0", name = "Enemy0", dropId = "0"),
                            EnemyData(id = "1", name = "Enemy1", dropId = "1")
                    )
            enemyDataRepository.saveAll(enemyData)
        }
    }
}
