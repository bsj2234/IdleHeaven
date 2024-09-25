package com.siko25.siko.controller

import com.siko25.siko.document.Item
import com.siko25.siko.service.ItemService
import org.springframework.web.bind.annotation.*

@RestController
@RequestMapping("/api/items")
class ItemController(private val itemService: ItemService) {

    @PostMapping
    fun createItem(@RequestBody item: Item): Item {
        return itemService.saveItem(item)
    }

    @GetMapping("/{name}")
    fun getItemByName(@PathVariable name: String): Item? {
        return itemService.getItemByName(name)
    }

    @GetMapping("/type/{type}")
    fun getItemsByType(@PathVariable type: String): List<Item> {
        return itemService.getItemsByType(type)
    }

    @GetMapping
    fun getAllItems(): List<Item> {
        return itemService.getAllItems()
    }
}
