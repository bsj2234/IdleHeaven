package com.siko25.siko.character.player

import org.springframework.stereotype.Service

@Service
class PlayerService(private val playerRepository: PlayerRepository) {

    fun initPlayerData(hardInit: Boolean) {
        if (hardInit) {
            playerRepository.deleteAll()
            playerRepository.save(
                    Player(
                            id = "0",
                            name = "Player",
                            level = 1,
                            experience = 0,
                            health = 100,
                            mana = 100,
                            strength = 10,
                            dexterity = 10,
                            inventory = mutableListOf()
                    )
            )
        }
    }

    fun getPlayer(playerId: String): Player? {
        // Implement player retrieval logic here
        // This might involve:
        return playerRepository.findById(playerId).orElse(null)
    }
}
