package com.siko25.siko.item

import org.springframework.http.HttpStatus
import org.springframework.http.ResponseEntity
import org.springframework.web.bind.annotation.*

@RestController
@RequestMapping("/api/items")
class ItemController(private val itemDataService: ItemDataService) {
    @GetMapping
    fun getAllItems(): ResponseEntity<List<ItemData>> {
        return try {
            val items = itemDataService.getAllItemDatas()
            ResponseEntity.ok(items)
        } catch (e: Exception) {
            println("Error fetching items: ${e.message}")
            ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(null)
        }
    }

    @PostMapping
    fun createItem(@RequestBody itemData: ItemData): ItemData {
        return itemDataService.createItemData(itemData)
    }
}
