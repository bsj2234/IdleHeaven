package com.siko25.siko.item

import org.springframework.web.bind.annotation.*

@RestController
@RequestMapping("/api/items")
class ItemController(private val itemService: ItemDataService) {}
