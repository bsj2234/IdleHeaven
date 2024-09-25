package com.siko25.siko

import com.siko25.siko.service.ItemDropService
import com.siko25.siko.service.ItemService
import com.siko25.siko.service.ItemTypeService
import com.siko25.siko.service.WeaponDataService
import com.siko25.siko.service.WeaponService
import org.springframework.boot.CommandLineRunner
import org.springframework.boot.autoconfigure.SpringBootApplication
import org.springframework.boot.runApplication
import org.springframework.context.annotation.Bean

@SpringBootApplication
class SikoApplication {

    @Bean
    fun init(
            itemTypeService: ItemTypeService,
            itemService: ItemService,
            weaponService: WeaponService,
            weaponDataService: WeaponDataService,
            itemDropService: ItemDropService
    ) = CommandLineRunner {
        itemTypeService.initItemTypes(hardInit = true)
        itemService.InitItems(hardInit = true)
        weaponDataService.initWeaponData(hardInit = true)
        weaponService.initWeaponData(hardInit = true)
        itemDropService.initDropItemData(hardInit = true)
    }
}

fun main(args: Array<String>) {
    runApplication<SikoApplication>(*args)
}
