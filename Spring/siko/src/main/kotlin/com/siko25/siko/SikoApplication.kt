package com.siko25.siko

import com.siko25.siko.character.player.*
import com.siko25.siko.item.*
import com.siko25.siko.item.effect.*
import com.siko25.siko.item.rarity.*
import org.springframework.boot.CommandLineRunner
import org.springframework.boot.autoconfigure.SpringBootApplication
import org.springframework.boot.runApplication
import org.springframework.context.annotation.Bean
import org.springframework.data.redis.repository.configuration.EnableRedisRepositories

@EnableRedisRepositories(basePackages = ["com.siko25.siko"])
@SpringBootApplication
class SikoApplication {

    @Bean fun init() = CommandLineRunner {}
}

fun main(args: Array<String>) {
    runApplication<SikoApplication>(*args)
}
