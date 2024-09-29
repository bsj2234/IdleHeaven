package com.siko25.siko.game

import com.siko25.siko.character.player.PlayerRepository
import com.siko25.siko.item.itemdrop.ItemDropService
import com.siko25.siko.item.itemdrop.StageEnterDropSetDataRepository
import com.siko25.siko.stage.StageDataRepository
import com.siko25.siko.stage.StageEnterRequest
import org.slf4j.LoggerFactory
import org.springframework.http.HttpStatus
import org.springframework.http.ResponseEntity
import org.springframework.web.bind.annotation.*

@RestController
@RequestMapping("/game")
class GameController(
        private val gameService: GameService,
        private val dropService: ItemDropService,
        private val playerRepository: PlayerRepository,
        private val stageDataRepository: StageDataRepository,
        private val stageEnterDropSetDataRepository: StageEnterDropSetDataRepository
) {
    private val logger = LoggerFactory.getLogger(GameController::class.java)

    @PostMapping("/stageEnter", consumes = ["application/json"], produces = ["application/json"])
    fun OnStageClear(@RequestBody stageEnterRequest: StageEnterRequest): ResponseEntity<String> {
        return try {
            val stageId = stageEnterRequest.stageId
            val stage = stageDataRepository.findById(stageId).orElse(null)
            if (stage == null) {
                return ResponseEntity.status(HttpStatus.NOT_FOUND).body("Stage not found")
            }
            val StageDropSet =
                    stageEnterDropSetDataRepository.findById(stage.stageDropSetId).orElse(null)
            val dropItems = dropService.getDropItems(StageDropSet)
            val clearItems = dropService.getClearDropItems(StageDropSet)

            val playerId = stageEnterRequest.playerId

            val playerData = playerRepository.findById(playerId).get()
            playerRepository.save(playerData)

            ResponseEntity.ok("Stage cleared successfully")
        } catch (e: Exception) {
            logger.error("Error on stage clear", e)
            ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Failed to clear stage")
        }
    }
}
