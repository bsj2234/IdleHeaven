package com.siko25.siko.controller

import com.siko25.siko.document.Item
import com.siko25.siko.service.GameService
import com.siko25.siko.service.ItemDropService
import org.springframework.web.bind.annotation.*

@RestController
@RequestMapping("/game")
class GameController(
        private val gameService: GameService,
        private val dropService: ItemDropService
) {
    @GetMapping("/dropitem")
    fun GetDropItem(@RequestParam playerId: String): Item? {
        val droppedItem = dropService.getDropItem(playerId)

        return droppedItem
    }
}
