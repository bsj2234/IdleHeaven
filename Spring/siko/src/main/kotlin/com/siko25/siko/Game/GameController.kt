package com.siko25.siko.game

import com.siko25.siko.OAuth.CustomOAuth2UserService
import com.siko25.siko.character.player.*
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
        private val stageEnterDropSetDataRepository: StageEnterDropSetDataRepository,
        private val playerService: PlayerService,
        private val customOAuth2UserService: CustomOAuth2UserService
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
    @GetMapping("/droppableDead", produces = ["application/json"])
    fun OnDroppableDead(
            @RequestParam playerId: String,
            @RequestParam stageId: String
    ): ResponseEntity<String> {
        return try {
            val player = playerService.getPlayer(playerId)
            if (player == null) {
                return ResponseEntity.status(HttpStatus.NOT_FOUND).body("Player not found")
            }
            val stageEnterDropSet = stageEnterDropSetDataRepository.findById(stageId).orElse(null)
            if (stageEnterDropSet == null) {
                return ResponseEntity.status(HttpStatus.NOT_FOUND)
                        .body("StageEnterDropSet not found")
            }
            dropService.tryDropItem(player, stageEnterDropSet)
            ResponseEntity.ok("Droppable dead")
        } catch (e: Exception) {
            logger.error("Error on droppable dead", e)
            ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Failed to droppable dead")
        }
    }

    @GetMapping("/test")
    fun test(): ResponseEntity<String> {
        return ResponseEntity.ok("test")
    }

    @PostMapping("/userinfo", consumes = ["application/json"], produces = ["application/json"])
    fun GetUserInfo(@RequestBody loginRequest: LoginRequest): ResponseEntity<String> {
        val isValid = customOAuth2UserService.validateUser(loginRequest.playerId, loginRequest.token)
        if (isValid) {
            return ResponseEntity.ok("Valid token")
        }
        return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body("Invalid token")
    }
}

class LoginRequest(val token: String, val playerId: String)
