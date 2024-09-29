package com.siko25.siko

import com.siko25.siko.character.enemy.*
import com.siko25.siko.character.player.*
import com.siko25.siko.item.*
import com.siko25.siko.item.effect.*
import com.siko25.siko.item.itemdrop.*
import com.siko25.siko.item.rarity.*
import com.siko25.siko.item.weapon.*
import com.siko25.siko.item.weapon.weaponData.*
import com.siko25.siko.stage.*
import org.springframework.boot.CommandLineRunner
import org.springframework.boot.autoconfigure.SpringBootApplication
import org.springframework.boot.runApplication
import org.springframework.context.annotation.Bean

@SpringBootApplication
class SikoApplication {

    @Bean
    fun init(
            itemTypeService: ItemTypeService,
            itemDataService: ItemDataService,
            weaponService: WeaponService,
            weaponDataService: WeaponDataService,
            enemyDataService: EnemyDataService,
            stageDataService: StageDataService,
            playerService: PlayerService,
    ) = CommandLineRunner {
        itemTypeService.initItemTypes(hardInit = true)
        itemDataService.initItemDatas(hardInit = true)
        weaponDataService.initWeaponData(hardInit = true)
        weaponService.initWeaponData(hardInit = true)
        enemyDataService.initEnemyData(hardInit = true)
        stageDataService.initStageData(hardInit = true)
        playerService.initPlayerData(hardInit = true)
    }
}

fun main(args: Array<String>) {
    runApplication<SikoApplication>(*args)
}
