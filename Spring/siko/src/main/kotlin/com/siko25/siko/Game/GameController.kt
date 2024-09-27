package com.siko25.siko.game

import com.siko25.siko.character.player.PlayerRepository
import com.siko25.siko.item.Item
import com.siko25.siko.item.itemdrop.ItemDropService
import com.siko25.siko.stage.StageClearRequest
import org.slf4j.LoggerFactory
import org.springframework.http.HttpStatus
import org.springframework.http.ResponseEntity
import org.springframework.web.bind.annotation.*

@RestController
@RequestMapping("/game")
class GameController(
        private val gameService: GameService,
        private val dropService: ItemDropService,
        private val playerRepository: PlayerRepository
) {
    private val logger = LoggerFactory.getLogger(GameController::class.java)

    @GetMapping("/dropitem", produces = ["application/json"])
    fun getDropItem(@RequestParam enemy: String): ResponseEntity<Item?> {
        return try {
            val droppedItem = dropService.getDropItem(enemy)
            ResponseEntity.ok(droppedItem)
        } catch (e: Exception) {
            logger.error("Error getting drop item for enemy: $enemy", e)
            ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(null)
        }
    }

    @PostMapping("/stageClear", consumes = ["application/json"], produces = ["application/json"])
    fun OnStageClear(@RequestBody stageClearRequest: StageClearRequest): ResponseEntity<String> {
        return try {
            val rewards = dropService.getDropItems(stageClearRequest)
            val playerId = stageClearRequest.playerId

            val playerData = playerRepository.findById(playerId).get()
            playerData.inventory.addAll(rewards ?: mutableListOf())
            playerRepository.save(playerData)

            ResponseEntity.ok("Stage cleared successfully")
        } catch (e: Exception) {
            logger.error("Error on stage clear", e)
            ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Failed to clear stage")
        }
    }
}
